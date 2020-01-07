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
        GameObject[] GOS = GameObject.FindGameObjectsWithTag("Icon");
        foreach (GameObject go in GOS)
        {
            Destroy(go);
        }
        for (int i = 0; i < skins.Length; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            if (PlayerController.charUnlocked[i])
            {
                newObj = Instantiate(prefabSkins, transform);
                newObj.GetComponentInChildren<SkinSelect>().characterNumber = i;
                newObj.GetComponentInChildren<SkinSelect>().DisableAchievementText();
                newObj.GetComponentInChildren<SkinSelect>().DisableQuestionMark();
                newObj.GetComponentInChildren<Image>().sprite = skins[i];
                if (!PlayerController.newcharUnlock[i])
                {
                    newObj.GetComponentInChildren<SkinSelect>().DisableNewName();
                }
            }
            else
            {
                newObj = Instantiate(prefabSkins, transform);
                newObj.GetComponentInChildren<SkinSelect>().characterNumber = i;
                newObj.GetComponentInChildren<Image>().sprite = skins[i];
                newObj.GetComponentInChildren<Image>().color = Color.black;
                newObj.GetComponentInChildren<SkinSelect>().DisableNewName();
                if (FindObjectOfType<AchievementController>().achievementCookie[i] != 0)
                {
                    newObj.GetComponentInChildren<SkinSelect>().DisableQuestionMark();
                    newObj.GetComponentInChildren<TextMesh>().text = FindObjectOfType<AchievementController>().achievementCookie[i].ToString();
                }
                else
                {
                    newObj.GetComponentInChildren<SkinSelect>().DisableAchievementText();
                }
            }
        }
        ScrollFocus.FocusOnObject(GetComponentInChildren<RectTransform>());
    }
}
