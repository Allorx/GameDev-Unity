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
    }

    public void StartGame () {
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
        FindObjectOfType<ButtonController> ().HideButton ();
        yield return new WaitForSecondsRealtime (0.5f);
        TouchController.touchControllerActive = true;
        gameReset = true;
    }

    void ResetAllScores () {
        //WARNING RESETS SCORE DATA !!!!!!!!!!!!!!!!!
        PlayerPrefs.SetInt ("HighScore", 0);
    }
}