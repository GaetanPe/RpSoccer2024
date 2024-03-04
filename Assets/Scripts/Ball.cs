using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float forwardMagnitude;
    [SerializeField] float upwardMagnitude;
    [SerializeField] AudioManager audioManager;
    
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.CompareTag("Player"))
        {
            audioManager.PlaySFX("shoot");
            Vector3 repulsionDirection = transform.position - collision.transform.position;
            repulsionDirection.Normalize();

            rb.AddForce(repulsionDirection * forwardMagnitude, ForceMode.Impulse);
            
            rb.AddForce(Vector3.up * upwardMagnitude, ForceMode.Impulse);
        }
    }
}
