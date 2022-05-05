using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindingTableState : State
{
    
    public EatingState eatingState;
    private NavMeshAgent agent;
    [SerializeField] private GameObject plate;
    [SerializeField] private Transform destination;
    public override void EnterState()
    {
        Debug.Log("Finding table!");
        agent = transform.parent.parent.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
        plate.SetActive(true);
    }

    public override void ExitState()
    {
        Debug.Log("Found!");
    }

    public override State UpdateState()
    {
        if (agent.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(agent.velocity);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return eatingState;
        }
        else
            return this;
    }

   
}
