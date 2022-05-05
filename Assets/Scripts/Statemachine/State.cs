using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void EnterState();

    public abstract State UpdateState();

    public abstract void ExitState();
}
