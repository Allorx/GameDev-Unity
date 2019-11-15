using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelect : MonoBehaviour
{
    public int characterNumber;

    public void CharacterSelect()
    {
        if (PlayerController.charUnlocked[characterNumber])
        {
            PlayerController.SelectCharacter(characterNumber);
        }
    }
}
