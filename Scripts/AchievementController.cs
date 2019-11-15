using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour
{
    public int[] achievementScore;
    public int[] achievementStars;

    public static void CheckAchievements(int score, int stars)
    {
        FindObjectOfType<AchievementController>().RunCheck(score, stars);
    }

    void RunCheck(int score, int stars)
    {
        StartCoroutine(AchievementCheck(score, stars));
    }

    IEnumerator AchievementCheck(int score, int stars)
    {
        //Returns true if character can be unlocked
        for (int i = 0; i < PlayerController.characterList.Length; i++)
        {
            if (achievementScore[i] <= score && !PlayerController.charUnlocked[i] && achievementScore[i] > 0)
            {
                Unlock(i);
            }
            if (achievementStars[i] <= stars && !PlayerController.charUnlocked[i] && achievementStars[i] > 0)
            {
                Unlock(i);
            }
        }
        SaveUnlocks();
        yield return null;
    }

    void Unlock(int i)
    {
        FindObjectOfType<PlayerController>().AchievementEffects();
        PlayerController.charUnlocked[i] = true;
    }

    void SaveUnlocks()
    {
        FindObjectOfType<PlayerController>().SaveCharacterUnlock();
    }
}
