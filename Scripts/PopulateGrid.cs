﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject prefabSkins;
    Sprite[] skins;

    void Awake()
    {
        skins = PlayerController.staticCharacterList;
        Populate();
    }

    void Populate()
    {
        GameObject newObj; // Create GameObject instance

        for (int i = 0; i < skins.Length; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj = Instantiate(prefabSkins, transform);
            newObj.GetComponentInChildren<SkinSelect>().characterNumber = i;
            newObj.GetComponentInChildren<Image>().sprite = skins[i];
        }

    }
}