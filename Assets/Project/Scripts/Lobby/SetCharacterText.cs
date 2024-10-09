using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCharacterText : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> charactersInformation;

    public void SetText(int index)
    {
        if(index == 0)
        {
            charactersInformation[0].text = "ARCHER";
            charactersInformation[1].text = "HP: ";
            charactersInformation[2].text = "Armor: ";
            charactersInformation[3].text = "Damage: ";
            charactersInformation[4].text = "HP Regen: ";
            charactersInformation[5].text = "Attk Speed: ";
            charactersInformation[6].text = "Pick Up Radius: ";

            charactersInformation[7].text = " ";
            charactersInformation[8].text = " ";
        }
        else
        {
            charactersInformation[0].text = "TANK";
            charactersInformation[1].text = "HP: ";
            charactersInformation[2].text = "Armor: ";
            charactersInformation[3].text = "Damage: ";
            charactersInformation[4].text = "HP Regen: ";
            charactersInformation[5].text = "Attk Speed: ";
            charactersInformation[6].text = "Pick Up Radius: ";

            charactersInformation[7].text = " ";
            charactersInformation[8].text = " ";
        }

    }
}
