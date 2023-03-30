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
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _goBackGameButton;
    [SerializeField] private TextMeshProUGUI _easyGameText;
    [SerializeField] private TextMeshProUGUI _hardGameText;

    private void Awake()
    {
        CheckSavedValues();
        _easyGameButton.onClick.AddListener(SetEasyGame);
        _hardGameButton.onClick.AddListener(SetHardGame);
        _startGameButton.onClick.AddListener(StartGame);
        _loadGameButton.onClick.AddListener(LoadGame);
        _playGameButton.onClick.AddListener(ProceedToNext);
        _goBackGameButton.onClick.AddListener(GoBack);
    }

    private void GoBack()
    {
        _easyGameButton.gameObject.SetActive(false);
        _hardGameButton.gameObject.SetActive(false);
        _startGameButton.gameObject.SetActive(false);
        _loadGameButton.gameObject.SetActive(true);
        _playGameButton.gameObject.SetActive(true);
        _goBackGameButton.gameObject.SetActive(false);
    }

    private void ProceedToNext()
    {
        _easyGameButton.gameObject.SetActive(true);
        _hardGameButton.gameObject.SetActive(true);
        _startGameButton.gameObject.SetActive(true);
        _loadGameButton.gameObject.SetActive(false);
        _playGameButton.gameObject.SetActive(false);
        _goBackGameButton.gameObject.SetActive(true);
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
