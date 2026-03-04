using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    public static MatchSystem Instance;

    List<CardController> flippedCards = new List<CardController>();

    public GameObject Gameoverpanel;
    public GameObject GamePanel;

    int matchedPairs = 0;
    int totalPairs;

    bool checkingMatch = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int totalCards = FindObjectsOfType<CardController>().Length;
        totalPairs = totalCards / 2;
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

            ScoreManager.Instance.AddMatchScore();
            AudioManager.Instance.PlayMatch();

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

   
}