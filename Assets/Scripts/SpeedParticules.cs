using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedParticules : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public float vitesseMax = 15f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float vitesseActuelle = rb.velocity.magnitude;
        float normalisedVitesse = Mathf.Clamp01(vitesseActuelle / vitesseMax);
        trailRenderer.time = normalisedVitesse * 2f;
    }
}
