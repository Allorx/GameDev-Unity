using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    //BlockController variables are not public as easier to change all prefabs here
    BlockSpawn spawnBlocks;
    public Collider bx;

    float fallSpeed = 10.0f;

    Vector3 newPosition;
    Vector3 fallDirection = new Vector3(0, -1, 0);

    void Awake()
    {
        gameObject.transform.rotation = Random.rotation;
    }

    void Update()
    {
        if (GameManager.gamePlay == true)
        {
            BlockFall();
        }
    }

    void BlockFall()
    {
        newPosition = transform.position;
        transform.position = newPosition + fallDirection * fallSpeed * Time.deltaTime;
    }
}