using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Camera")]
    private bool[] cornerCheck;
    Camera cam;
    Vector2 cameraBorder;

    private void Start()
    {
        cam = Camera.main;
        cameraBorder = new Vector2(cam.aspect * cam.orthographicSize, cam.orthographicSize);

        cornerCheck = new bool[4];
        for (int i = 0; i < cornerCheck.Length; i++)
        {
            cornerCheck[i] = false;
        }
    }

    public void CreateEnemy(int index)
    {
        GameObject newEnemy = Instantiate(EnemyManager.instance.GenerateEnemy(index));

        newEnemy.GetComponent<Enemy>().SetTarget(PlayersManager.instance.GetPlayersList());

        Vector3 spawnPosition = GenerateRandomPosition(newEnemy);

        spawnPosition += cam.transform.position;
        spawnPosition.z = 0;

        newEnemy.transform.localPosition = spawnPosition;
        newEnemy.transform.SetParent(this.gameObject.transform);

        EnemyManager.instance.GetEnemies().Add(newEnemy);
    }

    private Vector3 GenerateRandomPosition(GameObject enemy)
    {
        Vector3 position = new Vector3();
        float f = 0;
        BoxCollider2D boxCollder2D = enemy.GetComponent<BoxCollider2D>();

        if (UnityEngine.Random.value > 0.5f) // random left right
        {
            position.x = SetFirstPosition(0, 1, cameraBorder.x);
            f = SetSecondPosition(2, 3);
            position.y = cameraBorder.y * f;

            if (f < 0)
                position.y -= boxCollder2D.bounds.size.y;
            else
                position.y += boxCollder2D.bounds.size.y;
        }
        else // random top down
        {
            position.y = SetFirstPosition(2, 3, cameraBorder.y);
            f = SetSecondPosition(0, 1);
            position.x = cameraBorder.x * f;

            if (f < 0)
                position.x -= boxCollder2D.bounds.size.x;
            else
                position.x += boxCollder2D.bounds.size.x;
        }

        position.z = 0;

        return position;
    }

    private float SetFirstPosition(int index1, int index2, float _cameraBorder)
    {
        if (cornerCheck[index1]) //left (0), down(2)
            return UnityEngine.Random.Range(_cameraBorder / 2, _cameraBorder);
        else if (cornerCheck[index2]) //rigt (1), top(3)
            return UnityEngine.Random.Range(-_cameraBorder, -(_cameraBorder / 2));
        else
            return UnityEngine.Random.Range(-_cameraBorder, _cameraBorder);
    }

    private float SetSecondPosition(int index1, int index2)
    {
        if (cornerCheck[index1]) //left(0), down(2)
            return 1;
        else if (cornerCheck[index2]) // right(1), top(3)
            return -1;
        else
            return UnityEngine.Random.value > 0.5f ? -1f : 1f;
    }

    public void SetCornerCheck(bool state, int _index)
    {
        cornerCheck[_index] = state;
    }
}
