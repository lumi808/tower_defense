using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTower : BaseTower, IUpgradable
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LineRenderer _lineRenderer;

    public void Upgrade()
    {
        Level++;
        UpgradeStats();
        ChangeLevelModel();
    }

    private void Update()
    {
        if(AvailabeEnemies.Count == 0 || !IsActive)
        {
            return;
        }

        BaseEnemy target = AvailabeEnemies[0];
        if(target == null)
        {
            AvailabeEnemies.RemoveAt(0);
            return;
        }

        Vector3 localPoint = _attackPoint.transform.InverseTransformPoint(target.transform.position);
        _lineRenderer.SetPosition(1, localPoint);

        Vector3 toEnemyVector = target.transform.position - transform.position;
        toEnemyVector.y = 0;
        _rotateElement.right = toEnemyVector;

        Attack(target);
    }

    private void Attack(BaseEnemy target)
    {
        target.GetDamage(BaseDamage * Time.deltaTime);
    }
}