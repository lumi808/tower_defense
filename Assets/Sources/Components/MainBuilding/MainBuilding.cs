using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    public float Health => _health;

    [SerializeField] private float _health;
    [SerializeField] private HealthBar _healthBar;


    public void Initialize(float health, float maxHealth)
    {
        _health = health;
        _healthBar.Initialize(0f, maxHealth);
        _healthBar.SetValue(health);
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
        SceneEventSystem.Instance.NotifyGameLoose();
    }
}