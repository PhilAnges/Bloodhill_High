using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICamera : AIController
{

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        currentState = new CamPatrolState(this);
        SetState(currentState);
    }

    void Update()
    {
        currentState.UpdateBehavior();
    }
}
