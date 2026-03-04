using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] List<Sprite> cardSprites;

    public GameObject cardPrefab;
    public Transform gridParent;

    public int rows = 4;
    public int columns = 4;

   public void GenerateGrid()
    {
        int totalCards = rows * columns;

        List<int> ids = CreatePairs(totalCards);
        Shuffle(ids);

        foreach (int id in ids)
        {
            GameObject card = Instantiate(cardPrefab, gridParent);

            CardController controller = card.GetComponent<CardController>();

            controller.cardID = id;

            // Assign sprite based on card ID
            controller.SetSprite(cardSprites[id]);
        }
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
        foreach (Transform child in gridParent)
        {
            DestroyImmediate(child);
        }
    }
}
