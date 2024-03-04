using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCanKill : MonoBehaviour
{
    [SerializeField] public bool ballCanKill;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private Renderer auraRenderer;
    [SerializeField] private float TimeToWait;
    [SerializeField] private float TimerToKill;
    private float IsTimeToKill;
    private float StartWaiting;
    void Start()
    {
        auraRenderer.enabled = false;
        ballCanKill = false;
        StartWaiting = TimeToWait;
        IsTimeToKill = TimerToKill;
        //auraRenderer = GetComponent<Renderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    void Update()
    {
        if (IsTimeToKill <= 0) 
        {
            auraRenderer.enabled = true;
            ballCanKill = true;
            StartWaiting -= Time.deltaTime;
            if (StartWaiting < 0)
            {
                IsTimeToKill = TimerToKill;
                StartWaiting = TimeToWait;
            }
        } else
        {
            auraRenderer.enabled = false;
            ballCanKill = false;
            IsTimeToKill -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && ballCanKill)
        {
            if (collision.gameObject.GetComponent<MovementPlayer>().PlayerNbr == MovementPlayer.PlayerNumber.One)
            {
                collision.gameObject.transform.position = gameManager.GetComponent<GameManager>().ListGameObject[3].transform.position;
            }
            else
            {
                collision.gameObject.transform.position = gameManager.GetComponent<GameManager>().ListGameObject[4].transform.position;
            }
        }
    }
}
