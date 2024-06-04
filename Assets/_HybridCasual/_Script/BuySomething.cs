using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class BuySomething : MonoBehaviour
{
    [SerializeField] string context = "";
    [SerializeField] int unlockAmount = 100;
    [SerializeField] float totalTimeToBuy = 3f;


    TextMeshProUGUI textUI;
    Coroutine buyCoroutine;
    int totalPlayerCoinSubtracted;
    int initialAmount;
    float elapsedTime = 0f;


    public UnityEvent unlockEvent;

    private void Start()
    {
        textUI = GetComponentInChildren<TextMeshProUGUI>();
        textUI.text = context +"\n$" + unlockAmount;
        initialAmount = unlockAmount;
        totalPlayerCoinSubtracted = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
            if (player != null && player.PlayerCoins > 0)
            {
                buyCoroutine = StartCoroutine(AreaBuyRoutine(player));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (buyCoroutine != null)
            {
                StopCoroutine(buyCoroutine);
                buyCoroutine = null;
            }
        }
    }

    IEnumerator AreaBuyRoutine(PlayerManager player)
    {
        int amount = unlockAmount;
        int previousAmount = unlockAmount;

        while (elapsedTime < totalTimeToBuy)
        {
            if (unlockAmount > 0 && player.PlayerCoins <= 0) yield break;

            // Calculate the new amount based on time elapsed
            float percentage = elapsedTime / totalTimeToBuy;
            unlockAmount = Mathf.RoundToInt(Mathf.Lerp(amount, 0, percentage));


            // Subtract coins from the player
            int coinsToSubtract = previousAmount - unlockAmount;
            player.ModifyPlayerCoins(-(coinsToSubtract));
            totalPlayerCoinSubtracted += coinsToSubtract;
            previousAmount = unlockAmount; // Update previousAmount


            UpdateAmountUI();
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the amount is exactly 0 at the end
        unlockAmount = 0;

        // This is to ensure deduction discrepancies in the end
        if (initialAmount != totalPlayerCoinSubtracted)
        {
            int diff = initialAmount - totalPlayerCoinSubtracted;
            player.ModifyPlayerCoins(-(diff));
        }
        unlockEvent?.Invoke();
        gameObject.SetActive(false);
    }

    void UpdateAmountUI()
    {
        textUI.text = context + "\n$" + unlockAmount;
    }
}
