using TMPro;
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
  

    public void Restartinggame()
    {
        score = 0;
        moves = 10;
        MatchSystem.Instance.matchedPairs = 0;
        MatchSystem.Instance.totalPairs = 0;
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
            gameEndLogic(0);

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


    public void gameEndLogic(int level)
    {
        SaveData data = _saveManager.Load();

        if (data == null)
        {
            data = new SaveData();
            data._bestScore = score;
        }
        else
        {
            if (score > data._bestScore)
            {
                data._bestScore = score;
            }
        }

        _saveManager.Save(data);

        scoreTextGameOver.text = score.ToString();
        BestTextGameOver.text = data._bestScore.ToString();

        if (level == 0)
        {
            _gridManager.DestroyAllChilds();
            AudioManager.Instance.PlayGameOver();
            MatchSystem.Instance.GamePanel.SetActive(false);
            MatchSystem.Instance.Gameendpanel.SetActive(true);
            MatchSystem.Instance.Gameendpanel.GetComponent<ChangeImages>().ChangeGameLostImage();

        }
        else if (level == 1)
        {
            if (MatchSystem.Instance.matchedPairs >= MatchSystem.Instance.totalPairs)
            {
                AudioManager.Instance.PlayGamewin();
                MatchSystem.Instance.GamePanel.SetActive(false);
                MatchSystem.Instance.Gameendpanel.SetActive(true);
                MatchSystem.Instance.Gameendpanel.GetComponent<ChangeImages>().ChangeGamewinImage();
            }
        }

       
    }
}