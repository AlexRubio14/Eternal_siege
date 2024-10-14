using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyMesage : MonoBehaviour
{
    private void Start()
    {
        SetText();
    }
    public void SetText()
    {
        GetComponent<TextMeshProUGUI>().text = MoneyManager.instance.GetCurrentMoney().ToString();
    }
}
