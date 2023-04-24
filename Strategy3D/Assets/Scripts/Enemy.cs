using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
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

    public float AttackPeriod = 1f;
    private float _timer;


    private void Start()
    {
        SetState(EnemyState.WalkToBuilding);
    }


    private void Update()
    {
        if (CurrentEnemyState == EnemyState.Idle)
        {
            FindClosestUnit();
        }
        else if (CurrentEnemyState == EnemyState.WalkToBuilding)
        {
            FindClosestUnit();

            if (!TargetBuilding)
                SetState(EnemyState.Idle);
        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {
            if (TargetUnit)
            {
                NavMeshAgent.SetDestination(TargetUnit.transform.position);

                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                if (distance > DistanceToFollow)
                    SetState(EnemyState.WalkToBuilding);

                if (distance < DistanceToAttack)
                    SetState(EnemyState.Attack);
            }
            else
            {
                SetState(EnemyState.WalkToBuilding);
            }
        }
        else if (CurrentEnemyState == EnemyState.Attack)
        {
            if (TargetUnit)
            {
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);

                if (distance > DistanceToAttack)
                    SetState(EnemyState.WalkToUnit);

                _timer += Time.deltaTime;
                if (_timer > AttackPeriod)
                {
                    TargetUnit.TakeDamage(1);
                    _timer = 0;
                }
            }
            else
            {
                SetState(EnemyState.WalkToBuilding);
            }
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
            _timer = 0;
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


    public void FindClosestUnit()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();

        float minDistance = Mathf.Infinity;
        Unit closestUnit = null;

        foreach (var unit in allUnits)
        {
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestUnit = unit;
            }
        }

        if (minDistance < DistanceToFollow)
        {
            TargetUnit = closestUnit;
            SetState(EnemyState.WalkToUnit);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToAttack);

        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, DistanceToFollow);
    }
#endif
}
