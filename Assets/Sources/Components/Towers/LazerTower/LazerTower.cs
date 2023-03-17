using System;
using UnityEngine;

public class LaserTower : BaseTower, IUpgradable
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LineRenderer _lineRenderer;

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