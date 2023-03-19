using UnityEngine;

public class Zombie : BaseEnemy
{
    private ZombieAnimation _animationController;

    private void Awake()
    {
        _animationController.GetComponent<ZombieAnimation>();
    }

    private void Update()
    {
        if (!_isAttacking)
            return;

        if (_enemyState == EnemyState.Move)
        {
            if (!_navMesh.hasPath)
                return;

            _animationController.Walking();

            if (_navMesh.remainingDistance < 5)
            {
                _navMesh.isStopped = true;
                _enemyState = EnemyState.Attack;
            }

        }
        else
        {
            _animationController.Attack();
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
