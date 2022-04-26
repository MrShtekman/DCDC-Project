using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerNavMesh : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform desti;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            Destroy(gameObject);

    }

    public void setDestination(Transform destination)
    {
        desti = destination;
        agent.SetDestination(destination.position);
        
    }

}

