﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    public int[] achievementScore;
    public int[] achievementStars;
    public Sprite[] achievementImages;
    public GameObject scrollViewObject;

    public static void CheckAchievements(int score)
    {
        FindObjectOfType<AchievementController>().RunCheck(score);
    }

    public static bool CheckStarUnlock(int characterNumber)
    {
        if (FindObjectOfType<AchievementController>().RunStarCheck(characterNumber))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    bool RunStarCheck(int charNum)
    {
        if (achievementStars[charNum] <= ScoreCounter.stars && !PlayerController.charUnlocked[charNum] && achievementStars[charNum] > 0)
        {
            Unlock(charNum);
            ScoreCounter.stars -= achievementStars[charNum];
            ScoreCounter.StarSet();
            SaveUnlocks();
            return true;
        }
        else
        {
            return false;
        }
    }

    void RunCheck(int score)
    {
        StartCoroutine(AchievementCheck(score));
    }

    IEnumerator AchievementCheck(int score)
    {
        //Returns true if character can be unlocked
        for (int i = 0; i < PlayerController.characterList.Length; i++)
        {
            if (achievementScore[i] <= score && !PlayerController.charUnlocked[i] && achievementScore[i] > 0)
            {
                Unlock(i);
            }
        }
        SaveUnlocks();
        yield return null;
    }

    void Unlock(int i)
    {
        PlayerController.charUnlocked[i] = true;
        PlayerController.newcharUnlock[i] = true;
        FindObjectOfType<PlayerController>().AchievementEffects("UnlockedCharacter");
    }

    void SaveUnlocks()
    {
        FindObjectOfType<PlayerController>().SaveCharacterUnlock();
        if (scrollViewObject.activeInHierarchy)
        {
            scrollViewObject.GetComponentInChildren<PopulateGrid>().Repopulate();
        }
    }
}
