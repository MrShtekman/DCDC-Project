using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerNavMesh : MonoBehaviour
{
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            Destroy(gameObject);

    }

    public void setDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

}

