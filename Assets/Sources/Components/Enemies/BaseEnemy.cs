using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float Speed;
    public float Health;
    public float Damage;

    protected NavMeshAgent _navMesh;
    protected float _attackRate;

    [SerializeField] protected EnemyState _enemyState;

    public enum EnemyState
    {
        Move,
        Attack
    }

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }

    public void Initialize(EnemyData enemyData, Vector3 destination)
    {
        Speed = enemyData.Speed;
        Health = enemyData.Health;
        Damage = enemyData.Damage;
        _attackRate = enemyData.AttackRate;

        _navMesh.SetDestination(destination);
        _enemyState = EnemyState.Move;
    }
}