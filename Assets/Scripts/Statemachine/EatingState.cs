using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingState : State
{
    public LeavingState leavingState;
    [SerializeField] private float eatingTime;
    [SerializeField] private GameObject plate;
    public override void EnterState()
    {
        Debug.Log("eating!");
    }

    public override void ExitState()
    {
        Debug.Log("done eating!");
        plate.SetActive(false);
    }

    public override State UpdateState()
    {
        if ((eatingTime -= Time.deltaTime) <= 0)
            return leavingState;
        else
            return this;
    }


}
