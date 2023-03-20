using UnityEngine;

public class Zombie : BaseEnemy
{
    private float _nextTimeAttack;
    [SerializeField]private ZombieAnimation _zombieAnimation;

    private void Update()
    {
        if (!_isAttacking)
            return;

        if (_enemyState == EnemyState.Move)
        {
            if (!_navMesh.hasPath)
                return;

            if (_navMesh.remainingDistance < 5)
            {
                _navMesh.isStopped = true;
                _enemyState = EnemyState.Attack;
            }
        }
        else
        {
            _zombieAnimation.ZombieAttack();
            Attack();
        }
    }

    private void Attack()
    {
        if (_mainBuilding)
            _mainBuilding.GetDamage(Damage);

        Death(false);
    }
}
