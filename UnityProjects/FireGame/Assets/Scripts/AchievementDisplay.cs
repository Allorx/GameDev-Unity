using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public Animator thisAnimator;

    public void Display(string type)
    {
        if (type == "UnlockedCharacter")
        {
            Debug.Log(type);
            //run animation unlock
            thisAnimator.Play("unlock", -1, 0f);
        }
        else if (type == "Reward")
        {
            Debug.Log(type);
            //run animation reward
            thisAnimator.Play("reward", -1, 0f);
        }
    }
}
