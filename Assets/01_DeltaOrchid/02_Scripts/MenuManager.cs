using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
        GameManager.Instance._mainMenuPanel.SetActive(false);
        GameManager.Instance._gamePanel.SetActive(true);

        GameManager.Instance._gridManager.SetGridSize(rows, columns);
        GameManager.Instance._scoreManager.Restartinggame();
    }

   
}
