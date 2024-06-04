using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerHolder;
    public int PlayerCoins = 0;
    public int InventoryLimit = 3;

    public List<GameObject> inventoryItem = new List<GameObject>();
    [SerializeField] TextMeshProUGUI coinsText;


    PlayerMovement movement;
    float holdingYDistance = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (inventoryItem.Count > 0)
            movement.IsCarrying = true;
        else movement.IsCarrying = false;

        coinsText.text = "Coins: $" + PlayerCoins;
    }

    public void AddItemToInventory(GameObject go)
    {
        inventoryItem.Add(go);
        Instantiate(go, playerHolder);
        UpdateInventory();
    }

    public void RemoveItemFromInventory()
    {
        if (inventoryItem.Count <= 0 || playerHolder.childCount <= 0) return;

        inventoryItem.RemoveAt(inventoryItem.Count - 1);
        Transform lastChild = playerHolder.GetChild(playerHolder.childCount - 1);
        Destroy(lastChild.gameObject);
    }

    void UpdateInventory()
    {
        if (playerHolder.childCount <= 0) return;

        for (int i = 0; i < playerHolder.childCount; i++)
        {
            int a = i;
            Vector3 pos = playerHolder.GetChild(a).localPosition;
            pos.y = a * holdingYDistance;
            playerHolder.GetChild(a).localPosition = pos;
        }
    }

    public void ModifyPlayerCoins(int amount)
    {
        // Method to Modify coins
        PlayerCoins += amount;
        if (PlayerCoins < 0)
        {
            PlayerCoins = 0; // Ensure player's coins don't go negative
        }
    }
}
