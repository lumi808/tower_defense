using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string Name;
    public float Speed;
    public float Health;
    public float Damage;
    public float AttackRate;
    public GameObject Prefab;
}
