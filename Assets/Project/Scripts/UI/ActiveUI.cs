using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUI : MonoBehaviour
{
    private void Update()
    {
        if (PlayersManager.instance.GetPlayersList().Count == 0)
            return;

        if(PlayersManager.instance.GetPlayersList().Count > 0)
        {
            ActiveChild(0);
            ActiveChild(1);
        }
        if (PlayersManager.instance.GetPlayersList().Count > 1)
        {
            ActiveChild(1);
        }
    }

    private void ActiveChild(int index)
    {
        transform.GetChild(index).gameObject.SetActive(true);
    }

}
