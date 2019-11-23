using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public TextMesh scoreText;
    public TextMesh cookieText;
    static TextMesh cookieTextStatic;
    public GameObject highscore;
    public Color highscoreScoreColour;
    public static int cookie;
    public static int score = 0;
    public static int lastScore = 0;
    bool firstSave = false;
    bool resetScoreOnce = true;
    int flashTimes = 5;
    Color originalScoreColour;
    bool highscoreActivated;

    void Awake()
    {
        cookieTextStatic = cookieText;
        ButtonController.startScore = false;
        score = 0;
        lastScore = 0;
        scoreText.characterSize = 5f;
        originalScoreColour = scoreText.color;

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
        if (PlayerPrefs.HasKey("Cookie"))
        {
            cookie = PlayerPrefs.GetInt("Cookie");
        }
        CookieSet();
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
        if (!highscoreActivated && ScoreCounter.score > PlayerPrefs.GetInt("HighScore") && firstSave)
        {
            // Make it flash a few times and disappear
            highscoreActivated = true;
            highscore.SetActive(true);
            StartCoroutine(HighscoreEffects());
            PlayerPrefs.SetInt("HighScore", ScoreCounter.score);
        }
        else if (!GameManager.gamePlay)
        {
            PlayerPrefs.SetInt("HighScore", ScoreCounter.score);
            PlayerPrefs.SetInt("FirstSave", 1);
        }
    }

    IEnumerator HighscoreEffects()
    {
        scoreText.color = Color.white;
        yield return new WaitForSecondsRealtime(0.1f);
        while (flashTimes > 0)
        {
            scoreText.color = originalScoreColour;
            flashTimes -= 1;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        scoreText.color = highscoreScoreColour;
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

    public static void CookieSet()
    {
        PlayerPrefs.SetInt("Cookie", ScoreCounter.cookie);
        cookieTextStatic.text = cookie.ToString();
    }
}