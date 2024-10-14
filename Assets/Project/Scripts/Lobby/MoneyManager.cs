using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] private int currentMoney;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }
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
