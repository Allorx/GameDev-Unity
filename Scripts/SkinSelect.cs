using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelect : MonoBehaviour
{
    public int characterNumber;
    static GameObject thisObject;

    void Awake()
    {
        thisObject = gameObject;
    }

    public void CharacterSelect()
    {
        if (PlayerController.charUnlocked[characterNumber])
        {
            PlayerController.SelectCharacter(characterNumber);
        }
        else
        {
            AchievementController.CheckStarUnlock(characterNumber);
        }
    }
}
