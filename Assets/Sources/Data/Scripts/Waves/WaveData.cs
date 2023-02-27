using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "Data/Wave Data")]
public class WaveData : ScriptableObject
{
    public List<WaveEnemyData> Enemies;

    [System.Serializable]
    public class WaveEnemyData
    {
        public int Count;
        public EnemyData EnemyData;
    }
}
