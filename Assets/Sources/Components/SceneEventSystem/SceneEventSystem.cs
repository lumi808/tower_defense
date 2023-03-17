using System;
using UnityEngine;

public class SceneEventSystem : MonoBehaviour
{
    public static SceneEventSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public delegate void CellEvent(TowerCell towerCell);
    public delegate void EnemyDieEvent(BaseEnemy enemy, bool giveReward);

    public event CellEvent CellSelected;
    public event CellEvent CellDeselected;
    public event CellEvent CellUsed;

    public event EnemyDieEvent EnemyDied;

    public event Action OnUpgradeButtonPressed;
    public event Action GameLoose;
    public event Action GameWin;
    public event Action<float> BalanceChanged;

    public void NotifyUpgradeButtonPressed() => OnUpgradeButtonPressed?.Invoke();

    public void NotifyCellSelected(TowerCell towerCell) => CellSelected?.Invoke(towerCell);

    public void NotifyCellDeselected(TowerCell towerCell) => CellDeselected?.Invoke(towerCell);

    public void NotifyCellUsed(TowerCell towerCell) => CellUsed?.Invoke(towerCell);

    public void NotifyEnemyDied(BaseEnemy enemy, bool giveReward) => EnemyDied?.Invoke(enemy, giveReward);

    public void NotifyBalanceChanged(float currentBalance) => BalanceChanged?.Invoke(currentBalance);

    public void NotifyGameLoose() => GameLoose?.Invoke();

    public void NotifyGameWin() => GameWin?.Invoke();
}