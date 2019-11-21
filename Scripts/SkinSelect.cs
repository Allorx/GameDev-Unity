using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelect : MonoBehaviour
{
    public int characterNumber;
    static GameObject thisObject;
    public GameObject achievementImage;
    public GameObject newName;
    public GameObject highlight;
    public GameObject highlightRed;

    void Awake()
    {
        thisObject = gameObject;
        if (PlayerController.charUnlocked[characterNumber])
        {
            achievementImage.SetActive(false);
        }
        else
        {
            achievementImage.SetActive(true);
            achievementImage.GetComponentInChildren<Image>().sprite = FindObjectOfType<AchievementController>().achievementImages[characterNumber];
        }
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
            if (!AchievementController.CheckStarUnlock(characterNumber))
            {
                StartCoroutine(EffectDisabled());
            }
        }
    }

    IEnumerator EffectDisabled()
    {
        highlightRed.GetComponentInChildren<Image>().color = Color.red;
        highlightRed.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        highlightRed.SetActive(false);
    }

    public void DisableNewName()
    {
        newName.SetActive(false);
    }
}
