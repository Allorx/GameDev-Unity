﻿using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    public Sprite[] goodBlockSprite;
    public Sprite[] badBlockSprite;
    public bool isGoodBlock = false;
    public bool isBadBlock = false;
    bool canBeDestroyed = true;

    void OnEnable()
    {
        if (isGoodBlock)
        {
            spriteRend.sprite = goodBlockSprite[Random.Range(0, goodBlockSprite.Length)];
        }
        else if (isBadBlock)
        {
            spriteRend.sprite = badBlockSprite[Random.Range(0, badBlockSprite.Length)];
        }
    }

    void OnTriggerEnter2D(Collider2D colliderInfo)
    {
        if (colliderInfo.gameObject.tag == "DestroyVolume" && canBeDestroyed)
        {
            Debug.Log("deact");
            gameObject.SetActive(false);
        }
        else if (colliderInfo.gameObject.tag == "Player")
        {
            PlayerController.DestroyPlayer();
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}