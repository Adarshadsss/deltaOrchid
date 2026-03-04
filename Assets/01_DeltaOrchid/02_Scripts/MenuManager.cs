using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gamePanel;

    public GridManager gridManager;

    public void StartGame(int rows, int columns)
    {
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);

        gridManager.SetGridSize(rows, columns);
        gridManager.GenerateGrid();
    }
}
