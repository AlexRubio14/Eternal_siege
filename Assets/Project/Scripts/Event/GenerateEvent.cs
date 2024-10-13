using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateEvent : MonoBehaviour
{
    public static GenerateEvent instance;

    [SerializeField] private GameObject[] eventGeneration;
    [SerializeField] private Transform[] corner;
    [SerializeField] private GameObject arrow;
    [SerializeField] private TextMeshProUGUI _text;

    public bool[] eventActive;

    private Vector2 radius;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;

        eventActive = new bool[eventGeneration.Length];
        for(int i = 0; i < eventGeneration.Length; ++i)
        {
            eventActive[i] = false;
        }
    }

    public void SpawnEvent(int index)
    {
        GameObject _event = Instantiate(eventGeneration[index]);
        _event.transform.SetParent(transform, true);

        radius.x = _event.GetComponent<SpriteRenderer>().bounds.size.x;
        radius.y = _event.GetComponent<SpriteRenderer>().bounds.size.z;

        _event.transform.localPosition = new Vector3(
            UnityEngine.Random.Range(corner[0].position.x + radius.x, corner[1].position.x - radius.x),
            -2.5f,
            UnityEngine.Random.Range(corner[0].position.y + radius.y, corner[1].position.y - radius.y)
            );

        _event.GetComponent<LevelEvent>().SetText(_text);
        _event.GetComponent<LevelEvent>().SetArrow(arrow);
        _event.GetComponent<LevelEvent>().SetCorner(corner);

        arrow.SetActive(true);
        arrow.GetComponent<ArrowDirection>().SetObjective(_event.transform.position);
    }
}
