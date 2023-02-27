using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildTowerPanel : MonoBehaviour
{
    // Задача: при запуске игры создавать кнопки для каждой из башнен
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private TowerLibrary _towerLibrary;
    [SerializeField] private BuildSystem _buildSystem;
    [SerializeField] private Animator _animator;

    private void Start()
    {
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
        }
    }

    public void Show()
    {
        _animator.SetBool("show", true);
    }

    public void Hide()
    {
        _animator.SetBool("show", false);
    }
}
