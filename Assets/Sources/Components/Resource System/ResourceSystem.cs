using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    private static ResourceSystem _instance;

    public float Balance
    {
        get => _balance;
        private set
        {
            _balance = value;
            SceneEventSystem.Instance.NotifyBalanceChanged(_instance.Balance);
        }
    }

    private float _balance;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            Debug.LogError("Respurce system alread exists");
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        SceneEventSystem.Instance.EnemyDied += OnEnemyDied;
    }

    private void OnDestroy()
    {
        SceneEventSystem.Instance.EnemyDied -= OnEnemyDied;
    }

    private void OnEnemyDied(BaseEnemy enemy, bool giveReward)
    {
        if (giveReward)
            Balance += enemy.ResourceReward;
    }

    public static void Initialize(float startBalance)
    {
        _instance.Balance = startBalance;
    }

    public static bool HasEnoughMoney(float count)
    {
        return _instance.Balance >= count;
    }

    public static void SpendMoney(float count)
    {
        if (!HasEnoughMoney(count))
        {
            Debug.LogError("Try spend more money then contains");
            return;
        }

        _instance.Balance -= count;

        SceneEventSystem.Instance.NotifyBalanceChanged(_instance.Balance);
    }
}