using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanelUI : MonoBehaviour
{
    private static GameManager gameManager;
    [SerializeField] private TMP_Text LooseUi;
    [SerializeField] private TMP_Text WinUi;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        LooseUi.text = gameManager.Loser;
        WinUi.text = gameManager.Winner;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
