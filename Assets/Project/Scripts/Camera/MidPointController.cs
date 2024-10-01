using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPointController : MonoBehaviour
{
    private Vector3 posToGo = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
