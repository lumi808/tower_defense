using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    private float _health;

    [SerializeField] private HealthBar _healthBar;

    public void Initialize(float maxHealth)
    {
        _health = maxHealth;
        _healthBar.Initialize(0f, maxHealth);
        _healthBar.SetValue(maxHealth);
    }

    public void GetDamage(float damage)
    {
        _health -= damage;
        _healthBar.SetValue(_health);

        if (_health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
    }
}