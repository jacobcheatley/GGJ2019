using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPill : MonoBehaviour
{
    bool held = false;
    float pillTime = 0;
    public Drunkeness drunkeness;

    public void Pickup()
    {
        held = true;
    }

    public void Putdown()
    {
        held = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (held && other.tag == "Consumption")
        {
            Debug.Log("Red Pill");
            // TODO: Effect / wait
            pillTime = 20f;
        }
    }

    private void Update()
    {
        pillTime -= Time.deltaTime;
        if (pillTime >= 0)
            drunkeness.SetDrugScale(2);
        else
            drunkeness.SetDrugScale(0);
    }
}
