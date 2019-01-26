using HTC.UnityPlugin.Vive;
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

    [HideInInspector]
    public List<FoodItem> fridgeFoodObjects = new List<FoodItem>();
    [HideInInspector]
    public Transform cameraTransform;
    private float startingMoney;

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
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Resetting scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
                fridgeFood.DisablePurchasability();
                fridgeFoodObjects.Remove(fridgeFood);
            }
        }

        // Change money display
        Color textColor = moneyGradient.Evaluate(money / startingMoney);
        flatMoneyText.color = textColor;
        flatMoneyText.text = "$" + money;
    }
}
