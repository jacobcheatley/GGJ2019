using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BluePill : MonoBehaviour
{
    bool held = false;

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
            Debug.Log("Blue Pill");
            // TODO: Effect / wait
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
