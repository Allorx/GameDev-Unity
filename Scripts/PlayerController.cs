using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameObject playerFlipPoint;
    public Collider2D collisionBox;
    public Animator animatorControl;
    public static GameObject player;
    bool movementActivated = false;
    bool playerCanMove = true;
    bool flippedLeft;

    void Start () {
        TouchController.touchControllerActive = true;
        flippedLeft = true;
        //animatorControl.enabled = false;
        player = gameObject;
    }

    void Update () {
        if (playerCanMove) {
            Flip ();
        } else if (!movementActivated && GameManager.gamePlay) {
            movementActivated = true;
            StartCoroutine (ActivateMovement ());
        }
    }

    void Flip () {
        if (TouchController.Tapped) {
            ScoreCounter.score++;
        }
        if (TouchController.TappedLeft && !flippedLeft) {
            transform.RotateAround (playerFlipPoint.transform.position, Vector3.up, 180);
            flippedLeft = true;
        } else if (TouchController.TappedRight && flippedLeft) {
            transform.RotateAround (playerFlipPoint.transform.position, Vector3.up, 180);
            flippedLeft = false;
        }
    }

    IEnumerator ActivateMovement () {
        yield return new WaitForSecondsRealtime (0.2f);
        playerCanMove = true;
    }

    public static void DestroyPlayer () {
        player.GetComponentInChildren<PlayerController> ().PlayDeathEffects ();
        TouchController.touchControllerActive = false;
    }

    void PlayDeathEffects () {
        //animatorControl.enabled = true;
        collisionBox.enabled = false;
    }
}