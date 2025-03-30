using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject levelCompleteScreen;
    public GameObject gameOverScreen;

    private bool isGameOver = false;

    private void Start()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; // Exit Start() if a GameManager already exists
        }
        Application.targetFrameRate = 60;
        NewGame();
    }

    public void NewGame()
    {
        LoadLevel(1);
    }

    public void GameOver()
    {
        isGameOver = true;
        ShowGameOverScreen();
    }

    public void LoadLevel(int stage)
    {
        isGameOver = false;
        SceneManager.LoadScene($"Level {stage}");
    }

    public void LevelComplete()
    {
        if (!isGameOver)
        {
            ShowLevelCompleteScreen();
        }
    }

    public void RestartLevel(float delay)
    {
        Invoke(nameof(RestartLevel), delay);
    }

    private void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ShowLevelCompleteScreen()
    {
        levelCompleteScreen.SetActive(true);
    }

    private void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
