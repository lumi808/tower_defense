using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] private BuildSystem _buildSystem;
    [SerializeField] private SpawnEnemySystem _spawnEnemySystem;
    [SerializeField] private MainBuilding _mainBuilding;
    [SerializeField] private Game _game;
    [SerializeField] private WaveSystem _waveSystem;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            SaveGame();
    }

    public void SaveGame()
    {
        GameSaveInfo saveInfo = new GameSaveInfo();

        saveInfo.Towers = _buildSystem.GetSaveInfo();
        saveInfo.Enemies = _spawnEnemySystem.GetSaveInfo();
        saveInfo.MainBuildingHealth = _mainBuilding.Health;
        saveInfo.Balance = ResourceSystem.GameBalance;
        saveInfo.IsEasyGame = _game.IsEasyGame;

        if (!Directory.Exists(GetSaveDirectory()))
        {
            Directory.CreateDirectory(GetSaveDirectory());
        }

        string serializedData = JsonUtility.ToJson(saveInfo);
        File.WriteAllText(GetSavePath(), serializedData);

        Process.Start(GetSavePath());
    }

    public void LoadGame()
    {

    }

    private string GetSaveDirectory()
    {
        return Path.Combine(Application.dataPath, "SaveTD");
    }

    private string GetSavePath()
    {
        return Path.Combine(GetSaveDirectory(), "save.json");
    }
}

[System.Serializable]
public class TowerSaveInfo
{
    public TowerData.TowerType TowerType;
    public int CellId;
    public int Level; 
}

[System.Serializable]
public class EnemySaveInfo
{
    public float Health;
    public Vector3 Position;
    public string Name;
}

[System.Serializable]
public class GameSaveInfo
{
    public List<TowerSaveInfo> Towers;
    public List<EnemySaveInfo> Enemies;
    public float MainBuildingHealth;
    public float Balance;
    public bool IsEasyGame;
    public int CurrentWaveIndex;
}

