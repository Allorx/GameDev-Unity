using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {
    public GameObject button;
    public static bool startScore = false;

    public void HideButton () {
        startScore = true;
        button.SetActive (false);
    }
}