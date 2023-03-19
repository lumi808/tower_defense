using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public int CurrentWaveIndex;

    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private float _timer;
    [SerializeField] private List<WaveData> _waveDataList;
    [SerializeField] private SpawnEnemySystem _spawnEnemySystem;

    private Coroutine _waveCorotine;

    private void Start()
    {
        SceneEventSystem.Instance.GameWin += OnWinOrLoose;
        SceneEventSystem.Instance.GameLoose += OnWinOrLoose;
    }

    private void OnWinOrLoose()
    {
        SceneEventSystem.Instance.GameWin -= OnWinOrLoose;
        SceneEventSystem.Instance.GameLoose -= OnWinOrLoose;

        if (_waveCorotine != null)
        {
            StopCoroutine(_waveCorotine);
        }
    }

    public void Initialize(List<WaveData> waveData, int waveIndex)
    {
        _waveDataList = waveData;
        _waveCorotine = StartCoroutine(WaveCycle(waveIndex));
    }

    private IEnumerator WaveCycle(int waveIndex)
    {
        CurrentWaveIndex = waveIndex;

        for (; CurrentWaveIndex < _waveDataList.Count; CurrentWaveIndex++)
        {
            WaveData wave = _waveDataList[CurrentWaveIndex];
            yield return TimerCycle(_timeBetweenWaves);
            _spawnEnemySystem.SpawnWaveUnit(wave, 0.15f);
            yield return WaitForAllEnemiesDie();
        }

        SceneEventSystem.Instance.NotifyGameWin();
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
        }
    }

    private IEnumerator WaitForAllEnemiesDie()
    {
        yield return new WaitForSeconds(15);
        yield break;
    }
}