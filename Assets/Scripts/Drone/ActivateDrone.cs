using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementPlayer;

public class ActivateDrone : MonoBehaviour
{
    [SerializeField] private GameObject DroneManager;
    [SerializeField] private InstanciateDrone scriptListDrones;
    [SerializeField] public ActiveZoneNumber ActiveZonenbr;

    public enum ActiveZoneNumber
    {
        One,
        Two
    }

    private void Start()
    {
          scriptListDrones = DroneManager.GetComponent<InstanciateDrone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Activation");
            if (other.gameObject.GetComponent<MovementPlayer>().PlayerNbr == MovementPlayer.PlayerNumber.One && ActiveZonenbr == ActiveZoneNumber.One)
            {
                scriptListDrones.Drones[0].GetComponent<DroneFindBall>().droneState = DroneFindBall.DroneState.FindBall;
            }
            else if (other.gameObject.GetComponent<MovementPlayer>().PlayerNbr == MovementPlayer.PlayerNumber.Two && ActiveZonenbr == ActiveZoneNumber.Two)
            {
                scriptListDrones.Drones[1].GetComponent<DroneFindBall>().droneState = DroneFindBall.DroneState.FindBall;
            }
        }
    }
}
