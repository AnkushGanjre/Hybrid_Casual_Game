using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] GameObject npcPrefab;
    [SerializeField] Transform npcSpawnPos;
    [SerializeField] Transform npcHomePos;

    public List<Transform> waitingQueuePositionList = new List<Transform>();
    int npcCount = 0;

    public GameObject ForeFrontCustomer = null;

    void Start()
    {
        InvokeRepeating(nameof(SpawnNPC), 1f, 5f);
    }

    public void SpawnNPC()
    {
        if (WorldManager.Instance.CustomerList.Count >= waitingQueuePositionList.Count) return;

        GameObject go = Instantiate(npcPrefab, npcSpawnPos.position, Quaternion.identity);
        WorldManager.Instance.CustomerList.Add(go);
        go.name = npcCount.ToString();
        Invoke(nameof(SetNPCDestination), 1f);
        npcCount++;
    }

    public void SetNPCDestination()
    {
        if (WorldManager.Instance.CustomerList.Count > waitingQueuePositionList.Count) Debug.LogWarning("Error more npc less position.");

        int index = 0;
        foreach (var go in WorldManager.Instance.CustomerList)
        {
            NPCCustomer npc = go.GetComponent<NPCCustomer>();
            Vector3 pos = waitingQueuePositionList[index].position;
            if (npc != null)
                npc.GoToDestination(pos);
            index++;
        }
    }
}
