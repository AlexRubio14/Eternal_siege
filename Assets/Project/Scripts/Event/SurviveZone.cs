using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurviveZone : MonoBehaviour
{
    [SerializeField] private float maxTimeComplete;
    [SerializeField] private float maxTimeLose;
    private float timeComplete;
    private float timeLose;

    private int currentPlayersIn;

    private enum surviveState { In, Out };
    private surviveState state;

    private void Start()
    {
        currentPlayersIn = 0;
        timeComplete = 0;
        timeLose = maxTimeLose;
        state = surviveState.Out;
    }

    private void Update()
    {
        switch (state)
        {
            case surviveState.In:
                Surviving();
                Losing();
                break;
            case surviveState.Out:
                Losing();
                break;
        }

    }

    private void Surviving()
    {
        timeComplete += Time.deltaTime;
        transform.GetChild(0).transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeComplete / maxTimeComplete);
        if(timeComplete > maxTimeComplete) 
        {
            //Victoria
            Destroy(gameObject);
        }
    }

    private void Losing()
    {
        timeLose -= Time.deltaTime;
        int minutes = (int)timeLose / 60;
        int seconds = (int)timeLose - (minutes * 60);
        if (timeLose < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider othern)
    {
        if (othern.CompareTag("Player") && othern is CapsuleCollider)
        {
            currentPlayersIn++;
            state = surviveState.In;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other is CapsuleCollider)
        {
            currentPlayersIn--;
            if (currentPlayersIn <= 0)
            {
                state = surviveState.Out;
            }
        }
    }
}
