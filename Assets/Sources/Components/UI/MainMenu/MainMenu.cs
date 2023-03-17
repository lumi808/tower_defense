using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _easyGameButton;
    [SerializeField] private Button _hardGameButton;

    private void Awake()
    {
        CheckSavedValues();
        _easyGameButton.onClick.AddListener(SetEasyGame);
        _hardGameButton.onClick.AddListener(SetHardGame);
        _startGameButton.onClick.AddListener(StartGame);
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
        _easyGameButton.image.color = Color.white;
        _hardGameButton.image.color = Color.green;
    }

    private void SetEasyGame()
    {
        PlayerPrefs.SetInt(Game.KEY, 0);
        _hardGameButton.image.color = Color.white;
        _easyGameButton.image.color = Color.green;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
