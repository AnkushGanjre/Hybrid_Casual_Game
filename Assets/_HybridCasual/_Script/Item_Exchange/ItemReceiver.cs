using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemReceiver : MonoBehaviour
{
    Material stallMaterial;
    [SerializeField] int paperRollCount = 0;

    private void Start()
    {
        stallMaterial = GetComponentInParent<Renderer>().material;
    }

    private void Update()
    {
        if (paperRollCount <= 0)
        {
            stallMaterial.color = Color.red;
        }
        else stallMaterial.color = Color.blue;
    }

    public void ReceivePaperRolls()
    {
        paperRollCount += 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (paperRollCount > 0) return;

            PlayerManager player = other.GetComponent<PlayerManager>();
            if (player != null)
            {
                if (player.inventoryItem.Count > 0)
                {
                    player.RemoveItemFromInventory();
                    ReceivePaperRolls();
                }
            }
            // if there is no paper roll &&
            // if player invertory has paper roll
            // then ReceivePaperRolls()
        }
    }
}
