using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private ParticleSystem LaserParticles;
    [SerializeField] private GoalId id;
    enum GoalId
    {
        One,
        Two
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();        
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("je suis dans les cache");
            StartCoroutine(gameManager.EndNewRound());
            LaserParticles.Play();
            if (id == GoalId.One)
                gameManager.setNbrGoalPlayer(0);
            else gameManager.setNbrGoalPlayer(1);
        }
    }
}
