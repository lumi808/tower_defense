using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerTower : BaseTower, IUpgradable
{
    public void Upgrade()
    {
        Level++;
        UpgradeStats();
        ChangeLevelModel();
    }
}
