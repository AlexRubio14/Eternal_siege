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
            charactersInformation[1].text = "HP: 500";
            charactersInformation[2].text = "Armor: 5";
            charactersInformation[3].text = "Damage: 20";
            charactersInformation[4].text = "HP Regen: 0";
            charactersInformation[5].text = "Attk Speed: 2";
            charactersInformation[6].text = "Pick Up Radius: 5";

            charactersInformation[7].text = "Summons a large arrow that pierces enemies causing great damage";
            charactersInformation[8].text = "Summons eight arrows around the archer that damage enemies hitted";

        }
        else
        {
            charactersInformation[0].text = "TANK";
            charactersInformation[1].text = "HP: 750";
            charactersInformation[2].text = "Armor: 20";
            charactersInformation[3].text = "Damage: 10";
            charactersInformation[4].text = "HP Regen: 0";
            charactersInformation[5].text = "Attk Speed: 1";
            charactersInformation[6].text = "Pick Up Radius: 5";

            charactersInformation[7].text = "Charges against enemies violently, damaging them and negating the damage received";
            charactersInformation[8].text = "Summons a circular shield that enemies cannot pass through";

        }

    }
}
