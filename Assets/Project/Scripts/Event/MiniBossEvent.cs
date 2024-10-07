using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossEvent : LevelEvent
{
    private void Start()
    {
        Initialize();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void EventUpdate()
    {
        LevelManager.instance.SpawnMiniBoss();
        Destroy(gameObject);
        arrow.SetActive(false);
    }
}
