using System.Collections;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public SpriteRenderer spriteRend;
    public Color[] spriteColour;
    public Sprite[] goodBlockSprite;
    public Sprite[] badBlockSprite;
    public bool isGoodBlock = false;
    public bool isBadBlock = false;
    public bool isStar = false;
    public bool isCookie = false;
    TimeLimitController timeLimitControl;
    bool canBeDestroyed = true;
    int randomNumber;

    void Awake()
    {
        timeLimitControl = FindObjectOfType<TimeLimitController>();
    }

    void OnEnable()
    {
        if (isGoodBlock)
        {
            randomNumber = Random.Range(0, goodBlockSprite.Length);
            spriteRend.sprite = goodBlockSprite[randomNumber];
        }
        else if (isBadBlock)
        {
            randomNumber = Random.Range(0, badBlockSprite.Length);
            spriteRend.sprite = badBlockSprite[randomNumber];
        }
    }

    void OnTriggerEnter2D(Collider2D colliderInfo)
    {
        if (colliderInfo.gameObject.tag == "DestroyVolume" && canBeDestroyed)
        {
            gameObject.SetActive(false);
        }
        else if (colliderInfo.gameObject.tag == "Player" && !GameManager.gameEnded)
        {
            if (isGoodBlock)
            {
                ScoreCounter.score++;
                ParticleController.ParticleFall(spriteColour[2]);
                gameObject.SetActive(false);
            }
            else if (isBadBlock)
            {
                FindObjectOfType<GameManager>().EndGame();
            }
            else if (isStar)
            {
                FindObjectOfType<AudioController>().PlayAudio(2);
                timeLimitControl.TimePause();
                PostProcessController.StartPostProcess();
                //StarParticleController.StartStars();
                gameObject.SetActive(false);
            }
            else if (isCookie)
            {
                FindObjectOfType<AudioController>().PlayAudio(3);
                FindObjectOfType<PlayerController>().HeartAnimation();
                ScoreCounter.cookie++;
                ScoreCounter.CookieSet();
                gameObject.SetActive(false);
            }
        }
    }
}