using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

    private void Start()
    {
        SceneEventSystem.Instance.BalanceChanged += OnBalancceChanged;
    }

    private void OnDestroy()
    {
        SceneEventSystem.Instance.BalanceChanged -= OnBalancceChanged;
    }

    private void OnBalancceChanged(float currentBalance)
    {
        _moneyText.text = currentBalance.ToString();
    }
}