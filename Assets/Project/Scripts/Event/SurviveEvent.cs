using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurviveEvent : LevelEvent
{
    [Header("Survive")]
    [SerializeField] private GameObject surviveZone;
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
        GameObject _surviveZone = Instantiate(surviveZone);
        _surviveZone.transform.position = transform.position;
        Destroy(gameObject);
        arrow.SetActive(false);
    }
}
