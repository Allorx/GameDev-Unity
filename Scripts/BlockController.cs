using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    float fallAmount = 1;
    static Vector3 fallVector;

    void Start()
    {
        fallVector = new Vector3(0, fallAmount, 0);
    }

    void Update()
    {
        if (TouchController.Tapped)
        {
            transform.position -= fallVector;
        }
    }
}
