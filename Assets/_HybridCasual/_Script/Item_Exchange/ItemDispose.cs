using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemDispose : MonoBehaviour
{
    Coroutine RemoveItemRoutine;
    float waitTimeToDispose = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
            if (player != null && player.inventoryItem.Count > 0)
            {
                RemoveItemRoutine = StartCoroutine(RemoveItemFromPlayer(player));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (RemoveItemRoutine != null)
            {
                StopCoroutine(RemoveItemRoutine);
                RemoveItemRoutine = null;
            }
        }
    }

    private IEnumerator RemoveItemFromPlayer(PlayerManager player)
    {
        while (player.inventoryItem.Count > 0)
        {
            yield return new WaitForSeconds(waitTimeToDispose);
            Debug.Log("Item remove");
            player.RemoveItemFromInventory();
        }
    }
}
