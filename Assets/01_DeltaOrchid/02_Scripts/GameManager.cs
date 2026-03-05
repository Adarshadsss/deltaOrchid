using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GridManager _gridManager;
    public ScoreManager _scoreManager;
    public SaveManager _saveManager;
    public AudioManager _audiomanager;
    public MatchSystem _matchSystem;
    public GameObject _mainMenuPanel;
    public GameObject _gamePanel;
    public GameObject _Gameendpanel;

    public int matchedPairs = 0;
    public int totalPairs;

    public GameObject cardPrefab;
    public Transform gridParent;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
