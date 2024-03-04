using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementDrone : MonoBehaviour
{
    [SerializeField] private Vector2 SizeWorld;
    private NavMeshAgent agent;
    float moveWaitingDuration = 2;
    bool isWaiting;
    Vector3 pos;
    void Start()
    {
        agent = transform.gameObject.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
            StartCoroutine("Move");
    }

    private void ChangeDistination()
    {
        pos = new Vector3(Random.Range(-SizeWorld.x, SizeWorld.x), transform.position.y, Random.Range(-SizeWorld.y, SizeWorld.y));
        agent.SetDestination(pos);
    }

    IEnumerator Move()
    {
        isWaiting = true;
        float timer = 0;
        while (timer < moveWaitingDuration)
        {
            ChangeDistination();
            timer += Time.deltaTime;
            yield return null;

        }
        yield return new WaitForSeconds(moveWaitingDuration);
        agent.SetDestination(pos);
        isWaiting = false;
    }
}
