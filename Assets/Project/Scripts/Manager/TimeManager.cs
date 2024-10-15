using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private float paused;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);

        paused = 1f;
    }

    public void PauseTime()
    {
        paused = 0.0f;
    }

    public void ResumeTime()
    {
        paused = 1f;
    }

    public float GetPaused()
    {
        return paused;
    }
}
