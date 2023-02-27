using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Library", menuName = "Data/Tower Library")]
public class TowerLibrary : ScriptableObject
{
    public List<TowerData> Library;
}
