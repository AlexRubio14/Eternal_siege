using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurviveZone : MonoBehaviour
{
    [SerializeField] private float maxTimeComplete;
    [SerializeField] private float maxTimeLose;
    [SerializeField] private GameObject timer = null;
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
        if(timer != null)
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
        else
        {
            GameObject canvas = GameObject.Find("Canvas");
            timer = canvas.transform.GetChild(5).gameObject;
            timer.SetActive(true);
        }

    }

    private void Surviving()
    {
        timeComplete += Time.deltaTime;
        transform.GetChild(0).transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeComplete / maxTimeComplete);
        if(timeComplete > maxTimeComplete) 
        {
            //Victoria
            timer.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void Losing()
    {
        timeLose -= Time.deltaTime;
        int minutes = (int)timeLose / 60;
        int seconds = (int)timeLose - (minutes * 60);
        timer.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "The event ends in: " +  minutes.ToString("00") + ":" + seconds.ToString("00");
        if (timeLose < 0)
        {
            //Derrota
            timer.SetActive(false);
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
