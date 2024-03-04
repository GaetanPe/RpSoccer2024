using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] GameObject settingsUi;
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Settinngs()
    {
        settingsUi.SetActive(true);
    }
}
