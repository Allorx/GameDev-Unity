using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
    public TextMesh scoreText;
    public static int score = 0;
    public static int lastScore = 0;
    bool firstSave = false;
    bool resetScoreOnce = true;

    void Awake () {
        lastScore = 0;
        scoreText.characterSize = 1.5f;

        if (PlayerPrefs.GetInt ("FirstSave") == 1) {
            firstSave = true;
        }
        if (PlayerPrefs.HasKey ("HighScore")) {
            score = PlayerPrefs.GetInt ("HighScore");
        }
    }

    void Update () {
        if (GameManager.gamePlay && score != 0) {
            // Make transition from Highscore to 0
            ScoreReset ();
            scoreText.text = score.ToString ();
        }
        ScoreCheck ();
    }

    void ScoreReset () {
        while (resetScoreOnce) {
            resetScoreOnce = false;
            score = 0;
        }
    }

    void ScoreCheck () {
        if (score > lastScore) {
            HighScoreCheck ();
            DecreasecharacterSize ();
            scoreText.text = score.ToString ();
            lastScore = score;
        }
    }

    void HighScoreCheck () {
        if (ScoreCounter.score > PlayerPrefs.GetInt ("HighScore") && firstSave) {
            // Make it flash a few times and disappear - maybe change graphics and make fire different colour
            PlayerPrefs.SetInt ("HighScore", ScoreCounter.score);
        } else if (!GameManager.gamePlay) {
            PlayerPrefs.SetInt ("HighScore", ScoreCounter.score);
            PlayerPrefs.SetInt ("FirstSave", 1);
        }
    }

    void DecreasecharacterSize () {
        if (score > 9999) {
            scoreText.characterSize = 1;
        } else if (score > 99) {
            scoreText.characterSize = 1.2f;
        }
    }
}