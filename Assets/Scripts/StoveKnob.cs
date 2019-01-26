using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class StoveKnob : MonoBehaviour
{
    public StoveCoil coil;
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

    public void Toggle()
    {
        on = !on;
        Debug.Log("Toggled " + on);
        transform.rotation = on ? Quaternion.Euler(-145, 0, 170) : Quaternion.Euler(-145, 0, 0);
        if (on)
            coil.TurnOn();
        else
            coil.TurnOff();
    }
}
