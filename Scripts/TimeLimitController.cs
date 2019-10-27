using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitController : MonoBehaviour
{
    public Image healthBar;
    public float timeCounter = 3f;
    public float timeLimit = 3f;
    bool activatedTimer = false;

    void Update()
    {
        healthBar.fillAmount = timeCounter / timeLimit;
        if (!activatedTimer && GameManager.gamePlay)
        {
            activatedTimer = true;
            StartCoroutine(Timer());
        }
        if (TouchController.Tapped && timeLimit > 0.5 && ScoreCounter.score >= 1 && ScoreCounter.score % 20 == 0)
        {
            timeLimit -= 20f / ScoreCounter.score;
            timeCounter -= 20f / ScoreCounter.score;
            Debug.Log("Decreased time" + timeLimit);
        }
        if (TouchController.Tapped && timeCounter < timeLimit - timeCounter)
        {
            timeCounter += 0.25f;
        }
        else if (TouchController.Tapped && timeCounter < timeLimit)
        {
            timeCounter = timeLimit;
        }
    }

    IEnumerator Timer()
    {
        while (timeCounter > 0 && GameManager.gamePlay)
        {
            timeCounter -= Time.deltaTime;
            yield return null;
        }
        FindObjectOfType<GameManager>().EndGame();
        PlayerController.DestroyPlayer();
    }
}