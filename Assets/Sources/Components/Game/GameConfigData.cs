using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameConfig", menuName = "Data/GameConfig")]
public class GameConfigData : ScriptableObject
{
    public string Name;
    public List<WaveData> WaveData;
    public float StartBalance;
    public float MainBuildingHealth;
}
