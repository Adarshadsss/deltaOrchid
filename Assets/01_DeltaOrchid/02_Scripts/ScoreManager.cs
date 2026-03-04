using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text scoreTextGameOver;
    [SerializeField] TMP_Text movesText;
    [SerializeField] TMP_Text BestTextGameOver;
    public SaveManager _saveManager;
    public GridManager _gridManager;
    int score;
    int moves;
    int combo;

    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        score = 0;
        moves = 10;
        UpdateUI();
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
        if (score != 0)
        {
            score -= 10;

        }

        UpdateUI();
    }

    public void AddMove()
    {
        moves--;
        if (moves == 0)
        {

            SaveData data = new SaveData();
            data._bestScore = score;
            _saveManager.Save(data);
            _gridManager.DestroyAllChilds();
            AudioManager.Instance.PlayGameOver();
            MatchSystem.Instance.GamePanel.SetActive(false);
            MatchSystem.Instance.Gameoverpanel.SetActive(true);
            scoreTextGameOver.text = score.ToString();
            if (_saveManager.Load()._bestScore.ToString() != null)
            {
                BestTextGameOver.text = _saveManager.Load()._bestScore.ToString();
            }
            else
            {
                BestTextGameOver.text = score.ToString();
            }
        }
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