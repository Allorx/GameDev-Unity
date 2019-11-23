using System.Collections;
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
    public static bool[] charUnlocked;
    public static bool[] newcharUnlock;
    public GameObject achievementObj;
    public GameObject newName;
    public Animator heartAnim;
    static Animator animControl;
    bool achievementEnabled = false;
    bool movementActivated = false;
    bool playerCanMove = true;
    bool flippedLeft;
    Vector3 scaleMove = new Vector3(-0.1f, 0.1f, 1f);
    Vector3 scaleIncrease = new Vector3(0.1f, 0.1f, 0.1f);
    Vector3 maxScale = new Vector3(2f, 2f, 2f);

    void Awake()
    {
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
        flippedLeft = true;
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
        if (playerCanMove)
        {
            //Flip sides
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
            //Increase size over score
            if (ScoreCounter.score > 0 && playerTransform.localScale.x < maxScale.x && ScoreCounter.score % 50 == 0)
            {
                playerTransform.localScale += scaleIncrease;
                playerTransform.localPosition -= scaleMove;
                deathTransform.y -= scaleMove.y;
            }
            transform.RotateAround(playerFlipPoint.transform.position, Vector3.up, 180);
            flippedLeft = true;
            EatAnim();
        }
        else if (TouchController.TappedRight && flippedLeft)
        {
            //Increase size over score
            if (ScoreCounter.score > 0 && playerTransform.localScale.x < maxScale.x && ScoreCounter.score % 50 == 0)
            {
                playerTransform.localScale += scaleIncrease;
                playerTransform.localPosition += scaleMove;
                deathTransform.y -= scaleMove.y;
            }
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
}