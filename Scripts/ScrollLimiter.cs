using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollLimiter : MonoBehaviour
{
    public ScrollRect rectScroll;
    public float startClamp = 0.0f;
    public float endClamp = 1f;

    public void OnValueChanged(Vector2 value)
    {
        rectScroll.horizontalScrollbar.value = Mathf.Clamp(value.y, startClamp, endClamp);
    }
}
