using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    [SerializeField] private float corner;
    [SerializeField] private GameObject centerPoint;
    private Vector3 objective;
    private void Update()
    {
        if(gameObject.activeSelf && PlayersManager.instance.GetPlayersList().Count > 0)
        {
            Vector3 midPoint = new Vector3(centerPoint.transform.position.x, 0,
                centerPoint.transform.position.z);

            Vector3 direction = (objective - midPoint).normalized;

            Vector3 _position = midPoint + (direction * corner);

            transform.up = new Vector3(-direction.x, -direction.z, 0);

            transform.position = new Vector3(_position.x, 0, _position.z);
        }
    }

    public void SetObjective(Vector3 _objective)
    {
        objective = _objective;
        objective.y = 0;
    }
}
