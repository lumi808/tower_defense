using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _easyGameButton;
    [SerializeField] private Button _hardGameButton;
    [SerializeField] private TextMeshProUGUI _easyGameText;
    [SerializeField] private TextMeshProUGUI _hardGameText;

    private void Awake()
    {
        CheckSavedValues();
        _easyGameButton.onClick.AddListener(SetEasyGame);
        _hardGameButton.onClick.AddListener(SetHardGame);
        _startGameButton.onClick.AddListener(StartGame);
        _loadGameButton.onClick.AddListener(LoadGame);
    }

    private void LoadGame()
    {
        PlayerPrefs.SetInt(Game.LOAD_KEY, 1);
        SceneManager.LoadScene(1);
    }

    private void CheckSavedValues()
    {
        if (PlayerPrefs.HasKey(Game.KEY))
        {
            if (PlayerPrefs.GetInt(Game.KEY) == 0)
                SetEasyGame();
            else
                SetHardGame();
        }
        else
        {
            SetEasyGame();
        }
    }

    private void SetHardGame()
    {
        PlayerPrefs.SetInt(Game.KEY, 1);
        _hardGameText.fontStyle = FontStyles.Bold;
        _easyGameText.fontStyle = FontStyles.Normal;
    }

    private void SetEasyGame()
    {
        PlayerPrefs.SetInt(Game.KEY, 0);
        _hardGameText.fontStyle = FontStyles.Normal;
        _easyGameText.fontStyle = FontStyles.Bold;
    }

    private void StartGame()
    {
        PlayerPrefs.SetInt(Game.LOAD_KEY, 0);
        SceneManager.LoadScene(1);
    }
}
