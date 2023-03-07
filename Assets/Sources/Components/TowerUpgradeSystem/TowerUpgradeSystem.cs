using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeSystem : MonoBehaviour
{
    [SerializeField] private SelectionSystem _selectionSystem;

    private Dictionary<IMouseInteractable, IUpgradable> _upgradableMap;

    private void Start()
    {
        SceneEventSystem.Instance.OnUpgradeButtonPressed += onUpgradeButtonPressed;
    }

    private void onUpgradeButtonPressed()
    {
        IMouseInteractable currentSelected = _selectionSystem.CurrentSelected;
        if (_upgradableMap.ContainsKey(currentSelected))
        {
            _upgradableMap[currentSelected].Upgrade();
        }
        SceneEventSystem.Instance.NotifyBalanceChanged(_upgradableMap[currentSelected].GetUpgradePrice());
    }

    public void RegisterTower(IMouseInteractable interactable, BaseTower tower)
    {
        if (_upgradableMap == null)
        {
            _upgradableMap = new Dictionary<IMouseInteractable, IUpgradable>();
        }

        if (tower is IUpgradable upgradable)
        {
            _upgradableMap.Add(interactable, upgradable);
        }
    }

    public float GetSelectedUpgradePrice()
    {
        IMouseInteractable currentSelected = _selectionSystem.CurrentSelected;
        if (_upgradableMap.ContainsKey(currentSelected))
        {
            return _upgradableMap[currentSelected].GetUpgradePrice();
        }

        return 0f;
    }
}
