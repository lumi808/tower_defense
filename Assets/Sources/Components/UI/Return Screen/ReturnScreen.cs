using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnScreen : MonoBehaviour
{
    [SerializeField] private GameObject _root;
    [SerializeField] private Button _returnButtom;

    private void Awake()
    {
        _returnButtom.onClick.AddListener(ReturnToMainMenu);
    }

    public void Show()
    {
        _root.SetActive(true);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}