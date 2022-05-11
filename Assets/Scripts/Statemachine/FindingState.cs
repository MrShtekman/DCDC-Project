using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindingState : State
{
    public WaitingState waitingState;
    private NavMeshAgent agent;
    //[SerializeField] private Transform destination;
    [SerializeField] private Restaurant restaurant;
    [SerializeField] private List<Transform> waitingPoints;
    [SerializeField] private WaitingPoint waitingPoint;
    public override void EnterState()
    {

        agent = transform.parent.parent.GetComponent<NavMeshAgent>();
        //agent.SetDestination(destination.position);
        List<Transform> waitingPoints = restaurant.waitingPoints;
        WaitingInLine();
    }

    public override void ExitState()
    {

    }

    public override State UpdateState()
    {
        if (agent.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(agent.velocity);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return waitingState;
        }
        else
            return this;
    }

    public void WaitingInLine()
    {
        for(int i = 0; i < waitingPoints.Count; i++)
        {
            WaitingPoint nextWaitingPoint = waitingPoints[i].GetComponent<WaitingPoint>();
            if (!nextWaitingPoint.taken)
            {
                nextWaitingPoint.taken = true;
                agent.SetDestination(waitingPoints[i].transform.position);
                waitingPoint.taken = false;
                waitingPoint = nextWaitingPoint;
            }
        }
    }

   
}
