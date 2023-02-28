using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTower : BaseTower, IUpgradable
{
    public void Upgrade()
    {
        Level++;
        UpgradeStats();
        ChangeLevelModel();
    }

    private void Update()
    {
        if (AvailabeEnemies.Count == 0)
        {
            return;
        }

        BaseEnemy target = AvailabeEnemies[0];

        Vector3 toEnemyVector = target.transform.position - transform.position;

        _rotateElement.right = toEnemyVector;
    }
}
