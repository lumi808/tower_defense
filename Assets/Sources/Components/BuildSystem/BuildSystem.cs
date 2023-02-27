using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private TowerLibrary _towerLibrary;
    [SerializeField] private SelectionSystem _selectionSystem;
    [SerializeField] private TowerUpgradeSystem _upgradeSystem;

    private Dictionary<TowerData.TowerType, TowerData> _towerDataMap;

    private void Awake()
    {
        _towerDataMap = new Dictionary<TowerData.TowerType, TowerData>();
        foreach (TowerData towerData in _towerLibrary.Library)
        {
            _towerDataMap.Add(towerData.TypeOfTower, towerData);
        }
    }

    public void BuildTower(TowerData.TowerType towerType)
    {
        IMouseInteractable currentSelected = _selectionSystem.CurrentSelected;

        // если currentSelected это тип TowerCell
        if (currentSelected is TowerCell cell)
        {
            if(cell.IsCellUsed())
            {
                return;
            }

            GameObject prefab = _towerDataMap[towerType].TowerPrefab;
            Vector3 position = cell.transform.position;
            GameObject tower = Instantiate(prefab, position, Quaternion.identity);
            BaseTower baseTower = tower.GetComponent<BaseTower>();

            if(baseTower != null)
            {
                baseTower.Initialze(_towerDataMap[towerType]);
            }

            StartCoroutine(BuildTowerInTime(tower, 5f));

            _upgradeSystem.RegisterTower(currentSelected, baseTower);
            cell.UseCell();
        }
    }

    private IEnumerator WaitForTime(float delay, float time)
    {
        yield return new WaitForSeconds(delay);

        // do actions

        yield return new WaitForSeconds(time);

        // do action
    }

    private IEnumerator BuildTowerInTime(GameObject tower, float duration)
    {
        // увеличивать башню до необходимого размера в течени duration времени

        // 1. Определяем время окончания "анимация"
        // 2. Каждый кадр выставляем размер башни

        float startTime = Time.time;
        float endTime = startTime + duration;

        float startSize = 0.05f;
        float endSize = tower.transform.localScale.x;

        while(Time.time < endTime)
        {
            // 0..1
            float lerp = (Time.time - startTime) / duration;
            float size = Mathf.Lerp(startSize, endSize, lerp);

            tower.transform.localScale = Vector3.one * size;

            yield return new WaitForEndOfFrame();
        }

        tower.transform.localScale = Vector3.one * endSize;
    }
}