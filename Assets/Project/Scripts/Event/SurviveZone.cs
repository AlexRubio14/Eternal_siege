using System.Collections;
using System.Collections.Generic;
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
        timeLose = 0;
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
        if(timeComplete > maxTimeComplete) 
        { 
            //Victoria
            Destroy(gameObject);
        }
    }

    private void Losing()
    {
        timeLose += Time.deltaTime;
        if (timeLose > maxTimeLose)
        {
            //Derrota
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            currentPlayersIn++;
            state = surviveState.In;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            currentPlayersIn--;
            if (currentPlayersIn <= 0)
            {
                state = surviveState.Out;
            }
        }
    }
}
