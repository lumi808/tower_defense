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
    [SerializeField] private GameObject[] _levelObjects;
    [SerializeField] protected Transform _rotateElement;

    protected List<BaseEnemy> Enemies;
    protected List<BaseEnemy> AvailabeEnemies;

    public void Initialze(TowerData towerData, List<BaseEnemy> enemies)
    {
        Level = 0;
        TowerData = towerData;
        Enemies = enemies;
        AvailabeEnemies = new List<BaseEnemy>();
        UpgradeStats();
    }

    public void SetTowerActive(bool isActive)
    {
        IsActive = isActive;
    }

    private void FixedUpdate()
    {
        if (!IsActive)
        {
            return;
        }

        FindEnemies();
    }

    protected void FindEnemies()
    {
        AvailabeEnemies.Clear();
        for(int i = 0; i < Enemies.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, Enemies[i].transform.position);
            if(distance < Radius)
            {
                AvailabeEnemies.Add(Enemies[i]);
            }
        } 
    }

    protected void UpgradeStats()
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
            _levelObjects[i].SetActive(i == Level);
        }
    }
}