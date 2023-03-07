using UnityEngine;

public class MissleRocket : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _explodeDistance;
    [SerializeField] private GameObject _explodeFx;

    private BaseEnemy _enemy;
    private float _damage;
    private bool _initialized;

    public void Initialize(BaseEnemy enemy, Vector3 position, float damage)
    {
        _enemy = enemy;
        _damage = damage;
        _initialized = true;

        transform.position = position;
    }

    private void Update()
    {
        if (!_initialized)
            return;

        if (!_enemy)
        {
            Explode();
            return;
        }

        Vector3 toTarget = _enemy.transform.position - transform.position;
        Vector3 nextPosition = transform.position + toTarget.normalized * (_speed * Time.deltaTime);

        transform.right = toTarget;

        float toTargetDistance = toTarget.magnitude;
        if (toTargetDistance < _explodeDistance)
        {
            Explode();
            return;
        }

        transform.position = nextPosition;
    }

    private void Explode()
    {
        if (!_enemy)
        {
            Destroy(Instantiate(_explodeFx, transform.position, Quaternion.identity), 2f);
            Destroy(gameObject);
            return;
        }

        Destroy(Instantiate(_explodeFx, transform.position, Quaternion.identity), 2f);
        _enemy.GetDamage(_damage);
        Destroy(gameObject);
    }
}