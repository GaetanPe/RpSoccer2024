using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawUi : MonoBehaviour
{
    private static GameManager gameManager;
    [SerializeField] private TMP_Text TimerUi;
    [SerializeField] private TMP_Text ScorePlayerOne;
    [SerializeField] private TMP_Text ScorePlayerTwo;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        TimerUi.text = gameManager.Timer.ToString("F0");
        ScorePlayerOne.text = gameManager.getNbrGoalPlayer(0).ToString();
        ScorePlayerTwo.text = gameManager.getNbrGoalPlayer(1).ToString();
    }
}
