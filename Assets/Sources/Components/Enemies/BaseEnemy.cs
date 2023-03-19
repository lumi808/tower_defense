using System;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float ResourceReward { get; private set; }

    protected string Name;
    protected float Speed;
    protected float Health;
    protected float Damage;

    protected NavMeshAgent _navMesh;
    protected float _attackRate;
    protected MainBuilding _mainBuilding;
    protected bool _isAttacking = false;

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
        _isAttacking = true;
    }

    public void Initialize(EnemyData enemyData, MainBuilding mainBuilding, float health)
    {
        Speed = enemyData.Speed;
        Health = health;
        Damage = enemyData.Damage;
        Name = enemyData.Name;
        ResourceReward = enemyData.ResourceReward;
        _attackRate = enemyData.AttackRate;
        _mainBuilding = mainBuilding;

        _navMesh.SetDestination(mainBuilding.transform.position);
        _enemyState = EnemyState.Move;

        _healthBar.Initialize(0, enemyData.Health);
        _healthBar.SetValue(Health);
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        _healthBar.SetValue(Health);

        if (Health <= 0)
        {
            Death(true);
        }
    }

    public EnemySaveInfo GetSaveInfo()
    {
        EnemySaveInfo saveInfo = new EnemySaveInfo();
        saveInfo.Health = Health;
        saveInfo.Name = Name;
        saveInfo.Position = transform.position;

        return saveInfo;
    }

    protected void Death(bool giveReward)
    {
        _isAttacking = false;
        SceneEventSystem.Instance.NotifyEnemyDied(this, giveReward);
        Destroy(gameObject);
    }
}