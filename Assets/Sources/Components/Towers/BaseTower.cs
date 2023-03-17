using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    protected string Name;
    protected float BaseDamage;
    protected float SellPrice;
    protected float BuildPrice;
    protected float AttackRate;
    protected float Radius;

    protected int Level;
    protected TowerData TowerData;
    protected bool IsActive;

    [Header("Level Objects")]
    [SerializeField] protected GameObject[] _levelObjects;
    [SerializeField] protected Transform _rotateElement;

    protected List<BaseEnemy> Enemies;
    protected List<BaseEnemy> AvailableEnemies;
    protected int CellId;

    public void Initialze(TowerData towerData, List<BaseEnemy> enemies, int cellId)
    {
        Level = 0;
        TowerData = towerData;
        Enemies = enemies;
        CellId = cellId;
        AvailableEnemies = new List<BaseEnemy>();
        UpdateStats();
    }

    public void SetTowerActive(bool isActive) => IsActive = isActive;

    private void FixedUpdate()
    {
        if (!IsActive)
            return;

        FindEnemies();
    }

    protected void FindEnemies()
    {
        AvailableEnemies.Clear();
        for (int i = 0; i < Enemies.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, Enemies[i].transform.position);
            if (distance < Radius)
            {
                AvailableEnemies.Add(Enemies[i]);
            }
        }
    }

    protected void UpdateStats()
    {
        TowerData.TowerLevelInfo levelInfo = TowerData.UpdageInfo[Level];
        Name = levelInfo.Name;
        BaseDamage = levelInfo.BaseDamage;
        SellPrice = levelInfo.SellPrice;
        AttackRate = levelInfo.AttackRate;
        BuildPrice = levelInfo.UpgradePrice;
        Radius = levelInfo.Radius;
    }

    protected void ChangeLevelModel()
    {
        for (int i = 0; i < _levelObjects.Length; i++)
        {
            bool isRightLevel = i == Level;
            _levelObjects[i].SetActive(isRightLevel);
        }
    }

    public float GetUpgradePrice()
    {
        TowerData.TowerLevelInfo levelInfo = TowerData.UpdageInfo[Level];
        return levelInfo.UpgradePrice;
    }

    public bool HasUpgrade()
    {
        return Level + 1 < TowerData.UpdageInfo.Count;
    }

    public TowerSaveInfo GetSaveInfo()
    {
        TowerSaveInfo info = new TowerSaveInfo();
        info.Level = Level;
        info.TowerType = TowerData.TypeOfTower;
        info.CellId = CellId;

        return info;
    }
}