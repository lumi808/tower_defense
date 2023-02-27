using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : BaseEnemy
{
    private float _nextTimeAttack;

    private void Update()
    {
        if (_enemyState == EnemyState.Move)
        {
            if (_navMesh.remainingDistance < 5)
            {
                _navMesh.isStopped = true;
                _enemyState = EnemyState.Attack;
            }
        }
        else
        {
            if (Time.time >= _nextTimeAttack)
            {
                Attack();
                _nextTimeAttack = Time.time + 1 / _attackRate;
            }
        }
    }

    private void Attack()
    {
        Debug.Log("attack");
    }
}