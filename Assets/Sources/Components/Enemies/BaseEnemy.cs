using System;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float ResourceReward { get; private set; }

    protected float Speed;
    protected float Health;
    protected float Damage;

    protected NavMeshAgent _navMesh;
    protected float _attackRate;
    protected MainBuilding _mainBuilding;

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

    public void Initialize(EnemyData enemyData, MainBuilding mainBuilding)
    {
        Speed = enemyData.Speed;
        Health = enemyData.Health;
        Damage = enemyData.Damage;
        ResourceReward = enemyData.ResourceReward;
        _attackRate = enemyData.AttackRate;
        _mainBuilding = mainBuilding;

        _navMesh.SetDestination(mainBuilding.transform.position);
        _enemyState = EnemyState.Move;

        _healthBar.Initialize(0, Health);
        _healthBar.SetValue(Health);
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        _healthBar.SetValue(Health);

        if (Health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        SceneEventSystem.Instance.NotifyEnemyDied(this);
        Destroy(gameObject);
    }
}