using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    public static MatchSystem Instance;

    List<CardController> flippedCards = new List<CardController>();

    public GameObject Gameendpanel;
    public GameObject GamePanel;

   public int matchedPairs = 0;
   public int totalPairs;

    bool checkingMatch = false;
    public GridManager gridManager;
    void Awake()
    {
        Instance = this;
    }

    private void CountTotalcards()
    {
        int totalCards = gridManager._totalcards;
        totalPairs = totalCards / 2;
        Debug.Log("totalPairs : " + totalPairs);
    }
    public void RegisterCard(CardController card)
    {
        if (checkingMatch) return;

        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            checkingMatch = true;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        ScoreManager.Instance.AddMove();

        if (flippedCards[0].cardID == flippedCards[1].cardID)
        {
            flippedCards[0].SetMatched();
            flippedCards[1].SetMatched();

            flippedCards[0].gameObject.SetActive(false);
            flippedCards[1].gameObject.SetActive(false);

            matchedPairs++;
            Debug.Log("matchedPairs : " + matchedPairs);

            ScoreManager.Instance.AddMatchScore();
            AudioManager.Instance.PlayMatch();
            CountTotalcards();
            CheckGamewin();
        }
        else
        {
            flippedCards[0].FlipBack();
            flippedCards[1].FlipBack();

            ScoreManager.Instance.AddMismatchPenalty();
            AudioManager.Instance.PlayMismatch();
        }

        flippedCards.Clear();
        checkingMatch = false;
    }
    void CheckGamewin()
    {
        ScoreManager.Instance.gameEndLogic(1);
    }

}