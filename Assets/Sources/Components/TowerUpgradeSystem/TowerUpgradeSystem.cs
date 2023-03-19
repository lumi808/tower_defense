using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeSystem : MonoBehaviour
{
    [SerializeField] private SelectionSystem _selectionSystem;

    private Dictionary<IMouseInteractable, IUpgradable> _upgradableMap;

    private void Start()
    {
        SceneEventSystem.Instance.OnUpgradeButtonPressed += OnUpgradeButtonPressed;
    }

    public void RegisterTower(IMouseInteractable interactable, BaseTower tower)
    {
        if (_upgradableMap == null)
            _upgradableMap = new Dictionary<IMouseInteractable, IUpgradable>();

        if (tower is IUpgradable upgradable)
        {
            _upgradableMap.Add(interactable, upgradable);
        }
    }

    public bool SelectedHasUpgrade()
    {
        IMouseInteractable currentSelected = _selectionSystem.CurrentSelected;
        if (currentSelected == null)
            return false;

        return _upgradableMap.ContainsKey(currentSelected) && _upgradableMap[currentSelected].HasUpgrade();
    }

    public float GetSelectedUpgradePrice()
    {
        IMouseInteractable currentSelected = _selectionSystem.CurrentSelected;
        if (currentSelected == null)
            return 0;

        if (_upgradableMap.ContainsKey(currentSelected))
        {
            return _upgradableMap[currentSelected].GetUpgradePrice();
        }

        return 0f;
    }

    private void OnUpgradeButtonPressed()
    {
        IMouseInteractable currentSelected = _selectionSystem.CurrentSelected;
        if (_upgradableMap.ContainsKey(currentSelected))
        {
            _upgradableMap[currentSelected].Upgrade();
            SceneEventSystem.Instance.NotifyBalanceChanged(_upgradableMap[currentSelected].GetUpgradePrice());
        }
    }
}
