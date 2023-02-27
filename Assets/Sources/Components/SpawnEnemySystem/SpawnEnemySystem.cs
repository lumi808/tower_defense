using System.Collections;
using UnityEngine;

public class SpawnEnemySystem : MonoBehaviour
{
    [SerializeField] private Transform _firstSpawnPosition;
    [SerializeField] private Transform _secondSpawnPosition;
    [SerializeField] private Transform _mainBuilding;

    [SerializeField] private WaveData _testWave;

    private void Start()
    {
        SpawnWaveUnit(_testWave, 0.5f);
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

                baseEnemy.Initialize(waveEnemyData.EnemyData, _mainBuilding.position);

                yield return waitForSeconds;
            }
        }
    }
}