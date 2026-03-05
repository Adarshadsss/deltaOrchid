using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text scoreTextGameOver;
    [SerializeField] TMP_Text movesText;
    [SerializeField] TMP_Text BestTextGameOver;
   
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
       GameManager.Instance.matchedPairs = 0;
        GameManager.Instance.totalPairs = 0;
        UpdateUI();
    }
    public void AddMatchScore()
    {
        combo++;
        moves++;
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
        SaveData data =GameManager.Instance._saveManager.Load();

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

        GameManager.Instance._saveManager.Save(data);

        scoreTextGameOver.text = score.ToString();
        BestTextGameOver.text = data._bestScore.ToString();

        if (level == 0)
        {
           GameManager.Instance._gridManager.DestroyAllChilds();
            AudioManager.Instance.PlayGameOver();
            GameManager.Instance._gamePanel.SetActive(false);
            GameManager.Instance._Gameendpanel.SetActive(true);
            GameManager.Instance._Gameendpanel.GetComponent<ChangeImages>().ChangeGameLostImage();

        }
        else if (level == 1)
        {
            if (GameManager.Instance.matchedPairs >= GameManager.Instance.totalPairs)
            {
                GameManager.Instance._gridManager.DestroyAllChilds();
                AudioManager.Instance.PlayGamewin();
                GameManager.Instance._gamePanel.SetActive(false);
                GameManager.Instance._Gameendpanel.SetActive(true);
                GameManager.Instance._Gameendpanel.GetComponent<ChangeImages>().ChangeGamewinImage();
            }
        }

       
    }
}