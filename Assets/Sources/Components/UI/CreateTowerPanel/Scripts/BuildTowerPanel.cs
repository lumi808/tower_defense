using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildTowerPanel : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private TowerLibrary _towerLibrary;
    [SerializeField] private BuildSystem _buildSystem;
    [SerializeField] private Animator _animator;

    private Dictionary<TowerData, Button> _buttons;

    private void Start()
    {
        _buttons = new Dictionary<TowerData, Button>();
        SceneEventSystem.Instance.BalanceChanged += OnBalanceChanged;

        foreach (TowerData towerData in _towerLibrary.Library)
        {
            GameObject newButton = Instantiate(_buttonPrefab);
            newButton.transform.SetParent(transform);

            TextMeshProUGUI text = newButton.GetComponentInChildren<TextMeshProUGUI>();
            Image image = newButton.GetComponent<Image>();
            image.sprite = towerData.Icon;

            UnityAction action = () =>
            {
                _buildSystem.BuildTower(towerData.TypeOfTower);
            };

            Button button = newButton.GetComponent<Button>();
            button.onClick.AddListener(action);

            _buttons.Add(towerData, button);
        }
    }

    private void OnDestroy()
    {
        SceneEventSystem.Instance.BalanceChanged -= OnBalanceChanged;
    }

    private void OnBalanceChanged(float currentBalance)
    {
        ChangeButtonsInteractivity();
    }

    public void ChangeButtonsInteractivity()
    {
        foreach(var buttonKV in _buttons)
        {
            buttonKV.Value.interactable = ResourceSystem.HasEnoughMoney(buttonKV.Key.UpdageInfo[0].UpgradePrice);
        }
    }

    public void Show()
    {
        _animator.SetBool("show", true);
        ChangeButtonsInteractivity();
    }

    public void Hide()
    {
        _animator.SetBool("show", false);
    }
}
