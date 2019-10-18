using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float slowMotionScale = 0.5f;
    public float slowMotionDuration = 2f;
    public static bool gamePlay = false;
    float timeLimit = 5f;
    bool gameReset = false;

    void Awake()
    {
        Time.timeScale = 1f;
        StartCoroutine(ResetGame());
    }

    void Update()
    {
        Timer(TouchController.Tapped);
        CheckGameStart();
    }

    IEnumerator Timer(bool reset){
        if(reset){

        }
        if(ScoreCounter.score >= 1 && ScoreCounter.score % 10 == 0){
            timeLimit - 1/ScoreCounter.score;
        }
        yield return WaitForSecondsRealtime(timeLimit);
        GameManager.EndGame();
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
        gamePlay = false;

        Time.timeScale = slowMotionScale;
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameReset = true;
    }

    void ResetAllScores()
    {
        //WARNING RESETS SCORE DATA !!!!!!!!!!!!!!!!!
        PlayerPrefs.SetInt("HighScore", 0);
    }
}
