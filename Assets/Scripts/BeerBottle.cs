using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkInstance
{
    public float timeRemaining;
    public float halfTime;
}

public class BeerBottle : MonoBehaviour
{
    public Drunkeness drunkeness;

    bool held = false;
    private List<DrinkInstance> drinks = new List<DrinkInstance>();

    public void Pickup()
    {
        Debug.Log("Beer picked up");
        held = true;
    }

    public void Putdown()
    {
        Debug.Log("Beer put down");
        held = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (held && other.tag == "Consumption")
        {
            Debug.Log("Drank");
            drinks.Add(new DrinkInstance { timeRemaining = 30f, halfTime = 15f });
        }
    }

    public void Update()
    {
        float intensity = 0;
        List<DrinkInstance> toRemove = new List<DrinkInstance>();
        foreach (DrinkInstance drink in drinks)
        {
            drink.timeRemaining -= Time.deltaTime;
            intensity += Mathf.Clamp(1 - (Mathf.Abs(drink.timeRemaining - drink.halfTime) / drink.halfTime), 0, 5);
            if (drink.timeRemaining <= 0) toRemove.Add(drink);
        }
        foreach (DrinkInstance drink in toRemove)
        {
            drinks.Remove(drink);
        }
        drunkeness.SetVariables(0.005f * intensity, 0.05f * intensity);
    }
}
