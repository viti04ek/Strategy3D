using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public NavMeshAgent NavMeshAgent;
    public int Price;


    public override void WhenClickOnGround(Vector3 point)
    {
        NavMeshAgent.SetDestination(point);
    }
}
