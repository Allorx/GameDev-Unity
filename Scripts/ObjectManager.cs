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
    GameObject starParticles;
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
            if (isStar)
            {
                gameObject.SetActive(false);
            }
            else
            {
                ParticleController.ParticleFall(spriteColour[2]);
                gameObject.SetActive(false);
            }
        }
        else if (colliderInfo.gameObject.tag == "Player")
        {
            if (isStar)
            {
                ScoreCounter.stars++;
                timeLimitControl.TimePause();
                PostProcessController.StartPostProcess();
                StarParticleController.StartStars(); ;
                gameObject.SetActive(false);
            }
            else
            {
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }
}