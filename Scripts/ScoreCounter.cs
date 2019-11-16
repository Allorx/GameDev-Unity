using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public TextMesh scoreText;
    public TextMesh starText;
    static TextMesh starTextStatic;
    public static int stars;
    public static int score = 0;
    public static int lastScore = 0;
    bool firstSave = false;
    bool resetScoreOnce = true;

    void Awake()
    {
        starTextStatic = starText;
        ButtonController.startScore = false;
        score = 0;
        lastScore = 0;
        scoreText.characterSize = 5f;

        if (PlayerPrefs.HasKey("FirstSave"))
        {
            firstSave = true;
        }
        else
        {
            firstSave = false;
        }
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        if (PlayerPrefs.HasKey("Stars"))
        {
            stars = PlayerPrefs.GetInt("Stars");
        }
        StarSet();
    }

    void Update()
    {
        if (ButtonController.startScore && score != 0)
        {
            // Make transition from Highscore to 0
            ScoreReset();
            scoreText.text = score.ToString();
        }
        ScoreCheck();
    }

    void ScoreReset()
    {
        gameObject.SetActive(true);
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
            DecreasecharacterSize();
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
        }
        else if (!GameManager.gamePlay)
        {
            PlayerPrefs.SetInt("HighScore", ScoreCounter.score);
            PlayerPrefs.SetInt("FirstSave", 1);
        }
    }

    void DecreasecharacterSize()
    {
        if (score > 9999)
        {
            scoreText.characterSize = 4;
        }
        else if (score > 99)
        {
            scoreText.characterSize = 4.5f;
        }
    }

    public static void StarSet()
    {
        PlayerPrefs.SetInt("Stars", ScoreCounter.stars);
        starTextStatic.text = stars.ToString();
    }
}