using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] protected string Name;
    [SerializeField] protected float BaseDamage;
    [SerializeField] protected float SellPrice;
    [SerializeField] protected float BuildPrice;
    [SerializeField] protected float AttackRate;

    [SerializeField] protected int Level;
    [SerializeField] protected TowerData TowerData;

    [Header("Level Objects")]
    [SerializeField] private GameObject[] _levelObjects;

    public void Initialze(TowerData towerData)
    {
        Level = 0;
        TowerData = towerData;
        UpgradeStats();
    }

    protected void FindEnemies()
    {
    }

    protected void UpgradeStats()
    {
        TowerData.TowerLevelInfo levelInfo = TowerData.UpdageInfo[Level];
        Name = levelInfo.Name;
        BaseDamage = levelInfo.BaseDamage;
        SellPrice = levelInfo.SellPrice;
        AttackRate = levelInfo.AttackRate;
        BuildPrice = levelInfo.UpgradePrice;
    }

    protected void ChangeLevelModel()
    {
        for (int i = 0; i < _levelObjects.Length; i++)
        {
            _levelObjects[i].SetActive(i == Level);
        }
    }
}
