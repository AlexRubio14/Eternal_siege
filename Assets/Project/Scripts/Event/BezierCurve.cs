using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField] private float timeDivier;
    private List<Vector3> points;
    private float time;
    private float _magnitude;
    private float currentPlayersIn;
    private bool carring;

    [SerializeField] private float maxDesactiveTime;
    private float desactiveTime;

    private void Start()
    {
        time = 0;
        desactiveTime = 0;
        currentPlayersIn = 0;
        carring = false;
    }

    private void Update()
    {
        if(points.Count == 4)
        {
            if(carring)
            {
                MoveTrain();
            }
            desactiveTime += Time.deltaTime;
            if(desactiveTime > maxDesactiveTime) 
            { 
                Destroy(gameObject);
            }
        }
    }

    private void MoveTrain()
    {
        transform.position = ((Mathf.Pow((1 - time), 3) * points[0]) +
        (3 * Mathf.Pow((1 - time), 2) * time * points[1]) +
        (3 * (1 - time) * Mathf.Pow(time, 2) * points[2]) +
        (Mathf.Pow(time, 3) * points[3]));

        time += Time.deltaTime * timeDivier;

        if ((transform.position - points[0]).magnitude > _magnitude)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            currentPlayersIn++;
            carring = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision is CapsuleCollider2D)
        {
            currentPlayersIn--;
            if (currentPlayersIn <= 0)
            {
                carring = false;
            }
        }
    }
    public void SetPoint(Vector3 point)
    {
        points.Add(point);
        if (points.Count == 4)
        {
            _magnitude = (points[3] - points[0]).magnitude;
        }
    }

    public void SetInitPosition(Vector3 point)
    {
        points = new List<Vector3>();
        points.Add(point);
    }

}
