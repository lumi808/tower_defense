using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    protected float Speed;
    protected float Health;
    protected float Damage;

    protected NavMeshAgent _navMesh;
    protected float _attackRate;

    [SerializeField] protected EnemyState _enemyState;
    private HealthBar _healthBar;

    public enum EnemyState
    {
        Move,
        Attack
    }

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public void Initialize(EnemyData enemyData, Vector3 destination)
    {
        Speed = enemyData.Speed;
        Health = enemyData.Health;
        Damage = enemyData.Damage;
        _attackRate = enemyData.AttackRate;

        _navMesh.SetDestination(destination);
        _enemyState = EnemyState.Move;

        _healthBar.Initialize(0, Health);
        _healthBar.SetValue(Health);
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        _healthBar.SetValue(Health);

        if(Health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}