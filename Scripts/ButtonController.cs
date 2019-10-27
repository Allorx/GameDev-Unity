using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject button;
    public GameObject tutorial;
    public static bool startScore = false;

    public void HideButton()
    {
        tutorial.SetActive(true);
        startScore = true;
        button.SetActive(false);
    }
}