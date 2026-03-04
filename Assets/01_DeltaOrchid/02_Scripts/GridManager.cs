using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform gridParent;

    public int rows = 4;
    public int columns = 4;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        int totalCards = rows * columns;

        List<int> ids = CreatePairs(totalCards);
        Shuffle(ids);

        foreach (int id in ids)
        {
            GameObject card = Instantiate(cardPrefab, gridParent);
            card.GetComponent<CardController>().cardID = id;
        }
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
}
