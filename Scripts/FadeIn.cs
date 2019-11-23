using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public Color fadeRate = new Color(0, 0, 0, 0.1f);
    public SpriteRenderer spriteRend;

    void Start()
    {
        StartCoroutine(FadeSprite());
    }

    IEnumerator FadeSprite()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        while (spriteRend.color.a > 0)
        {
            spriteRend.color -= fadeRate;
            yield return null;
        }
    }
}
