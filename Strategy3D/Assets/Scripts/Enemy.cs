using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    WalkToBuilding,
    WalkToUnit,
    Attack
}


public class Enemy : MonoBehaviour
{
    public EnemyState CurrentEnemyState;

    public int Health;
    public Building TargetBuilding;
    public Unit TargetUnit;
    public float DistanceToFollow = 7;
    public float DistanceToAttack = 1;

    public NavMeshAgent NavMeshAgent;


    private void Start()
    {
        SetState(EnemyState.WalkToBuilding);
    }


    private void Update()
    {
        if (CurrentEnemyState == EnemyState.Idle)
        {

        }
        else if (CurrentEnemyState == EnemyState.WalkToBuilding)
        {

        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {

        }
        else if (CurrentEnemyState == EnemyState.Attack)
        {

        }
    }


    public void SetState(EnemyState enemyState)
    {
        CurrentEnemyState = enemyState;

        if (CurrentEnemyState == EnemyState.Idle)
        {

        }
        else if (CurrentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestBuilding();
            NavMeshAgent.SetDestination(TargetBuilding.transform.position);
        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {

        }
        else if (CurrentEnemyState == EnemyState.Attack)
        {

        }
    }


    public void FindClosestBuilding()
    {
        Building[] allBuildings = FindObjectsOfType<Building>();

        float minDistance = Mathf.Infinity;
        Building closestBuilding = null;

        foreach (var building in allBuildings)
        {
            float distance = Vector3.Distance(transform.position, building.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestBuilding = building;
            }
        }

        TargetBuilding = closestBuilding;
    }
}
