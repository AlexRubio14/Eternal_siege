using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPointController : MonoBehaviour
{
    private Vector3 posToGo;

    private void Awake()
    {
        posToGo = Vector3.zero;
    }

    void Update()
    {
        CalculatePosToGo();
        Move();
    }

    private void CalculatePosToGo()
    {
        if (PlayersManager.instance.GetPlayersList().Count == 0)
            return;

        posToGo = Vector3.zero;

        foreach(GameObject player in PlayersManager.instance.GetPlayersList())
        {
            posToGo += player.transform.position;
        }

        posToGo /= PlayersManager.instance.GetPlayersList().Count;
    }

    private void Move()
    {
        transform.position = posToGo;
    }
}
