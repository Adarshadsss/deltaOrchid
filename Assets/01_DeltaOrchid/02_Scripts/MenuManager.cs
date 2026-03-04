using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gamePanel;

    public GridManager gridManager;


    public void Easy()
    {
        StartGame(2, 2);
    }

    public void Medium()
    {
        StartGame(2, 3);
    }

    public void Hard()
    {
        StartGame(4, 4);
    }

    public void Expert()
    {
        StartGame(5, 6);
    }

    public void StartGame(int rows, int columns)
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);

        gridManager.SetGridSize(rows, columns);
        gridManager.GenerateGrid();
    }

   
}
