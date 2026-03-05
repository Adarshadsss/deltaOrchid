using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] List<Sprite> cardSprites;

   

    public int rows = 4;
    public int columns = 4;
    public int _totalcards;
    public void GenerateGrid()
    {

        GameManager.Instance.gridParent.GetComponent<GridLayoutGroup>().enabled = true;

        _totalcards = rows * columns;

        List<int> ids = CreatePairs(_totalcards);
        Shuffle(ids);

        List<CardController> cards = new List<CardController>();

        foreach (int id in ids)
        {
            GameObject card = Instantiate(GameManager.Instance.cardPrefab, GameManager.Instance.gridParent);

            CardController controller = card.GetComponent<CardController>();

            controller.cardID = id;
            controller.SetSprite(cardSprites[id]);

            cards.Add(controller);
        }

        StartCoroutine(ShowCardsThenHide(cards));
    }

    IEnumerator ShowCardsThenHide(List<CardController> cards)
    {
        foreach (var card in cards)
            card.ShowFrontInstant();

        yield return new WaitForSeconds(.2f);

        foreach (var card in cards)
            card.FlipBack();
        GameManager.Instance.gridParent.GetComponent<GridLayoutGroup>().enabled = false;
    }


    public void SetGridSize(int r, int c)
    {
        rows = r;
        columns = c;
        GenerateGrid();

    }
    List<int> CreatePairs(int total)
    {
        List<int> ids = new List<int>();

        int pairs = total / 2;

        for (int i = 0; i < pairs; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        return ids;
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);

            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    public void DestroyAllChilds()
    {
        foreach (Transform child in GameManager.Instance.gridParent)
        {
            Destroy(child.gameObject);
        }
    }
}
