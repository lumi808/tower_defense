using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private float _timer;
    [SerializeField] private List<WaveData> _waveDataList;
    [SerializeField] private SpawnEnemySystem _spawnEnemySystem;

    private void Start()
    {
        StartCoroutine(WaveCycle());
    }

    private IEnumerator WaveCycle()
    {
        foreach (WaveData wave in _waveDataList)
        {
            yield return TimerCycle(_timeBetweenWaves);
            _spawnEnemySystem.SpawnWaveUnit(wave, 0.15f);
            yield return WaitForAllEnemiesDie();
        }
    }

    private IEnumerator TimerCycle(float time)
    {
        float timer = Time.time;
        float endTime = timer + time;
        _timer = time;

        WaitForSeconds waitOneSecond = new WaitForSeconds(1f);

        while (Time.time < endTime)
        {
            yield return waitOneSecond;
            _timer--;
            // обновлять ui таймера
        }
    }

    private IEnumerator WaitForAllEnemiesDie()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        while (_spawnEnemySystem.Enemies.Count > 0)
        {
            yield return waitForEndOfFrame;
        }
    }
}