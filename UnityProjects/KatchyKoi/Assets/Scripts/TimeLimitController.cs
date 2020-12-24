using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitController : MonoBehaviour
{
    public GameObject starParticles;
    public Color starBar;
    public Image healthBar;
    public float timeCounter = 3f;
    public float timeLimit = 3f;
    public float starWaitTime = 5f;
    bool activatedTimer = false;
    Coroutine timerRoutine;
    Coroutine waitTimerRoutine;
    Color originalColour;

    void Awake()
    {
        originalColour = healthBar.color;
    }

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
        StopTimer();
        waitTimerRoutine = StartCoroutine(WaitTime());
    }

    IEnumerator FlashHealth()
    {
        healthBar.color = Color.red;
        timeCounter = 3;
        yield return new WaitForSecondsRealtime(0.1f);
        timeCounter = 0;
    }

    IEnumerator WaitTime()
    {
        starParticles.SetActive(true);
        healthBar.color = starBar;
        yield return new WaitForSecondsRealtime(starWaitTime);
        timerRoutine = StartCoroutine(Timer());
        PostProcessController.EndPostProcess();
        healthBar.color = originalColour;
        starParticles.SetActive(false);
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
            //StartCoroutine(FlashHealth());
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void StopTimer()
    {
        StopCoroutine(timerRoutine);
        if (waitTimerRoutine != null)
        {
            StopCoroutine(waitTimerRoutine);
            healthBar.color = originalColour;
        }
    }
}