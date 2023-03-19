using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies Library", menuName = "Data/Enemies Library")]
public class EnemyLibrary : ScriptableObject
{
    public List<EnemyData> Enemies;
}
