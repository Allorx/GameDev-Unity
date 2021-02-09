using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    public string[] objectTagBlock;
    public string[] objectTagStar;
    public string[] objectTagCookie;
    public ObjectPooler objectPooler;
    public int maxNumGood = 1;
    public int initialAmount = 11;
    public Vector3[] startPosition;
    public Vector3 distancePosition = new Vector3(0, 1, 0);
    public Vector3 blockOffset = new Vector3(-1, 0, 0);
    public Vector3 starOffset = new Vector3(-1, 0, 0);
    int numOfGood = 0;

    //new
    public float timeToSpawn = 2f;
	public float timeBetweenSpawn = 0.25f;

    void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
        //InitialSpawn();
        numOfGood = maxNumGood;
    }

    void FixedUpdate()
    {
		if(PlayerController.movementActivated && Time.timeSinceLevelLoad >= timeToSpawn){
            Spawn(objectTagBlock[0], startPosition[Random.Range(0,5)]);
			timeToSpawn = Time.timeSinceLevelLoad + timeBetweenSpawn;
		}
    }

/*
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
                    numOfGood = Random.Range(0, maxNumGood) + 2;
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
*/
    void Spawn(string block, Vector3 position)
    {
        switch (block)
        {
            case "Block":
                objectPooler.SpawnFromPool(block, position, Quaternion.identity);
                break;
            case "Star":
                objectPooler.SpawnFromPool(block, position + starOffset, Quaternion.identity);
                break;
            case "Cookie":
                objectPooler.SpawnFromPool(block, position + starOffset, Quaternion.identity);            
                break;
            default:
                break;
        }
    }
}