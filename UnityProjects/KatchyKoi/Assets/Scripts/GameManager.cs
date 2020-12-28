using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject title;
    public float slowMotionScale = 0.5f;
    public float slowMotionDuration = 2f;
    public static bool gamePlay = false;
    bool gameReset = false;
    public static bool gameEnded = false;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        FindObjectOfType<PlayerController>().ResetAchievementEffects();
        if (gameEnded)
        {
            RestartLevel();
            gameEnded = false;
        }
        else
        {
            StartCoroutine(ResetGame(false));
        }
    }

    void Update()
    {
        //ResetAllScores();//WARNING RESETS SCORE DATA AND PREFERENCES !!!!!!!!!!!!!!!!!
        CheckGameStart();
    }

    void CheckGameStart()
    {
        if (gameReset && TouchController.Tapped)
        {
            gamePlay = true;
            gameReset = false;
        }
    }

    public void EndGame()
    {
        StartCoroutine(EndLevel());
    }

    IEnumerator EndLevel()
    {
        gameEnded = true;
        gamePlay = false;

        PlayerController.DestroyPlayer();
        PostProcessController.EndPostProcess();
        CameraController.shakeDuration = 2f;
        FindObjectOfType<TimeLimitController>().StopTimer();
        FindObjectOfType<AudioController>().PlayAudio(5);
        FindObjectOfType<AudioController>().StopMusic();

        TouchController.touchControllerActive = false;
        Time.timeScale = slowMotionScale;
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        AchievementController.CheckAchievements(ScoreCounter.score);
        FindObjectOfType<ButtonController>().RestartButton();
    }

    public void RestartLevel()
    {
        FindObjectOfType<ButtonController>().HideButton(true);
        StartCoroutine(RebuildLevel());
    }

    IEnumerator RebuildLevel()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ResetGame(bool withButtons)
    {
        if (!withButtons)
        {
            FindObjectOfType<ButtonController>().HideButton(false);
        }
        title.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        TouchController.touchControllerActive = true;
        gameReset = true;
    }

    void ResetAllScores()
    {
        //WARNING RESETS SCORE DATA AND PREFERENCES !!!!!!!!!!!!!!!!!
        PlayerPrefs.DeleteAll();
    }
}