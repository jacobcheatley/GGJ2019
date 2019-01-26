using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class FridgeFood : MonoBehaviour
{
    public Rigidbody rb;
    public FoodInfo foodInfo;
    public FoodUI foodUI;
    public MonoBehaviour[] interactables;
    public Text title;

    private bool bought;

    public void DisablePurchasability()
    {
        foreach (MonoBehaviour interactable in interactables)
        {
            interactable.enabled = false;
        }
    }

    public void Pickup()
    {
        Debug.Log("Picked up " + foodInfo.name);
        if (!bought)
        {
            bought = true;
            GameManager.instance.SpendMoney(foodInfo.price);
            foodUI.Remove();
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void Release()
    {
        Debug.Log("Released " + foodInfo.name);
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }
}
