using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodGroup
{
    Protein = 0,
    Fruit = 1,
    Vegetable = 2,
    Carbohydrate = 3,
    Dairy = 4
}

public class FoodInfo : MonoBehaviour
{
    public string name = "Mystery Meat";
    public int price = 50;
    public FoodGroup foodGroup;
    [Range(1, 3)]
    public int quality = 1;
}
