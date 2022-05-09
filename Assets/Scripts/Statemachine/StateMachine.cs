using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;

    private void Start()
    {
        SwitchState(currentState);
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.UpdateState();

        if (nextState != currentState)
        {
            currentState.ExitState();
            SwitchState(nextState);
        }
    }

    private void SwitchState(State nextState)
    {
        currentState = nextState;
        currentState.EnterState();
    }

    void Update()
    {
        RunStateMachine();
        
    }


}
