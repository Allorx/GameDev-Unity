﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitController : MonoBehaviour
{
    public Image healthBar;
    public float timeCounter = 3f;
    public float timeLimit = 3f;
    public float starWaitTime = 5f;
    bool activatedTimer = false;
    bool starCollectFinished = false;
    Coroutine timerRoutine;

    void Update()
    {
        healthBar.fillAmount = timeCounter / timeLimit;

        if (!activatedTimer && GameManager.gamePlay && ButtonController.startScore)
        {
            activatedTimer = true;
            timerRoutine = StartCoroutine(Timer());
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

    public void TimePause()
    {
        starCollectFinished = false;
        StopTimer();
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(starWaitTime);
        starCollectFinished = true;
        if (starCollectFinished)
        {
            timerRoutine = StartCoroutine(Timer());
            PostProcessController.EndPostProcess();
        }
    }

    IEnumerator Timer()
    {
        if (GameManager.gamePlay)
        {
            while (timeCounter > 0)
            {
                timeCounter -= Time.deltaTime;
                yield return null;
            }
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void StopTimer()
    {
        StopCoroutine(timerRoutine);
    }
}