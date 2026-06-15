using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timeText;

    public GameObject gameOverUI;
    public GameObject restartButton;

    public float timeLimit = 60f;

    bool gameOver = false;

    void Update()
    {
        if (gameOver)
            return;

        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0)
        {
            timeLimit = 0;
            gameOver = true;

            gameOverUI.SetActive(true);
            restartButton.SetActive(true);

            Time.timeScale = 0f;
        }

        int minutes = Mathf.FloorToInt(timeLimit / 60);
        int seconds = Mathf.FloorToInt(timeLimit % 60);

        timeText.text =
            $"Time : {minutes:00}:{seconds:00}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}