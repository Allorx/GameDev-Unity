using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    void Update()
    {
        if (ScoreCounter.score > 0 && TouchController.Tapped)
        {
            gameObject.SetActive(false);
        }
    }
}
