using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    public string[] objectTagBad;
    public string[] objectTagGood;
    public ObjectPooler objectPooler;
    public int maxNumGood = 1;
    public int initialAmount = 11;
    public Vector3 startPosition = new Vector3(0, -2f, 0);
    public Vector3 distancePosition = new Vector3(0, 1, 0);
    public Vector3 blockOffset = new Vector3(-1, 0, 0);
    bool spawnedBad = true;
    bool sideToSpawn;
    int randNum;
    int splitChance = 50;
    int numOfGood = 0;

    void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
        InitialSpawn();
        numOfGood = maxNumGood;
    }

    void Update()
    {
        if (TouchController.Tapped)
        {
            if (!spawnedBad && numOfGood == 0)
            {
                sideToSpawn = SideSelection(splitChance);
                randNum = Random.Range(0, objectTagBad.Length);
                Spawn(objectTagBad[randNum], transform.position);
                spawnedBad = true;
            }
            else
            {
                if (spawnedBad)
                {
                    numOfGood = Random.Range(0, maxNumGood) + 1;
                }
                sideToSpawn = SideSelection(splitChance);
                randNum = Random.Range(0, objectTagGood.Length);
                Spawn(objectTagGood[randNum], transform.position);
                spawnedBad = false;
                numOfGood--;
            }
        }
    }

    void InitialSpawn()
    {
        for (int i = 0; i < initialAmount; i++)
        {
            if (!spawnedBad && numOfGood == 0)
            {
                sideToSpawn = SideSelection(splitChance);
                randNum = Random.Range(0, objectTagBad.Length);
                Spawn(objectTagBad[randNum], startPosition);
                spawnedBad = true;
            }
            else
            {
                if (spawnedBad)
                {
                    numOfGood = Random.Range(0, maxNumGood) + 1;
                }
                sideToSpawn = SideSelection(splitChance);
                randNum = Random.Range(0, objectTagGood.Length);
                Spawn(objectTagGood[randNum], startPosition);
                spawnedBad = false;
                numOfGood--;
            }
            startPosition += distancePosition;
        }
    }

    void Spawn(string block, Vector3 position)
    {
        switch (block)
        {
            case "Good":
                if (sideToSpawn)
                {
                    objectPooler.SpawnFromPool(block, position, Quaternion.Euler(0, 180, 0));
                }
                else
                {
                    objectPooler.SpawnFromPool(block, position, Quaternion.identity);
                }
                break;
            case "Bad":
                if (sideToSpawn)
                {
                    objectPooler.SpawnFromPool(block, position - blockOffset, Quaternion.Euler(0, 180, 0));
                }
                else
                {
                    objectPooler.SpawnFromPool(block, position + blockOffset, Quaternion.identity);
                }
                break;
            default:
                break;
        }
    }

    bool SideSelection(int percProbTrue)
    {
        int side = Random.Range(0, 99);
        switch (side < percProbTrue)
        {
            case true:
                return true;
            default:
                return false;
        }
    }
}