using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "3369706";
#elif UNITY_ANDROID
    private string gameId = "3369707";
#endif

    public static bool buttonCanBeEnabled;
    public string myPlacementId = "rewardedVideo";

    void Start()
    {
        // Set interactivity to be dependent on the Placement’s status:
        if (Advertisement.IsReady(myPlacementId))
        {
            buttonCanBeEnabled = true;
        }

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    public void ShowAd()
    {
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
        FindObjectOfType<PlayerController>().AchievementEffects("Reward");
        ScoreCounter.stars += 5;
        ScoreCounter.StarSet();
    }
}
