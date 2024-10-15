using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private void Update()
    {
        if (TimeManager.instance.GetPaused() == 0)
            return;

        int minutes = LevelManager.instance.GetTime() / 60;
        int seconds = LevelManager.instance.GetTime() - (minutes * 60);
        GetComponent<TextMeshProUGUI>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
