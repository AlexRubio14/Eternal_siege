using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private int costSum;
    [SerializeField] private TextMeshProUGUI _text;
    private bool maxed;

    private void Start()
    {
        maxed = false;
        _text.text = cost.ToString();
    }
    public void UpgradeAbility()
    {
        if (MoneyManager.instance.GetCurrentMoney() > cost && !maxed) 
        {
            Buy();
            ActiveAbility();
        }

    }

    private void Buy()
    {
        MoneyManager.instance.SetInitMoney(-cost);
        cost += costSum;
        _text.text = cost.ToString();
    }

    private void ActiveAbility()
    {
        GameObject bar = transform.GetChild(1).gameObject;
        for(int i = 0; i < bar.transform.childCount; i++)
        {
            if(!bar.transform.GetChild(i).gameObject.activeSelf)
            {
                bar.transform.GetChild(i).gameObject.SetActive(true);
                MoneyManager.instance.SetText();
                if (bar.transform.GetChild(bar.transform.childCount - 1).gameObject.activeSelf)
                {
                    maxed = true;
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX";
                    _text.text = " ";
                }
                else
                    return;
            }
        }
    }
}
