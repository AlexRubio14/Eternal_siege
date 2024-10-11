using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] private int currentMoney;
    [SerializeField] private TextMeshProUGUI moneyText;

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
        SetText();
    }

    public void SetText()
    {
        moneyText.text = currentMoney.ToString();
    }

    public int GetCurrentMoney()
    {
        return currentMoney;
    }

    public void SetInitMoney(int quantity)
    {
        currentMoney += quantity;
    }
}
