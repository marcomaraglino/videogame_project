using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button LoadGameBTN;
    public void NewGame()
    {
<<<<<<< Updated upstream
        SceneManager.LoadScene("Scene Canvas - Pierfabio");

        Time.timeScale = 1f;
=======
        SceneManager.LoadScene("ScenaDefinitiva - Pierfabio");
>>>>>>> Stashed changes
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
