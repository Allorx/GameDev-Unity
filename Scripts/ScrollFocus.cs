using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollFocus : MonoBehaviour
{
    static float characterWidth = 180;

    public static void FocusOnObject(RectTransform rectTrans)
    {
        rectTrans.transform.localPosition -= new Vector3(PlayerPrefs.GetInt("CharacterNumber") * characterWidth + 20, 0, 0);
        Debug.Log(PlayerPrefs.GetInt("CharacterNumber") * characterWidth);
    }
}
