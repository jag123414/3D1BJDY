using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;

    public TMP_Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score : " + score;
    }
}