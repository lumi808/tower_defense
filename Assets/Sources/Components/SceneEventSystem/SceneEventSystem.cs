using UnityEngine;
using System;

public class SceneEventSystem : MonoBehaviour
{
    public static SceneEventSystem Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public delegate void CellEvent(TowerCell towerCell);

    public event CellEvent CellSelected;
    public event CellEvent CellDeselected;
    public event CellEvent CellUsed;

    public event Action OnUpgradeButtonPressed;

    public void NotifyUpgradeButtonPressed()
    {
        OnUpgradeButtonPressed?.Invoke();
    }

    public void NotifyCellSelected(TowerCell towerCell)
    {
        CellSelected?.Invoke(towerCell);
    }

    public void NotifyCellDeselected(TowerCell towerCell)
    {
        CellDeselected?.Invoke(towerCell);
    }

    public void NotifyCellUsed(TowerCell towerCell)
    {
        CellUsed?.Invoke(towerCell);
    }
}