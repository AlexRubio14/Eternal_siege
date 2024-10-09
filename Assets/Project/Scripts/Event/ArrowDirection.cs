using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    [SerializeField] private float corner;
    private Vector2 objective;
    private void Update()
    {
        if(gameObject.activeSelf && PlayersManager.instance.GetPlayersList().Count > 0)
        {
            Vector2 direction = (objective - (Vector2)PlayersManager.instance.GetPlayersList()[0].transform.localPosition).normalized;

            Vector3 _position = (Vector2)PlayersManager.instance.GetPlayersList()[0].transform.localPosition + (direction * corner);

            transform.up = -direction;

            transform.position = _position;
        }
    }

    public void SetObjective(Vector2 _objective)
    {
        objective = _objective;
    }
}
