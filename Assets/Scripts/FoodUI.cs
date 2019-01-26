using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodUI : MonoBehaviour
{
    [Header("Info")]
    public FoodInfo foodInfo;
    public Sprite[] nutritionIcons;

    [Header("Objects")]
    public Image nutritionImage;
    public Text namePriceText;

    public void Start()
    {
        // Name and Price
        namePriceText.text = foodInfo.name + " - $" + foodInfo.price;

        // Nutrition Icons
        nutritionImage.sprite = nutritionIcons[(int)foodInfo.foodGroup];
        GameObject nutritionObject = nutritionImage.gameObject;
        for (int i = 0; i < foodInfo.quality - 1; i++)
        {
            Instantiate(nutritionObject, nutritionObject.transform.parent);
        }
    }

    public void Remove()
    {
        // Disable all UI
        this.enabled = false;
    }

    public void Update()
    {
        //transform.LookAt(GameManager.instance.cameraTransform, Vector3.up);
        Vector3 cameraRotation = GameManager.instance.cameraTransform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
    }
}
