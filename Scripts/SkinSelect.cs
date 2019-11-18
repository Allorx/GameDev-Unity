using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelect : MonoBehaviour
{
    public int characterNumber;
    static GameObject thisObject;
    public GameObject newName;
    public GameObject highlight;

    void Awake()
    {
        thisObject = gameObject;
    }

    void Update()
    {
        if (characterNumber == PlayerPrefs.GetInt("CharacterNumber"))
        {
            highlight.SetActive(true);
        }
        else
        {
            highlight.SetActive(false);
        }
    }

    public void CharacterSelect()
    {
        if (PlayerController.charUnlocked[characterNumber])
        {
            PlayerController.SelectCharacter(characterNumber);
            PlayerController.newcharUnlock[characterNumber] = false;
            DisableNewName();
        }
        else
        {
            AchievementController.CheckStarUnlock(characterNumber);
        }
    }

    public void DisableNewName()
    {
        newName.SetActive(false);
    }
}
