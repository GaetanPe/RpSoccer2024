using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingAura : MonoBehaviour
{
    public Material auraMaterial;
    public Color baseColor = Color.red;
    public Color blinkColor = Color.white;
    public float blinkInterval = 0.5f; // Durée d'un clignotement en secondes

    private Renderer auraRenderer;
    private bool isBlinking = false;

    void Start()
    {
        auraRenderer = GetComponent<Renderer>();
/*        if (auraRenderer == null || auraMaterial == null)
        {
            Debug.LogError("Assurez-vous d'avoir un Renderer et un Material attribués au script.");
            enabled = false;
            return;
        }*/

        auraMaterial.color = baseColor;
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkInterval);

            if (isBlinking)
            {
                auraMaterial.color = baseColor;
            }
            else
            {
                auraMaterial.color = blinkColor;
            }
            isBlinking = !isBlinking;
        }
    }
}
