using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedBall : MonoBehaviour
{
    [SerializeField] float detectionDistance;
    public bool isDetected;
    Rigidbody ballRb;
    [SerializeField] float amplifyForce;
    [SerializeField] Energy energy;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position + transform.forward * 0.5f, transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            if(hit.collider.CompareTag("Ball"))
            {
                isDetected = true;
                ballRb = hit.collider.GetComponent<Rigidbody>();
            }
          
        }
        else
        {
            isDetected = false;
        }

    }
    public void Amplify()
    {
        Vector3 direction  = ballRb.velocity.normalized;
        Vector3 amplify = direction * amplifyForce;
        if (ballRb != null)
        {
            ballRb.AddForce(transform.forward *energy.getEnergy()* amplifyForce);
        }
    }
}
