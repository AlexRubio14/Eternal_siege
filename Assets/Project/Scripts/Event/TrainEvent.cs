using System.Collections.Generic;
using UnityEngine;

public class TrainEvent : LevelEvent
{
    [Header("Train")]
    [SerializeField] private GameObject train;
    [SerializeField] private float _magnitude;


    private void Start()
    {
        Initialize();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void EventUpdate()
    {
        GameObject _train = Instantiate(train);
        _train.transform.position = transform.position;
        _train.GetComponent<BezierCurve>().SetInitPosition(transform.position);

        List<Vector3> result = GenerateLastPoints(_train);
        _train.GetComponent<BezierCurve>().SetPoint(result[1]);
        _train.GetComponent<BezierCurve>().SetPoint(result[2]);
        _train.GetComponent<BezierCurve>().SetPoint(result[0]);

        Destroy(gameObject);
        arrow.SetActive(false);
    }

    private List<Vector3> GenerateLastPoints(GameObject _train)
    {
        List<Vector3> result = new List<Vector3>
        {
            _train.transform.position
        };

        Vector2 radius;
        radius.x = _train.GetComponent<SpriteRenderer>().bounds.size.x;
        radius.y = _train.GetComponent<SpriteRenderer>().bounds.size.y;

        while ((_train.transform.position - result[0]).magnitude < _magnitude)
        {
            result[0] = new Vector3(UnityEngine.Random.Range(corner[0].position.x + radius.x, corner[1].position.x - radius.x),
                UnityEngine.Random.Range(corner[0].position.y + radius.y, corner[1].position.y - radius.y),0);
        }
        GenerateIntermediatePoint(_train.transform.position, result[0], result, radius);
        GenerateIntermediatePoint(result[0], result[1], result, radius);

        return result;
    }

    private void GenerateIntermediatePoint(Vector3 position1, Vector3 position2, List<Vector3> result, Vector2 radius)
    {
        if ((position1.x - result[0].x) > (position1.y - result[0].y))
        {
            result.Add(new Vector3(UnityEngine.Random.Range(position1.x, position2.x),
                UnityEngine.Random.Range(corner[0].position.y + radius.y, corner[1].position.y - radius.y), 0));
        }
        else
        {
            result.Add(new Vector3(UnityEngine.Random.Range(corner[0].position.x + radius.x, corner[1].position.x - radius.x),
                UnityEngine.Random.Range(position1.y, position2.y), 0));
        }

    }

}
