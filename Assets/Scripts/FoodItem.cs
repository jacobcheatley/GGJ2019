﻿using System;
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
    public ParticleSystem particle;
    public ParticleSystemRenderer psRenderer;
    public AudioSource source;
    public AudioClip moneyClip;
    public AudioClip sizzleClip;

    private bool bought;
    private bool flying = false;
    private bool distanceAchieved = false;
    private bool velocityAchieved = false;
    private bool farFlip = false;
    private bool fastFlip = false;
    private Transform panArea;
    private float panTime;
    private float leaveTime;
    private const float minTime = 0.1f;
    private const float minVelocity = 0.1f;
    private const float minFlyingDistance = 0.30f;

    private void Start()
    {
        // Set up canvas
        fiveSecondRuleCanvas.transform.parent = transform.parent;
        countdownImage.fillAmount = 0;
        psRenderer.material = new Material(psRenderer.material);
        psRenderer.material.mainTexture = foodUI.nutritionImage.sprite.texture;
        ParticleSystem.ShapeModule shape = particle.shape;
        shape.texture = foodUI.nutritionImage.sprite.texture;
        var emission = particle.emission;
        emission.SetBurst(0, new ParticleSystem.Burst(0, foodInfo.quality));
    }

    private void Update()
    {
        if (flying)
        {
            if (Vector3.Distance(transform.position, panArea.position) >= minFlyingDistance)
                distanceAchieved = true;
            if (Vector3.Distance(transform.position, panArea.position) >= minFlyingDistance * 3)
                farFlip = true;

            if (Vector3.Magnitude(rb.velocity) >= minVelocity)
                velocityAchieved = true;
            if (Vector3.Magnitude(rb.velocity) >= minVelocity * 5)
                fastFlip = false;
        }
    }

    public void CantBuy()
    {
        foodUI.CantBuy();
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
            Destroy(foodUI.gameObject);
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            source.PlayOneShot(moneyClip);
        }

        ResetCanScore();
    }

    public void Release()
    {
        Debug.Log("Released " + foodInfo.name);
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Furniture")
        {
            ResetCanScore();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Furniture")
        {
            ResetCanScore();
        }
    }

    private void ResetCanScore()
    {
        flying = false;
        velocityAchieved = false;
        distanceAchieved = false;
        farFlip = false;
        fastFlip = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            StartCoroutine("FiveSecondRule");
            flying = false;
        }

        if (other.tag == "FryingPan" && bought)
        {
            if (distanceAchieved && velocityAchieved)
            {
                Score();
                ResetCanScore();
            }
            panTime = Time.time;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "FryingPan" && bought) {
    //        if (Time.time - leaveTime >= 0.25f)
    //            flying = false;
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            StopCoroutine("FiveSecondRule");
            countdownImage.fillAmount = 0;
        }

        if (other.tag == "FryingPan" && bought)
        {
            if (Time.time - panTime >= minTime)
            {
                panArea = other.transform;
                flying = true;
                leaveTime = Time.time;
            }
        }
    }

    private void Score()
    {
        if (GameManager.instance.panHeld)
        {
            if (!GameManager.instance.canScore)
                return;

            Debug.Log($"{foodInfo.name} + scored!");
            particle.Play();
            int bonus = 0;
            if (farFlip) bonus++;
            // TODO: Particle EFFECTS
            if (fastFlip) bonus++;
            source.PlayOneShot(sizzleClip);
            GameManager.instance.IncreaseScore(foodInfo.foodGroup, foodInfo.quality, bonus);
        }
        else
        {
            Debug.Log($"{foodInfo.name} + NOT HOLDING!");
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
