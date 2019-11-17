using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject[] button;
    public GameObject tutorial;
    public GameObject skinUI;
    public GameObject newName;
    public static bool startScore = false;

    public void HideButton()
    {
        tutorial.SetActive(true);
        skinUI.SetActive(false);
        startScore = true;
        for (int i = 0; i < button.Length; i++)
        {
            button[i].SetActive(false);
        }
    }

    public void OpenSkinMenu()
    {
        newName.SetActive(false);
        skinUI.SetActive(true);
        button[1].SetActive(false);
    }

    public void RestartButton()
    {
        button[0].SetActive(true);
    }
}