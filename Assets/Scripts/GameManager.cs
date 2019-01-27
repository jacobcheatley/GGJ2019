using HTC.UnityPlugin.Vive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{
    // Spawn random objects in fridge
    // Scene control (reset player location?)

    public static GameManager instance;

    [Header("Settings and Objects")]
    public int money = 287;
    public Transform[] spawnLocations;
    public GameObject[] foodPrefabs;
    public Text flatMoneyText;
    public Gradient moneyGradient;
    public float gameTime = 60;
    public Dictionary<FoodGroup, int> scores = new Dictionary<FoodGroup, int>();
    public int bonusScore = 0;
    public ScoreCanvas scoreCanvas;
    public EggTimer eggTimer;

    [HideInInspector]
    public List<FoodItem> fridgeFoodObjects = new List<FoodItem>();

    [HideInInspector]
    public Transform cameraTransform;
    [HideInInspector]
    public bool panHeld;
    private float startingMoney;
    [HideInInspector]
    public bool canScore = false;
    private bool playedGame = false;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        startingMoney = money;
        cameraTransform = Camera.main.transform;
        //Spawn food in fridge
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Transform location = spawnLocations[i];
            fridgeFoodObjects.Add(Instantiate(Randomization.RandomObject(foodPrefabs), location.position, Quaternion.identity).GetComponent<FoodItem>());
        }

        for (int i = 0; i <= (int)FoodGroup.Dairy; i++)
        {
            scores.Add((FoodGroup)i, 0);
        }

        scoreCanvas.UpdateScore(scores, bonusScore);
    }

    public void StartGame()
    {
        if (!playedGame)
        {
            playedGame = true;
            canScore = true;
            eggTimer.SetTicking(gameTime);
        }
    }

    public void IncreaseScore(FoodGroup foodGroup, int score, int bonus)
    {
        if (!canScore)
            return;

        scores[foodGroup] += score;
        bonusScore += bonus;
        scoreCanvas.UpdateScore(scores, bonusScore);
    }

    public void BuyFood(FoodItem foodItem)
    {
        money -= foodItem.foodInfo.price;
        fridgeFoodObjects.Remove(foodItem);

        for (int i = fridgeFoodObjects.Count - 1; i >= 0; i--)
        {
            FoodItem fridgeFood = fridgeFoodObjects[i];
            if (fridgeFood.foodInfo.price > money)
            {
                fridgeFood.CantBuy();
                fridgeFoodObjects.Remove(fridgeFood);
            }
        }

        // Change money display
        Color textColor = moneyGradient.Evaluate(money / startingMoney);
        flatMoneyText.color = textColor;
        flatMoneyText.text = "$" + money;
    }

    public void PanHeld()
    {
        panHeld = true;
    }

    public void PanReleased()
    {
        panHeld = false;
    }

    public void EndGame()
    {
        Debug.Log("ENDED TIMER");
        canScore = false;
    }
}
