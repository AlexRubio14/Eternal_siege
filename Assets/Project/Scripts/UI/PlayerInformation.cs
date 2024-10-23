using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider HpSlider;

    [SerializeField] private GameObject[] image;
    [SerializeField] private int index;

    private void Start()
    {
        levelText.text = "0";
        HpSlider.value = 1;
        image[PlayerLobbyManager.instance.GetTypeCharacter()[index]].SetActive(true);
    }

    public void SetLevelText(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetHPBar(float hp)
    {
        if(hp < 0)
        {
            hp = 0;
        }
        HpSlider.value = hp;
    }

    public float GetValue()
    {
        return HpSlider.value;
    }
}
