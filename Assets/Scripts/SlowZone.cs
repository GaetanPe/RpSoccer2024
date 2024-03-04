using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SlowZone : MonoBehaviour
{
    [SerializeField] bool isOnSlow = false;
    [SerializeField] bool isOnSlowBall = false;
    [SerializeField] float slowDuration;
    [SerializeField] float slowDurationBall;
    [SerializeField] float LastSpeed;
    [SerializeField] float LastSpeedRun;
    [SerializeField] float LastSpeedBall;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("start slow");
            StartCoroutine("SlowPlayerCoroutine", collision);
        }

        else if (collision.gameObject.CompareTag("Ball"))
        {
            StartCoroutine("SlowBallCoroutine", collision);
        }
    }

    public void SlowPlayer(Collision collision)
    {
        LastSpeed = collision.gameObject.GetComponent<MovementPlayer>().speed;
        LastSpeedRun = collision.gameObject.GetComponent<MovementPlayer>().runSpeed;

        collision.gameObject.GetComponent<MovementPlayer>().speed /= 2;
        collision.gameObject.GetComponent<MovementPlayer>().runSpeed /= 2;
    }

    public void SlowBall(Collision collision)
    {
        //LastSpeed = collision.gameObject.GetComponent<MovementPlayer>().speed;

        collision.gameObject.GetComponent<Rigidbody>().velocity /= 2;
    }

    IEnumerator SlowPlayerCoroutine(Collision collision)
    {
        Debug.Log("is on slow");
        isOnSlow = true;
        float timer = 0;
        SlowPlayer(collision);
        while (timer < slowDuration)
        {
            timer += Time.deltaTime;
            yield return null;

        }
        collision.gameObject.GetComponent<MovementPlayer>().speed = LastSpeed;
        collision.gameObject.GetComponent<MovementPlayer>().runSpeed = LastSpeedRun;
        yield return new WaitForSeconds(slowDuration);
        isOnSlow = false;
    }

    IEnumerator SlowBallCoroutine(Collision collision)
    {
        Debug.Log("is on slow ball");
        isOnSlowBall = true;
        float timer = 0;
        SlowBall(collision);
        while (timer < slowDurationBall)
        {
            timer += Time.deltaTime;
            yield return null;

        }
        collision.gameObject.GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity * 2;
        yield return new WaitForSeconds(slowDurationBall);
        isOnSlowBall = false;
    }
}
