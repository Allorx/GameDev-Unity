using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public Image thisImage;
    public Sprite unlockImage;
    public Sprite rewardImage;

    public void Display(string type)
    {
        if (type == "UnlockedCharacter")
        {
            thisImage.sprite = unlockImage;
        }
        else if (type == "Reward")
        {
            thisImage.sprite = rewardImage;
        }
    }
}
