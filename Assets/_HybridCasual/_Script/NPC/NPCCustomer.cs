using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCCustomer : MonoBehaviour
{
    [SerializeField] Vector3 target = Vector3.zero;
    NavMeshAgent agent;
    Animator animator;
    NPCManager npcManager;
    bool isSetFirstCustomer = false;
    int npcRoomWaitTime = 5;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        npcManager = FindObjectOfType<NPCManager>();
    }

    private void Update()
    {
        animator.SetFloat("WalkAmount", agent.velocity.magnitude);
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            if (!isSetFirstCustomer && target == npcManager.waitingQueuePositionList[0].position)
            {
                npcManager.ForeFrontCustomer = this.gameObject;
                isSetFirstCustomer = true;
            }

            transform.position = target;
            agent.isStopped = true;
        }
    }

    public void GoToDestination(Vector3 destination)
    {
        agent.isStopped = false;
        target = destination;
        agent.SetDestination(destination);
    }

    public void GoToRoom(Vector3 destination)
    {
        GoToDestination(destination);
    }

}
