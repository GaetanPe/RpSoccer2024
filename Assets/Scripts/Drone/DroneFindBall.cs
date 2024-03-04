using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneFindBall : MonoBehaviour
{
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject DroneManager;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] public DroneNumber DroneNbr;
    [SerializeField] public DroneState droneState;
    [SerializeField] public bool Cooldown;
    [SerializeField] private float turn_speed;
    [SerializeField] private float TimetoWait;
    [SerializeField] private float TimeStartingToWait;

    public enum DroneNumber
    {
        one,
        two
    }

    public enum DroneState
    {
        Idle,
        FindBall,
        GoBase,
    }
    void Start()
    {
        TimeStartingToWait = TimetoWait;
        droneState = DroneState.Idle;
        Cooldown = false;
        DroneManager = GameObject.FindGameObjectWithTag("DroneManager");
        Ball = GameObject.FindGameObjectWithTag("Ball");
        agent = this.GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (droneState == DroneState.Idle) IsIdle();
        else if (droneState == DroneState.FindBall && Cooldown == false) GoFindBall();
        else if (droneState == DroneState.GoBase) GoOnSpawn();
    }

    private void IsIdle()
    {
        if (Cooldown && TimeStartingToWait >= 0) 
        {
            TimeStartingToWait -= Time.deltaTime;
        } else
        {
            Cooldown = false;
            TimeStartingToWait = TimetoWait;
        }
    }
    private void rotateTowards(GameObject go, Vector3 to)
    {

        Quaternion _lookRotation = Quaternion.LookRotation((to - go.transform.position).normalized);

        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, _lookRotation, Time.deltaTime * turn_speed);
    }

    public void GoFindBall()
    {
        agent.SetDestination(Ball.transform.position);
        //this.gameObject.transform.LookAt(Ball.transform.position);
        if (Vector3.Distance(transform.position, Ball.transform.position) < agent.stoppingDistance)
        {
            GrapBall();
        }
    }


    private void GrapBall()
    {
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponent<Rigidbody>().isKinematic = true;
        Ball.transform.parent = this.gameObject.transform;
        droneState = DroneState.GoBase;
    }

    private void GoOnSpawn()
    {
        Debug.Log("Go On Spawn");
        if (DroneNbr == DroneNumber.one)
        {
            agent.SetDestination(DroneManager.GetComponent<InstanciateDrone>().Spawners[0].transform.position);
            rotateTowards(this.gameObject, DroneManager.GetComponent<InstanciateDrone>().Spawners[0].transform.position);
            if (Vector3.Distance(transform.position, DroneManager.GetComponent<InstanciateDrone>().Spawners[0].transform.position) < 2)
            {
                DropBall();
            }
        }
        else
        {
            agent.SetDestination(DroneManager.GetComponent<InstanciateDrone>().Spawners[1].transform.position);
            rotateTowards(this.gameObject, DroneManager.GetComponent<InstanciateDrone>().Spawners[1].transform.position);

            if (Vector3.Distance(transform.position, DroneManager.GetComponent<InstanciateDrone>().Spawners[1].transform.position) < 2)
            {
                DropBall();
            }
        }
    }

    public void DropBall()
    {
        Debug.Log("je lache la ball");
        Ball.transform.parent = null;
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        Cooldown = true;
        droneState = DroneState.Idle;
    }
}
