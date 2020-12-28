using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    float fallAmount = 2f;
    static Vector3 fallVector;

    void Start()
    {
        fallVector = new Vector3(0, fallAmount, 0);
    }

    void FixedUpdate()
    {
        if(PlayerController.movementActivated){
            transform.position -= fallVector*Time.deltaTime;
        }
    }
}