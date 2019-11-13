using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float slowMotionScale = 0.5f;
    public float slowMotionDuration = 2f;
    public static bool gamePlay = false;
    bool gameReset = false;
    public static bool gameEnded = false;

    void Awake()
    {
        //ResetAllScores(); //WARNING RESETS SCORE DATA AND PREFERENCES !!!!!!!!!!!!!!!!!
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
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
        PlayerController.DestroyPlayer();
        PostProcessController.EndPostProcess();
        //FindObjectOfType<TimeLimitController>().StopTimer();

        gameEnded = true;
        gamePlay = false;
        TouchController.touchControllerActive = false;
        Time.timeScale = slowMotionScale;
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        FindObjectOfType<ButtonController>().RestartButton();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ResetGame(bool withButtons)
    {
        if (!withButtons)
        {
            FindObjectOfType<ButtonController>().HideButton();
        }

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