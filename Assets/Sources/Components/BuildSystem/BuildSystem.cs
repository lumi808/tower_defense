using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private TowerLibrary _towerLibrary;
    [SerializeField] private SelectionSystem _selectionSystem;
    [SerializeField] private TowerUpgradeSystem _upgradeSystem;
    [SerializeField] private SpawnEnemySystem _spawnEnemySystem;

    private Dictionary<TowerData.TowerType, TowerData> _towerDataMap;
    private Dictionary<int, TowerCell> _towerCells;
    private List<BaseTower> _towers;

    private void Awake()
    {
        _towerDataMap = new Dictionary<TowerData.TowerType, TowerData>();
        _towerCells = new Dictionary<int, TowerCell>();
        _towers = new List<BaseTower>();

        TowerCell[] cellInScene = FindObjectsOfType<TowerCell>();

        foreach(TowerCell towerCell in cellInScene)
        {
            _towerCells.Add(towerCell.Id, towerCell);
        }

        foreach (TowerData towerData in _towerLibrary.Library)
        {
            _towerDataMap.Add(towerData.TypeOfTower, towerData);
        }
    }
    
    public List<TowerSaveInfo> GetSaveInfo()
    {
        List<TowerSaveInfo> saveInfo = new List<TowerSaveInfo>();
        foreach (BaseTower tower in _towers)
        {
            saveInfo.Add(tower.GetSaveInfo());
        }
    
        return saveInfo;
    }

    public void BuildTower(TowerData.TowerType towerType)
    {
        IMouseInteractable currentSelected = _selectionSystem.CurrentSelected;

        if (currentSelected is TowerCell cell)
        {
            if (cell.IsCellUsed())
            {
                return;
            }

            GameObject prefab = _towerDataMap[towerType].TowerPrefab;
            Vector3 position = cell.transform.position;
            GameObject tower = Instantiate(prefab, position, Quaternion.identity);
            BaseTower baseTower = tower.GetComponent<BaseTower>();

            if (baseTower != null)
            {
                baseTower.Initialze(_towerDataMap[towerType], _spawnEnemySystem.Enemies, cell.Id);
            }

            StartCoroutine(BuildTowerInTime(tower, 5f, baseTower));

            _upgradeSystem.RegisterTower(currentSelected, baseTower);
            cell.UseCell();
            _towers.Add(baseTower);
            ResourceSystem.SpendMoney(baseTower.GetUpgradePrice());
        }
    }

    public void LoadTowers(List<TowerSaveInfo> saveTowers)
    {
        foreach(TowerSaveInfo towerSave in saveTowers)
        {
            TowerCell cell = _towerCells[towerSave.CellId];
            TowerData.TowerType towerType = towerSave.TowerType;

            GameObject prefab = _towerDataMap[towerType].TowerPrefab;
            Vector3 position = cell.transform.position;
            GameObject tower = Instantiate(prefab, position, Quaternion.identity);
            BaseTower baseTower = tower.GetComponent<BaseTower>();

            if (baseTower != null)
            {
                baseTower.Initialze(_towerDataMap[towerType], _spawnEnemySystem.Enemies, cell.Id);
            }

            baseTower.SetTowerActive(true);

            _upgradeSystem.RegisterTower(cell, baseTower);
            cell.UseCell();
            _towers.Add(baseTower);
        }
    }

    private IEnumerator BuildTowerInTime(GameObject tower, float duration, BaseTower baseTower)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        float startSize = 0.05f;
        float endSize = tower.transform.localScale.x;

        while (Time.time < endTime)
        {
            float lerp = (Time.time - startTime) / duration;
            float size = Mathf.Lerp(startSize, endSize, lerp);

            tower.transform.localScale = Vector3.one * size;

            yield return new WaitForEndOfFrame();
        }

        tower.transform.localScale = Vector3.one * endSize;
        baseTower.SetTowerActive(true);
    }
}