using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class FridgeDoor : MonoBehaviour
{
    public bool closed = false;
    public int openAngle;
    public int closedAngle;
    public AudioSource source;
    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip lambSauce;

    private void Update()
    {
        //Debug.Log("localrot y " + transform.parent.localRotation.eulerAngles.y);

        // openAngle = -90, 270
        // closedAngle = 0
        if ((closed && (int)transform.parent.localRotation.eulerAngles.y != closedAngle)
            || (!closed && (int)transform.parent.localRotation.eulerAngles.y != openAngle))
        {
            transform.parent.RotateAround(transform.parent.position, new Vector3(0, 1, 0), closed ? -1 : 1);
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetGrabStarting() != GrabTypes.None)
        {
            Debug.Log("Closed is " + closed);
            closed = !closed;
            if (closed)
                source.PlayOneShot(closeClip, 0.5f);
            else
            {
                if (GameManager.instance.geraltActive)
                    GameManager.instance.geralt.PlayOneShot(lambSauce);

                source.PlayOneShot(openClip, 0.5f);
            }
        }
    }
}
