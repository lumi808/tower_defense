using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public const string KEY = "GameConfigOption";

    [Header("Configs")]
    [SerializeField] private GameConfigData _easyConfig;
    [SerializeField] private GameConfigData _hardConfig;

    [Header("References")]
    [SerializeField] private MainBuilding _mainBuildings;
    [SerializeField] private WaveSystem _waveSystem;
    [SerializeField] private ReturnScreen _looseScreen;
    [SerializeField] private ReturnScreen _winScreen;

    private void Start()
    {
        SceneEventSystem.Instance.GameLoose += OnGameLoose;
        SceneEventSystem.Instance.GameWin += OnGameWin;
        
        bool isEasyConfig = PlayerPrefs.GetInt(KEY) == 0;

        _mainBuildings.Initialize(isEasyConfig ? _easyConfig.MainBuildingHealth : _hardConfig.MainBuildingHealth);
        _waveSystem.Initialize(isEasyConfig ? _easyConfig.WaveData : _hardConfig.WaveData);
        ResourceSystem.Initialize(isEasyConfig ? _easyConfig.StartBalance : _hardConfig.StartBalance);
    }

    private void OnGameWin()
    {
        _winScreen.Show();
    }

    private void OnGameLoose()
    {
        _looseScreen.Show();
    }
}