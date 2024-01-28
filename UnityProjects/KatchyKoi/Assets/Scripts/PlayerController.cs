﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //new
    public Rigidbody2D rb;
    public Color[] spriteColour;
    public float slowRange = 4;
    public Vector2 slowEffect = new Vector2(0, 0.01f);
    public float deathRange = 4;
    public float crossMidVelocityReduction = 0.5f;
    public float forceMultiplier = 1.0f;
    public float amountGravity = 5;
    Vector2 forceVector = new Vector2(1,0);
    static Vector2 gravityDirection = new Vector2(1,0);
    float previousX = 0f;
    float currentX;

    //old
    public Collider2D collisionBox;
    public static GameObject player;
    public Transform playerTransform;
    public Vector3 deathTransform = new Vector3(0, -0.1f, 0);
    public AnimatorOverrideController[] character;
    public Sprite[] characterSprite;
    public static AnimatorOverrideController[] characterList;
    public static Sprite[] staticCharacterList;
    public static bool[] charUnlocked;
    public static bool[] newcharUnlock;
    public GameObject achievementObj;
    public GameObject newName;
    public Animator heartAnim;
    static Animator animControl;
    bool achievementEnabled = false;
    public static bool movementActivated = false;
    bool playerCanMove = true;
    Vector3 scaleLeft = new Vector3(-0.05f, 0.05f, 0f);
    Vector3 scaleRight = new Vector3(0.05f, 0.05f, 0f);
    Vector3 scaleIncrease = new Vector3(0.1f, 0.1f, 0.1f);
    Vector3 maxScale = new Vector3(2f, 2f, 2f);
    int thisColour = -1;
    SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        //new
        Physics2D.gravity = gravityDirection*0;
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //old
        ResetAchievementEffects();
        animControl = GetComponentInChildren<Animator>();
        characterList = character;
        staticCharacterList = characterSprite;
        charUnlocked = new bool[characterList.Length];
        newcharUnlock = new bool[characterList.Length];
        LoadCharacterUnlock();
        LoadSkin();
    }

    void Start()
    {
        player = gameObject;
    }

    static void SaveSkin(int number)
    {
        PlayerPrefs.SetInt("CharacterNumber", number);
    }

    static void LoadSkin()
    {
        SelectCharacter(PlayerPrefs.GetInt("CharacterNumber"));
    }

    public static void SelectCharacter(int intCharacter)
    {
        if (charUnlocked[intCharacter])
        {
            animControl.runtimeAnimatorController = characterList[intCharacter];
            animControl.Play("idle", -1, 0f);
            SaveSkin(intCharacter);
        }
    }

    void Update()
    {
        CheckColour();
        if (playerCanMove)
        {
            //Jump against gravity
            CrossMidCheck();
            Jump();
        }
        else if (!movementActivated && GameManager.gamePlay)
        {
            movementActivated = true;
            StartCoroutine(ActivateMovement());
        }
    }

    void Jump()
    {
        //new
        if(rb.position.x > slowRange || rb.position.x < -slowRange){
            rb.position -= slowEffect*Time.deltaTime;
        }
        if(rb.position.y < -deathRange){
            FindObjectOfType<GameManager>().EndGame();
        }
        else if (TouchController.Tapped)
        {
            //JumpAnim();
            FindObjectOfType<AudioController>().PlayAudio(1);
            if(rb.position.x > 0f && rb.position.x > 0.05f){
                rb.AddForce(-forceVector*forceMultiplier*0.05f, ForceMode2D.Impulse);
            }
            else if(rb.position.x < 0f && rb.position.x < -0.05f){
                rb.AddForce(forceVector*forceMultiplier*0.05f, ForceMode2D.Impulse);
            }
        }
        /*old
        if (TouchController.Tapped)
        {
            EatAnim();
        }
        if (TouchController.TappedLeft)
        {
            if (!flippedLeft)
            {
                transform.RotateAround(playerFlipPoint.transform.position, Vector3.up, 180);
                flippedLeft = true;
                EatAnim();
            }
            ScaleMove(false);
        }
        else if (TouchController.TappedRight)
        {
            if (flippedLeft)
            {
                transform.RotateAround(playerFlipPoint.transform.position, Vector3.up, 180);
                flippedLeft = false;
                EatAnim();
            }
            ScaleMove(true);
        }
        */
    }
    /*
    void ScaleMove(bool Right)
    {
        if (ScoreCounter.score > 0 && playerTransform.localScale.x < maxScale.x && ScoreCounter.score % 50 == 0)
        {
            playerTransform.localScale += scaleIncrease;
            if (Right)
            {
                playerTransform.localPosition += scaleRight;
                deathTransform.y -= scaleRight.y;
            }
            else
            {
                playerTransform.localPosition += scaleLeft;
                deathTransform.y -= scaleLeft.y;
            }
            Debug.Log("Scale");
        }
    }
    */

    void CheckColour(){
        if(thisColour != ColourManager.currentColour){
            thisColour = ColourManager.currentColour;
            m_SpriteRenderer.color = spriteColour[thisColour];
        }
    }

    void JumpAnim()
    {
        animControl.Play("eat", -1, 0f);
    }

    IEnumerator ActivateMovement()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        playerCanMove = true;
        //new
        SwitchGravity();
    }

    public static void DestroyPlayer()
    {
        player.GetComponentInChildren<PlayerController>().PlayDeathEffects();
    }

    void OnTriggerEnter2D(Collider2D colliderInfo)
    {
        if (colliderInfo.gameObject.tag == "DestroyVolume")
        {
             playerTransform.position += deathTransform;
        }
    }

    void PlayDeathEffects()
    {
        playerCanMove = false;
        movementActivated = false;
        Physics2D.gravity = new Vector2(0,-1)*amountGravity;
        animControl.Play("death", -1, 0f);
        collisionBox.enabled = false;
    }

    public void SaveCharacterUnlock()
    {
        for (int i = 0; i < character.Length; i++)
        {
            PlayerPrefs.SetInt("unlock" + i.ToString(), charUnlocked[i] ? 1 : 0);
            PlayerPrefs.SetInt("firstUnlock" + i.ToString(), newcharUnlock[i] ? 1 : 0);
        }
    }

    void LoadCharacterUnlock()
    {
        for (int i = 0; i < character.Length; i++)
        {
            charUnlocked[i] = PlayerPrefs.GetInt("unlock" + i.ToString()) == 1 ? true : false;
            newcharUnlock[i] = PlayerPrefs.GetInt("firstUnlock" + i.ToString()) == 1 ? true : false;
        }
        // unlock default character
        charUnlocked[0] = true;
        // disable new name on default character
        newcharUnlock[0] = false;
    }

    public void AchievementEffects(string type)
    {
        //Say what achievement
        //Activate effects
        achievementObj.SetActive(true);
        achievementObj.GetComponentInChildren<AchievementDisplay>().Display(type);
        newName.SetActive(true);
        achievementEnabled = true;
    }

    public void ResetAchievementEffects()
    {
        if (achievementEnabled)
        {
            achievementEnabled = false;
            achievementObj.SetActive(false);
        }
    }

    public void HeartAnimation()
    {
        heartAnim.Play("heart", -1, 0f);
    }

//New
    void CrossMidCheck(){ 
        currentX = rb.transform.position.x;
        if((currentX > 0 && previousX < 0) || (currentX < 0 && previousX > 0)){
            ReduceVelocity();
            SwitchGravity();
        }
        previousX = currentX;
    }

    void ReduceVelocity(){
        playerCanMove = false;
        StartCoroutine(ActivateMovement());
        rb.velocity = rb.velocity*crossMidVelocityReduction;
    }

    void SwitchGravity(){
        if(rb.position.x > 0f){
            Physics2D.gravity = gravityDirection*amountGravity;
        }
        else if(rb.position.x < 0f){
            Physics2D.gravity = -gravityDirection*amountGravity;     
        }
    }
}