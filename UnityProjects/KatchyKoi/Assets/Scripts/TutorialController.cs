using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    void Update()
    {
        if (PlayerController.movementActivated)
        {
            gameObject.SetActive(false);
        }
    }
}
