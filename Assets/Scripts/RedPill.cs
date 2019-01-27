using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPill : MonoBehaviour
{
    bool held = false;
    float pillTime = 0;
    public Drunkeness drunkeness;
    public GameObject geralt;
    public AudioSource geraltAudioSource;
    public AudioClip raw;

    private Vector3 up;
    private Vector3 down;

    private void Start()
    {
        down = geralt.transform.position;
        up = new Vector3(geralt.transform.position.x, -0.08f, geralt.transform.position.z);
    }

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
        {
            geralt.transform.position = Vector3.MoveTowards(geralt.transform.position, up, 0.6f * Time.deltaTime);
            if (Vector3.Distance(geralt.transform.position, up) < 0.1f && !GameManager.instance.geraltActive)
            {
                GameManager.instance.geraltActive = true;
                geraltAudioSource.PlayOneShot(raw);
            }
                
            drunkeness.SetDrugScale(2);
        }
        else
        {
            //geralt.transform.position = Vector3.MoveTowards(geralt.transform.position, down, 0.3f * Time.deltaTime);
            drunkeness.SetDrugScale(0);
        }
    }
}
