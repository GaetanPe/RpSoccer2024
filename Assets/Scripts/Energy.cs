using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    [SerializeField] float maxEnergy;  
    [SerializeField] Slider barEnergy;
    void Start()
    {
        barEnergy.maxValue = maxEnergy;
        barEnergy.value = maxEnergy;
    }

    public void decrementEnergy()
    {
        barEnergy.value -= 1;
    }
    public void incrementEnergy()
    {
        barEnergy.value += 1;
    }
    public float getEnergy() {  return barEnergy.value; }
    public void setEnergy(float value) { barEnergy.value = value; } 
}
