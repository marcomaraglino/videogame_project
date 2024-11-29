using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button LoadGameBTN;

    private void Start()
    {
        LoadGameBTN.onClick.AddListener(() =>
        { 
            SaveManager.Instance.StartLoadedGame();

            Time.timeScale = 1f;
        });
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Scene Canvas - Pierfabio");

        Time.timeScale = 1f;
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
