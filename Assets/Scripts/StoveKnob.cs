using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class StoveKnob : MonoBehaviour
{
    public StoveCoil coil;
    public float rotationSpeed = 5f;
    public int onRotation = 320;
    public int offRotation = 40;
    public AudioSource source;
    public AudioClip onClip;
    public AudioClip offClip;

    private bool on = false;
    private Vector3 startRotation;

    private void Start()
    {
        Vector3 startRotation = transform.rotation.eulerAngles;
    }

    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetGrabStarting() != GrabTypes.None)
            Toggle();
    }

    public void Update()
    {
        // on 170 (320?), off 0 (40?)
        float zIncrement = Time.deltaTime * rotationSpeed;
        float beforehand = transform.localRotation.eulerAngles.z;

        if (on && (int)transform.localRotation.eulerAngles.z != onRotation)
        {

            if (Mathf.Abs(beforehand - onRotation) < zIncrement)
            {
                Vector3 euler = transform.localRotation.eulerAngles;
                euler.z = onRotation;
                transform.localRotation = Quaternion.Euler(euler);

            }
            else
                transform.Rotate(new Vector3(0, 0, zIncrement));

        }
        else if (!on && (int)transform.localRotation.eulerAngles.z != offRotation)
        {

            if (Mathf.Abs(beforehand - offRotation) < zIncrement)
            {
                Vector3 euler = transform.localRotation.eulerAngles;
                euler.z = offRotation;
                transform.localRotation = Quaternion.Euler(euler);

            }
            else
                transform.Rotate(new Vector3(0, 0, -zIncrement));

        }
    }

    public void Toggle()
    {
        on = !on;
        Debug.Log("Toggled " + on);
        if (on)
        {
            GameManager.instance.StartGame();
            source.PlayOneShot(onClip);
            coil.TurnOn();
        }
        else
        {
            source.PlayOneShot(offClip);
            coil.TurnOff();
        }
    }
}
