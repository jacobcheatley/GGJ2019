using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvas : MonoBehaviour
{
    public Text[] scoreTexts;

    public void UpdateScore(Dictionary<FoodGroup, int> scores, int bonusScore)
    {
        foreach (var item in scores)
        {
            Debug.Log(item.Key);
            scoreTexts[(int)item.Key].text = item.Value.ToString();
        }

        scoreTexts[5].text = bonusScore.ToString();
    }
}
