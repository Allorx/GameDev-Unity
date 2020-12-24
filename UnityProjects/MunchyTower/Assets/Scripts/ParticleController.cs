using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    static int spawnPoint = 0;
    static Animator[] animControl;
    static SpriteRenderer[] spriteRend;

    void Awake()
    {
        animControl = GetComponentsInChildren<Animator>();
        spriteRend = GetComponentsInChildren<SpriteRenderer>();
    }

    public static void ParticleFall(Color colour)
    {
        //Selects animating point to start so multiple can play and overlap
        //Adds colour
        switch (spawnPoint)
        {
            case 0:
                animControl[0].Play("ParticleFall", -1, 0f);
                spriteRend[0].color = colour;
                spawnPoint = 1;
                break;
            case 1:
                animControl[1].Play("ParticleFall", -1, 0f);
                spriteRend[1].color = colour;
                spawnPoint = 2;
                break;
            case 2:
                animControl[2].Play("ParticleFall", -1, 0f);
                spriteRend[2].color = colour;
                spawnPoint = 3;
                break;
            case 3:
                animControl[3].Play("ParticleFall", -1, 0f);
                spriteRend[3].color = colour;
                spawnPoint = 0;
                break;
        }
    }
}
