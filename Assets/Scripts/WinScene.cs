using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    public void LoadWinScene()
    {
        StartCoroutine(WinCoroutine());
    }

    public void LoadDeadScene()
    {
        StartCoroutine(DeadCoroutine());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;  // Reset time scale back to normal
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the game...");
    }

    private IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("WinScene");
    }

    private IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("DeadScene");
    }
}
