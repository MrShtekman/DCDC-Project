using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : State
{
    public FindingTableState findingTableState;
    [SerializeField] private float waitingTime;
    public override void EnterState()
    {
        Debug.Log("Waiting!");
    }

    public override void ExitState()
    {
        Debug.Log("Got food!");
    }

    public override State UpdateState()
    {
        if ((waitingTime -= Time.deltaTime) <= 0)
            return findingTableState;
        else
            return this;
    }
}
