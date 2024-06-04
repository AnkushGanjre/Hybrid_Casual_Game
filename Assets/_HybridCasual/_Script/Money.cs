using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] int moneyValue = 25;
    [SerializeField] float collectionDuration = 0.5f;
    [SerializeField] float animationDuration = 2f;
    [SerializeField] float height = 7f;
    BoxCollider boxCollider;

    private bool isMovingTowardsPlayer = false;
    private bool isAddingMoneyOverTime = false;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
            if (player != null)
            {
                StartCoroutine(MoveTowardsPlayer(player));
                StartCoroutine(AddMoneyOverTime(player));
                boxCollider.enabled = false;
            }
        }
    }

    IEnumerator AddMoneyOverTime(PlayerManager player)
    {
        isAddingMoneyOverTime = true;
        float timer = 0f;
        int totalValue = 0;
        while (timer < collectionDuration)
        {
            float progress = timer / collectionDuration;
            int amountToAdd = Mathf.RoundToInt(Mathf.Lerp(0, moneyValue, progress));

            // Calculate the incremental value to add to the player's coins
            int incrementalValue = amountToAdd - totalValue;

            // Add the incremental value to the player's coins
            player.ModifyPlayerCoins(incrementalValue);

            // Update the total value
            totalValue += incrementalValue;


            timer += Time.deltaTime;
            yield return null;
        }

        // Add remaining money in case of rounding errors
        if (totalValue != moneyValue)
        {
            player.ModifyPlayerCoins(moneyValue - totalValue);
        }

        isAddingMoneyOverTime = false;
        if (!isMovingTowardsPlayer)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator MoveTowardsPlayer(PlayerManager player)
    {
        isMovingTowardsPlayer = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = player.transform.position;
        Vector3 peakPosition = startPosition + Vector3.up * height;
        float elapsedTime = 0f;

        // Move upwards first
        while (transform.position != peakPosition)
        {
            transform.position = Vector3.Lerp(startPosition, peakPosition, elapsedTime / animationDuration/2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move towards the player
        elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            targetPosition = player.transform.position;
            transform.position = Vector3.Lerp(peakPosition, targetPosition, elapsedTime / animationDuration/2);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsedTime / (animationDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the money note reaches exactly at the player's position
        targetPosition = player.transform.position;
        transform.position = targetPosition;
        transform.localScale = Vector3.zero;

        isMovingTowardsPlayer = false;
        if (!isAddingMoneyOverTime)
        {
            Destroy(gameObject);
        }
    }
}
