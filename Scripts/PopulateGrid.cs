using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject prefabSkins;
    Sprite[] skins;

    void OnEnable()
    {
        skins = PlayerController.staticCharacterList;
        Populate();
    }

    public void Repopulate()
    {
        Populate();
    }

    void Populate()
    {
        GameObject newObj; // Create GameObject instance

        for (int i = 0; i < skins.Length; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            if (PlayerController.charUnlocked[i])
            {
                newObj = Instantiate(prefabSkins, transform);
                newObj.GetComponentInChildren<SkinSelect>().characterNumber = i;
                newObj.GetComponentInChildren<Image>().sprite = skins[i];
            }
            else
            {
                newObj = Instantiate(prefabSkins, transform);
                newObj.GetComponentInChildren<SkinSelect>().characterNumber = i;
                newObj.GetComponentInChildren<Image>().sprite = skins[i];
                newObj.GetComponentInChildren<Image>().color = Color.black;
            }
        }
    }
}
