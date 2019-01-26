using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class FoodItem : MonoBehaviour
{
    public Rigidbody rb;
    public FoodInfo foodInfo;
    public FoodUI foodUI;
    public GameObject fiveSecondRuleCanvas;
    public GameObject meshHolder;
    public Image countdownImage;
    public Text title;

    private bool bought;

    private void Start()
    {
        // Set up canvas
        fiveSecondRuleCanvas.transform.parent = transform.parent;
        countdownImage.fillAmount = 0;
    }

    public void DisablePurchasability()
    {
        meshHolder.transform.parent = transform.parent;
        Destroy(gameObject);
    }

    public void Pickup()
    {
        Debug.Log($"Picked up {foodInfo.name}");
        if (!bought)
        {
            bought = true;
            GameManager.instance.BuyFood(this);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            StartCoroutine("FiveSecondRule");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            StopCoroutine("FiveSecondRule");
            countdownImage.fillAmount = 0;
        }
    }

    private IEnumerator FiveSecondRule()
    {
        float remainingTime = 5f;
        countdownImage.fillAmount = 1f;

        while (remainingTime > 0)
        {
            fiveSecondRuleCanvas.transform.position = transform.position;
            remainingTime -= Time.deltaTime;
            countdownImage.fillAmount = remainingTime / 5;
            yield return null;
        }

        // Explode the food - to be done PARTICLES
        Debug.Log($"{foodInfo.name} exploded.");
        Destroy(GetComponent<Interactable>().highlightHolder);
        Destroy(fiveSecondRuleCanvas);
        Destroy(gameObject);
    }
}
