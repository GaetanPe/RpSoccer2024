using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _cameraTargetBall;
    [SerializeField] private GameObject _cameraTargetPlayer;
    [SerializeField] private cameraSet _cameraSet;
    
    enum cameraSet
    {
        Player,
        Ball
    }
    void Start()
    {
        _cameraSet = cameraSet.Player;
        _camera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (transform.gameObject.GetComponent<MovementPlayer>().PlayerNbr == MovementPlayer.PlayerNumber.One && Input.GetKeyDown(KeyCode.E))
        {
            if (_cameraSet == cameraSet.Player)
                _cameraSet = cameraSet.Ball;
            else
                _cameraSet = cameraSet.Player;
        }
        else if (transform.gameObject.GetComponent<MovementPlayer>().PlayerNbr == MovementPlayer.PlayerNumber.Two && Input.GetKeyDown(KeyCode.Keypad0))
        {
            if (_cameraSet == cameraSet.Player)
                _cameraSet = cameraSet.Ball;
            else
                _cameraSet = cameraSet.Player;
        }

        if (_cameraSet == cameraSet.Player)
        {
            _camera.transform.LookAt(_cameraTargetPlayer.transform);

        } else if (_cameraSet == cameraSet.Ball)
        {
            _camera.transform.LookAt(_cameraTargetBall.transform);
        }
    }
}
