using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTimer : MonoBehaviour
{
    [SerializeField] private float maxTime;
    private float time;

    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        if(gameObject.activeSelf)
        {
            time += Time.deltaTime;
            if(time > maxTime)
            {
                gameObject.SetActive(false);
                time = 0;
            }
        }
    }
}
