using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public GameObject gameOverScreen;
    public GameObject hudScreen;
    public UnityEvent onGameOver;
    public Score score;
    private int scoreVal;

    private void Start()
    {
        isGameActive = true;
        gameOverScreen.SetActive(false);
        hudScreen.SetActive(true);
        Cursor.visible = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        scoreVal = score.ScoreValue;
        gameOverScreen.transform.GetComponentInChildren<TextMeshProUGUI>().text = $"High Score: {scoreVal}";
        hudScreen.SetActive(false);
        Cursor.visible = true;
        onGameOver.Invoke();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
