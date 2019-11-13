﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject playerFlipPoint;
    public Collider2D collisionBox;
    public static GameObject player;
    public Transform playerTransform;
    public Vector3 deathTransform = new Vector3(0, -0.1f, 0);
    public AnimatorOverrideController[] character;
    public Sprite[] characterSprite;
    public static AnimatorOverrideController[] characterList;
    public static Sprite[] staticCharacterList;
    static Animator animControl;
    bool movementActivated = false;
    bool playerCanMove = true;
    bool flippedLeft;

    void Awake()
    {
        animControl = GetComponentInChildren<Animator>();
        characterList = character;
        staticCharacterList = characterSprite;
        PlayerController.SelectCharacter(0);
    }

    void Start()
    {
        flippedLeft = true;
        player = gameObject;
    }

    static void SaveSkin(int number)
    {
        PlayerPrefs.SetInt("CharacterNumber", number);
    }

    public static void SelectCharacter(int intCharacter)
    {
        animControl.runtimeAnimatorController = characterList[intCharacter];
        animControl.Play("idle", -1, 0f);
        SaveSkin(intCharacter);
    }

    void Update()
    {
        if (playerCanMove)
        {
            Flip();
        }
        else if (!movementActivated && GameManager.gamePlay)
        {
            movementActivated = true;
            StartCoroutine(ActivateMovement());
        }
    }

    void Flip()
    {
        if (TouchController.Tapped)
        {
            ScoreCounter.score++;
            EatAnim();
        }
        if (TouchController.TappedLeft && !flippedLeft)
        {
            transform.RotateAround(playerFlipPoint.transform.position, Vector3.up, 180);
            flippedLeft = true;
            EatAnim();
        }
        else if (TouchController.TappedRight && flippedLeft)
        {
            transform.RotateAround(playerFlipPoint.transform.position, Vector3.up, 180);
            flippedLeft = false;
            EatAnim();
        }
    }

    void EatAnim()
    {
        animControl.Play("eat", -1, 0f);
    }

    IEnumerator ActivateMovement()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        playerCanMove = true;
    }

    public static void DestroyPlayer()
    {
        player.GetComponentInChildren<PlayerController>().PlayDeathEffects();
    }

    void PlayDeathEffects()
    {
        animControl.Play("death", -1, 0f);
        playerTransform.position += deathTransform;
        collisionBox.enabled = false;
    }
}