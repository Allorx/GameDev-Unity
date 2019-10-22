using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public float slowMotionScale = 0.5f;
    public float slowMotionDuration = 2f;
    public static bool gamePlay = false;
    bool gameReset = false;

    void Awake () {
        Time.timeScale = 1f;
        StartCoroutine (ResetGame ());
    }

    void Update () {
        CheckGameStart ();
    }

    void CheckGameStart () {
        if (gameReset && TouchController.Tapped) {
            gamePlay = true;
            gameReset = false;
        }
    }

    public void EndGame () {
        StartCoroutine (EndLevel ());
    }

    IEnumerator EndLevel () {
        gamePlay = false;
        TouchController.touchControllerActive = false;
        Time.timeScale = slowMotionScale;
        yield return new WaitForSecondsRealtime (slowMotionDuration);

        RestartLevel ();
    }

    public void RestartLevel () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

    IEnumerator ResetGame () {
        yield return new WaitForSecondsRealtime (1f);
        gameReset = true;
        TouchController.touchControllerActive = true;
    }

    void ResetAllScores () {
        //WARNING RESETS SCORE DATA !!!!!!!!!!!!!!!!!
        PlayerPrefs.SetInt ("HighScore", 0);
    }
}