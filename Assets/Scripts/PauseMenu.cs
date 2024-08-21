using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private bool isPaused = false;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = FindAnyObjectByType<PlayerInput>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the ESC key is pressed
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Disable pause menu
        Time.timeScale = 1f;           // Resume game time
        isPaused = false;
        playerInput.enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);   // Enable pause menu
        Time.timeScale = 0f;           // Freeze the game time
        isPaused = true;
        playerInput.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Reset timeScale before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;  // Reset time scale back to normal
        SceneManager.LoadScene("MainMenu");
    }
}
