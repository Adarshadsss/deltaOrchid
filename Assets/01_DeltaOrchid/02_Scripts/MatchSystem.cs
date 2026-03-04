using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchSystem : MonoBehaviour
{
    public static MatchSystem Instance;

    List<CardController> flippedCards = new List<CardController>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterCard(CardController card)
    {
        flippedCards.Add(card);

        if (flippedCards.Count == 2)
            StartCoroutine(CheckMatch());
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (flippedCards[0].cardID == flippedCards[1].cardID)
        {
            flippedCards[0].SetMatched();
            flippedCards[1].SetMatched();

           
        }
        else
        {
            flippedCards[0].FlipBack();
            flippedCards[1].FlipBack();

          
        }

        flippedCards.Clear();
    }
}