using UnityEngine;
using UnityEngine.UI;

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
    public FridgeFood[] fridgeFoodObjects;
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
        fridgeFoodObjects = new FridgeFood[spawnLocations.Length];
        startingMoney = money;
        cameraTransform = Camera.main.transform;
        //Spawn food in fridge
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Transform location = spawnLocations[i];
            fridgeFoodObjects[i] = Instantiate(Randomization.RandomObject(foodPrefabs), location.position, Quaternion.identity).GetComponent<FridgeFood>();
        }
    }

    public void SpendMoney(int spent)
    {
        money -= spent;

        foreach (FridgeFood fridgeFood in fridgeFoodObjects)
        {
            if (fridgeFood.foodInfo.price > money)
            {
                fridgeFood.DisablePurchasability();
            }
        }

        // Change money display
        Color textColor = moneyGradient.Evaluate(money / startingMoney);
        flatMoneyText.color = textColor;
        flatMoneyText.text = "$" + money;
    }
}
