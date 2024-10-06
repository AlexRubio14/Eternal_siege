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

            Vector2 screenPosition = Camera.main.WorldToScreenPoint(PlayersManager.instance.GetPlayersList()[0].transform.localPosition + (Vector3)direction * 20f);

            screenPosition.x = Mathf.Clamp(screenPosition.x, corner, Screen.width - corner);
            screenPosition.y = Mathf.Clamp(screenPosition.y, corner, Screen.height - corner);

            transform.up = -direction;

            transform.position = screenPosition;
        }
    }

    public void SetObjective(Vector2 _objective)
    {
        objective = _objective;
    }
}
