using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OmbreBallon : MonoBehaviour
{
    [SerializeField] private GameObject Ombre;
    [SerializeField] LayerMask groundLayer;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 500f, groundLayer))
        {
            Ombre.transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            float scaleFactor = 0.1f + transform.position.y * 0.1f;
            Ombre.transform.localScale = new Vector3(-scaleFactor, 0.1f, -scaleFactor);
        }
    }
}
