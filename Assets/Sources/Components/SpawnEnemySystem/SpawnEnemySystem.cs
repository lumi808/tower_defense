using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemySystem : MonoBehaviour
{
    public List<BaseEnemy> Enemies { get; private set; }

    [SerializeField] private Transform _firstSpawnPosition;
    [SerializeField] private Transform _secondSpawnPosition;
    [SerializeField] private MainBuilding _mainBuilding;

    [SerializeField] private EnemyLibrary _enemyLibrary;

    private void Awake()
    {
        Enemies = new List<BaseEnemy>();
    }

    private void Start()
    {
        SceneEventSystem.Instance.EnemyDied += OnEnemyDied;
        //SpawnWaveUnit(_testWave, 0.5f);
    }

    private void OnDestroy()
    {
        SceneEventSystem.Instance.EnemyDied -= OnEnemyDied;
    }

    private void OnEnemyDied(BaseEnemy enemy, bool giveReward)
    {
        if (Enemies.Contains(enemy))
        {
            Enemies.Remove(enemy);
        }
    }

    public List<EnemySaveInfo> GetSaveInfo()
    {
        List<EnemySaveInfo> saveInfo = new List<EnemySaveInfo>();

        foreach (BaseEnemy enemy in Enemies)
        {
            saveInfo.Add(enemy.GetSaveInfo());
        }

        return saveInfo;
    }

    public void LoadEnemis(List<EnemySaveInfo> savedEnemies)
    {
        Dictionary<string, EnemyData> _enemiesMap = new Dictionary<string, EnemyData>();

        foreach (EnemyData enemyData in _enemyLibrary.Enemies)
        {
            _enemiesMap.Add(enemyData.Name, enemyData);
        }

        foreach (EnemySaveInfo savedEnemy in savedEnemies)
        {
            GameObject enemyPrefab = _enemiesMap[savedEnemy.Name].Prefab;
            GameObject enemy = Instantiate(enemyPrefab, savedEnemy.Position, Quaternion.identity);
            BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();

            baseEnemy.Initialize(_enemiesMap[savedEnemy.Name], _mainBuilding, savedEnemy.Health);
            Enemies.Add(baseEnemy);
        }
    }

    public void SpawnWaveUnit(WaveData waveData, float tickTime)
    {
        StartCoroutine(SpawnEnemiesInTime(waveData, tickTime));
    }

    private IEnumerator SpawnEnemiesInTime(WaveData waveData, float tickTime)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(tickTime);
        Vector3[] points = new Vector3[2];
        points[0] = _firstSpawnPosition.position;
        points[1] = _secondSpawnPosition.position;

        foreach (WaveData.WaveEnemyData waveEnemyData in waveData.Enemies)
        {
            GameObject enemyPrefab = waveEnemyData.EnemyData.Prefab;
            for (int i = 0; i < waveEnemyData.Count; i++)
            {
                int index = i % 2;
                GameObject enemy = Instantiate(enemyPrefab, points[index], Quaternion.identity);
                BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();

                baseEnemy.Initialize(waveEnemyData.EnemyData, _mainBuilding, waveEnemyData.EnemyData.Health);
                Enemies.Add(baseEnemy);

                yield return waitForSeconds;
            }
        }
    }
}