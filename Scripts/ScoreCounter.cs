using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static TextMeshProUGUI scoreText;
    public GameObject highScoreText;
    public static int score = 0;
    public static int lastScore = 0;
    bool firstSave = false;
    bool resetScoreOnce = true;

    void Awake()
    {
        lastScore = 0;
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        highScoreText.SetActive(false);
        scoreText.fontSize = 180;

        if (PlayerPrefs.GetInt("FirstSave") == 1)
        {
            firstSave = true;
        }
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
    }

    void FixedUpdate()
    {
        if (GameManager.gamePlay && score != 0)
        {
            // Make transition from Highscore to 0
            ScoreReset();
            scoreText.text = score.ToString();
        }
        ScoreCheck();
    }

    void ScoreReset()
    {
        while (resetScoreOnce)
        {
            resetScoreOnce = false;
            score = 0;
        }
    }

    void ScoreCheck()
    {
        if (score > lastScore)
        {
            HighScoreCheck();
            DecreaseFontSize();
            scoreText.text = score.ToString();
            lastScore = score;
        }
    }

    void HighScoreCheck()
    {
        if (ScoreCounter.score > PlayerPrefs.GetInt("HighScore") && firstSave)
        {
            // Make it flash a few times and disappear - maybe change graphics and make fire different colour
            PlayerPrefs.SetInt("HighScore", ScoreCounter.score);
            highScoreText.SetActive(true);
        }
        else if (!GameManager.gamePlay)
        {
            PlayerPrefs.SetInt("HighScore", ScoreCounter.score);
            PlayerPrefs.SetInt("FirstSave", 1);
        }
    }

    void DecreaseFontSize()
    {
        if (score > 9999)
        {
            scoreText.fontSize = 100;
        }
        else if (score > 99)
        {
            scoreText.fontSize = 120;
        }
    }
}
