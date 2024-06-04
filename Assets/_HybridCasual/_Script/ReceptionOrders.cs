using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReceptionOrders : MonoBehaviour
{
    [SerializeField] Image orderFillImage;
    Coroutine orderTakingRoutine;
    float waitTimeToExecute = 2f;
    NPCManager npcManager;

    private void Start()
    {
        orderFillImage.fillAmount = 0f;
        npcManager = FindObjectOfType<NPCManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            orderTakingRoutine = StartCoroutine(StartTakingOrder());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(orderTakingRoutine);
            orderTakingRoutine = null;
        }
    }

    

    IEnumerator StartTakingOrder()
    {
        while (npcManager.ForeFrontCustomer == null)
        {
            yield return null; // Wait for the next frame before checking again
        }
        HotelRoom hr = null;
        while (hr == null)
        {
            hr = GetRoom();
            yield return null;
        }

        float timer = 0f;
        float startFillAmount = orderFillImage.fillAmount;
        float targetFillAmount = 1f;

        while (timer < waitTimeToExecute)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Clamp01(timer / waitTimeToExecute);
            float fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, percentage);
            orderFillImage.fillAmount = fillAmount;
            yield return null;
        }

        orderFillImage.fillAmount = targetFillAmount;
        yield return new WaitForSeconds(0.1f);
        orderFillImage.fillAmount = 0f;

        // Give NPC To Go Room
        NPCCustomer customer = npcManager.ForeFrontCustomer.GetComponent<NPCCustomer>();
        if (customer != null)
        {
            customer.GoToRoom(hr.BedPosition);
            Debug.Log(hr.BedPosition);
        }
        WorldManager.Instance.OccupiedHotelRooms.Add(hr.gameObject);
        npcManager.ForeFrontCustomer = null;
        npcManager.SetNPCDestination();
    }

    HotelRoom GetRoom()
    {
        foreach (var room in WorldManager.Instance.AllHotelRooms)
        {
            HotelRoom hr = room.GetComponent<HotelRoom>();
            if (!hr.IsOccupied)
                return hr;
        }
        return null;
    }
}
