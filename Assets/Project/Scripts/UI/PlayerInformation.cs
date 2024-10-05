using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation : MonoBehaviour
{
    public static PlayerInformation instance;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider HpSlider;
    [SerializeField] Slider ExpSlider;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        levelText.text = "0";
        ExpSlider.value = 0;
        HpSlider.value = 600;
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

    public void SetExperienceBar(float exp)
    {
        ExpSlider.value = exp;
    }
}
