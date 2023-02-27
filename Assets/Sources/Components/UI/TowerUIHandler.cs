using System;
using UnityEngine;

public class TowerUIHandler : MonoBehaviour
{
    [SerializeField] private BuildTowerPanel _buildTowerPanel;
    [SerializeField] private TowerOptionsPanel _towerOpensPanel;

    // Задча: по события включать или выключать нужные ui элементы

    private void Start()
    {
        SceneEventSystem.Instance.CellSelected += OnCellSelected;
        SceneEventSystem.Instance.CellDeselected += OnCellDeselected;
        SceneEventSystem.Instance.CellUsed += OnCellUsed;
    }

    private void OnCellUsed(TowerCell towerCell)
    {
        _buildTowerPanel.Hide();
        _towerOpensPanel.Show();
    }

    private void OnCellDeselected(TowerCell towerCell)
    {
        _buildTowerPanel.Hide();
        _towerOpensPanel.Hide();
    }

    private void OnCellSelected(TowerCell towerCell)
    {
        if(towerCell.IsCellUsed())
        {
            _towerOpensPanel.Show();
        }
        else
        {
            _buildTowerPanel.Show();
        }
    }
}
