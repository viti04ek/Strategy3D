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


    void Update()
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

        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {

        }
        else if (CurrentEnemyState == EnemyState.Attack)
        {

        }
    }
}
