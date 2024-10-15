using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private bool paused;

    private void Awake()
    {
        if(instance != null || instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);

        paused = false;
    }

    public void PauseTime()
    {
        paused = true;
    }

    public void ResumeTime()
    {
        paused = false;
    }

    public bool GetPaused()
    {
        return paused;
    }
}
