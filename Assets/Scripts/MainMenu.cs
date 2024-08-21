using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;  // Reset time scale back to normal
        SceneManager.LoadScene("LoadScene");
    }

    public void CreditScene()
    {
        Time.timeScale = 1f;  // Reset time scale back to normal
        SceneManager.LoadScene("CreditScene");
    }

    public void ExitGame()
    {
        Application.Quit(); 
        Debug.Log("Quitting the game...");
    }
}
