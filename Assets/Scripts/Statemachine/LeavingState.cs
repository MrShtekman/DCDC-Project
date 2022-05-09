using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeavingState : State
{

    
    private NavMeshAgent agent;
    [SerializeField] private Transform destination;
    public override void EnterState()
    {
        Debug.Log("leaving!");
        agent = transform.parent.parent.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
    }

    public override void ExitState()
    {
        Debug.Log("Bye!");
    }

    public override State UpdateState()
    {
        if (agent.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(agent.velocity);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Destroy(this);
            return this;
        }
        else
            return this;
    }
}
