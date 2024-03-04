using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DroneFindBall;

public class InstanciateDrone : MonoBehaviour
{
    [SerializeField] public List<GameObject> Spawners;
    [SerializeField] public List<GameObject> Drones;
    [SerializeField] private GameObject DronePrefab;
    [SerializeField] private GameObject target;
    [SerializeField] private float turn_speed = 2;
    void Start()
    {
        Vector3 pos1 = Spawners[0].transform.position;
        Vector3 pos2 = Spawners[1].transform.position;
        //Quaternion rotation = new Quaternion(0,-180,0,1);
        GameObject go1 = Instantiate(DronePrefab, pos1, Quaternion.identity);
        go1.GetComponent<DroneFindBall>().DroneNbr = DroneNumber.one;
        GameObject go2 = Instantiate(DronePrefab, pos2, Quaternion.identity);
        go2.GetComponent<DroneFindBall>().DroneNbr = DroneNumber.two;
        Drones.Add(go1);
        Drones.Add(go2);
    }

    void Update()
    {
        rotateTowards(Drones[0], target.transform.position);
        rotateTowards(Drones[1], target.transform.position);
    }

    private void rotateTowards(GameObject go ,Vector3 to)
    {

        Quaternion _lookRotation = Quaternion.LookRotation((to - go.transform.position).normalized);

        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, _lookRotation, Time.deltaTime * turn_speed);
    }
}
