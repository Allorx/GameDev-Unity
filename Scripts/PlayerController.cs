using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameObject playerFlipPoint;
    public Collider2D collisionBox;
    public static GameObject player;
    Animator animControl;
    bool movementActivated = false;
    bool playerCanMove = true;
    bool flippedLeft;

    void Awake () {
        animControl = GetComponentInChildren<Animator> ();
    }

    void Start () {
        TouchController.touchControllerActive = true;
        flippedLeft = true;
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
            EatAnim ();
        }
        if (TouchController.TappedLeft && !flippedLeft) {
            transform.RotateAround (playerFlipPoint.transform.position, Vector3.up, 180);
            flippedLeft = true;
            EatAnim ();
        } else if (TouchController.TappedRight && flippedLeft) {
            transform.RotateAround (playerFlipPoint.transform.position, Vector3.up, 180);
            flippedLeft = false;
            EatAnim ();
        }
    }

    void EatAnim () {
        animControl.Play ("eat", -1, 0f);
    }

    IEnumerator ActivateMovement () {
        yield return new WaitForSecondsRealtime (0.2f);
        playerCanMove = true;
    }

    public static void DestroyPlayer () {
        player.GetComponentInChildren<PlayerController> ().PlayDeathEffects ();

    }

    void PlayDeathEffects () {
        collisionBox.enabled = false;
    }
}