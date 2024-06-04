using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemDespenser : MonoBehaviour
{
    [SerializeField] GameObject itemDespenserPrefab;
    Coroutine stackItemRoutine;
    float waitTimeToDispense = 1f;

    private void Start()
    {
        if (itemDespenserPrefab == null)
            Debug.Log("Item for Despenser not assigned");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
            if (player != null && player.inventoryItem.Count < player.InventoryLimit)
            {
                stackItemRoutine = StartCoroutine(GiveItemToPlayer(player));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (stackItemRoutine != null)
            {
                StopCoroutine(stackItemRoutine);
                stackItemRoutine = null;
            }
        }
    }

    private IEnumerator GiveItemToPlayer(PlayerManager player)
    {
        while (player.inventoryItem.Count < player.InventoryLimit)
        {
            yield return new WaitForSeconds(waitTimeToDispense);
            player.AddItemToInventory(itemDespenserPrefab);
        }
    }
}
