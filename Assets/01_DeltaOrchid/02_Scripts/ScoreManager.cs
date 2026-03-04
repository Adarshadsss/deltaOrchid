using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text movesText;

    int score;
    int moves;
    int combo;

    void Awake()
    {
        Instance = this;
    }

    public void AddMatchScore()
    {
        combo++;

        int value = 100 * combo;

        score += value;

        UpdateUI();
    }

    public void AddMismatchPenalty()
    {
        combo = 0;

        score -= 10;

        UpdateUI();
    }

    public void AddMove()
    {
        moves++;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText)
            scoreText.text = "Score: " + score;

        if (movesText)
            movesText.text = "Moves: " + moves;
    }

    public int GetScore()
    {
        return score;
    }
}