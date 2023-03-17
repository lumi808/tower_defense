using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleTower : BaseTower, IUpgradable
{
    [SerializeField] private GameObject _missleRocketPrefab;
    private float _nextAttackTime;

    public void Upgrade()
    {
        Level++;
        UpdateStats();
        ChangeLevelModel();
    }

    private void Update()
    {
        if (AvailableEnemies.Count == 0 || !IsActive)
            return;

        BaseEnemy target = AvailableEnemies[0];
        if (target == null)
        {
            AvailableEnemies.RemoveAt(0);
            return;
        }

        Vector3 toEnemyVector = target.transform.position - transform.position;
        toEnemyVector.y = 0;
        _rotateElement.right = toEnemyVector;

        Attack(target);
    }

    private void Attack(BaseEnemy target)
    {
        if (Time.time < _nextAttackTime)
            return;

        MissleAttackPoint[] points = _levelObjects[Level].GetComponentsInChildren<MissleAttackPoint>();
        foreach (MissleAttackPoint point in points)
        {
            MissleRocket rocket = Instantiate(_missleRocketPrefab).GetComponent<MissleRocket>();
            if (rocket)
            {
                rocket.Initialize(target, point.transform.position, BaseDamage);
            }
        }

        _nextAttackTime = Time.time + 1f / AttackRate;
    }
}