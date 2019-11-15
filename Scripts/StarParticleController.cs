using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticleController : MonoBehaviour
{
    public Animator starAnimator;

    public static void StartStars()
    {
        FindObjectOfType<StarParticleController>().PlayStarParticles();
    }

    void PlayStarParticles()
    {
        starAnimator.Play("particles", -1, 0f);
    }

}
