using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    List<CardController> flippedCards = new List<CardController>();
    bool checkingMatch = false;


    private void CountTotalcards()
    {
        int totalCards =GameManager.Instance._gridManager._totalcards;
        GameManager.Instance.totalPairs = totalCards / 2;
        Debug.Log("totalPairs : " + GameManager.Instance.totalPairs);
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

            GameManager.Instance.matchedPairs++;
            Debug.Log("matchedPairs : " + GameManager.Instance.matchedPairs);

            GameManager.Instance._scoreManager.AddMatchScore();
            GameManager.Instance._audiomanager.PlayMatch();
            CountTotalcards();
            CheckGamewin();
        }
        else
        {
            flippedCards[0].FlipBack();
            flippedCards[1].FlipBack();

            GameManager.Instance._scoreManager.AddMismatchPenalty();
            GameManager.Instance._audiomanager.PlayMismatch();
        }

        flippedCards.Clear();
        checkingMatch = false;
    }
    void CheckGamewin()
    {
        GameManager.Instance._scoreManager.gameEndLogic(1);
    }

}