using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "3379105";
#elif UNITY_ANDROID
    private string gameId = "3379104";
#endif

    public static bool buttonCanBeEnabled;
    public string myPlacementId = "rewardedVideo";
    static bool canReward = true;

    void Start()
    {
        // Initialize the Ads listener and service:
        canReward = true;
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);
    }

    public void ShowAd()
    {
        FindObjectOfType<AudioController>().PlayAudio(0);
        Debug.Log("Showed");
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            buttonCanBeEnabled = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            RewardUser();
            buttonCanBeEnabled = false;
            FindObjectOfType<ButtonController>().RestartButton();
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public static void RewardUser()
    {
        if (canReward)
        {
            canReward = false;
            FindObjectOfType<PlayerController>().AchievementEffects("Reward");
            ScoreCounter.cookie += 5;
            ScoreCounter.CookieSet();
        }
    }
}
