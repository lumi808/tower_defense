using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public const string KEY = "GameConfigOption";

    public bool IsEasyGame;

    [Header("Configs")]
    [SerializeField] private GameConfigData _easyConfig;
    [SerializeField] private GameConfigData _hardConfig;

    [Header("References")]
    [SerializeField] private MainBuilding _mainBuildings;
    [SerializeField] private WaveSystem _waveSystem;
    [SerializeField] private ReturnScreen _looseScreen;
    [SerializeField] private ReturnScreen _winScreen;

    public GameConfigData GetCurrentConfig() 
    {
        return IsEasyGame ? _easyConfig : _hardConfig;
    }

    private void Start()
    {
        SceneEventSystem.Instance.GameLoose += OnGameLoose;
        SceneEventSystem.Instance.GameWin += OnGameWin;
        
        IsEasyGame = PlayerPrefs.GetInt(KEY) == 0;

        _mainBuildings.Initialize(GetCurrentConfig().MainBuildingHealth);
        _waveSystem.Initialize(GetCurrentConfig().WaveData);
        ResourceSystem.Initialize(GetCurrentConfig().StartBalance);
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