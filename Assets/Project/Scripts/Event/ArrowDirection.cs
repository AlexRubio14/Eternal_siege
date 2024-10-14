using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    [SerializeField] private float corner;
    private Vector3 objective;
    private void Update()
    {
        if(gameObject.activeSelf && PlayersManager.instance.GetPlayersList().Count > 0)
        {
            Vector3 playerPosition = new Vector3(PlayersManager.instance.GetPlayersList()[0].transform.position.x, 0,
                PlayersManager.instance.GetPlayersList()[0].transform.position.z);

            Vector3 direction = (objective - playerPosition).normalized;

            Vector3 _position = playerPosition + (direction * corner);

            transform.up = -direction;

            transform.position = new Vector3(_position.x, 0, _position.z);
        }
    }

    public void SetObjective(Vector3 _objective)
    {
        objective = _objective;
        objective.y = 0;
    }
}
