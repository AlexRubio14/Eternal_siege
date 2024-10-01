using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private float spawnTimer;
    [SerializeField] private List<GameObject> player;

    Camera cam;
    float timer;
    Vector2 cameraBorder;

    private void Start()
    {
        cam = Camera.main;
        timer = 0;
        cameraBorder = new Vector2(cam.aspect * cam.orthographicSize, cam.orthographicSize);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            SpwanEnemy();
            timer = spawnTimer;
        }
    }

    private void SpwanEnemy()
    {
        GameObject _enemy = GenerateEnemy();
        CreateEnemy(_enemy);
    }

    private Vector3 GenerateRandomPosition(GameObject enemy)
    {
        Vector3 position = new Vector2();

        SpriteRenderer enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();

        float f = UnityEngine.Random.value > 0.5f? -1f : 1f;
        if(UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-cameraBorder.x, cameraBorder.x);
            position.y = cameraBorder.y * f;
            if(f < 0)
                position.y -= enemySpriteRenderer.bounds.size.y;        
            else
                position.y += enemySpriteRenderer.bounds.size.y;

        }
        else
        {
            position.y = UnityEngine.Random.Range(-cameraBorder.y, cameraBorder.y);
            position.x = cameraBorder.x * f;
            if (f < 0)
                position.x -= enemySpriteRenderer.bounds.size.x;
            else
                position.x += enemySpriteRenderer.bounds.size.x;
        }

        position.z = 0;

        return position;
    }

    private GameObject GenerateEnemy()
    {
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                return enemy[0];
            case 1:
                return enemy[1];
        }
        return null;
    }

    private void CreateEnemy(GameObject _enemy)
    {
        //if(_enemy == enemy[0])
        //{
        //    for(int i = 0; i< UnityEngine.Random.Range(3, 6); i++)
        //    {
        //        GameObject newEnemy = Instantiate(_enemy);
        //        newEnemy.GetComponent<Enemy>().SetTarget(player);

        //        Vector3 spwanPosition = GenerateRandomPosition(newEnemy);

        //        spwanPosition += cam.transform.position;
        //        spwanPosition.z = 0;

        //        newEnemy.transform.position = spwanPosition;
        //        newEnemy.transform.SetParent(this.gameObject.transform);
        //    }
        //}
        //else if (_enemy == enemy[1])
        //{
        //    for (int i = 0; i < UnityEngine.Random.Range(1, 2); i++)
        //    {
        //        GameObject newEnemy = Instantiate(_enemy);
        //        newEnemy.GetComponent<Enemy>().SetTarget(player);

        //        Vector3 spwanPosition = GenerateRandomPosition(newEnemy);

        //        spwanPosition += cam.transform.position;
        //        spwanPosition.z = 0;

        //        newEnemy.transform.position = spwanPosition;
        //        newEnemy.transform.SetParent(this.gameObject.transform);
        //    }
        //}
        GameObject newEnemy = Instantiate(enemy[2]);
        newEnemy.GetComponent<Enemy>().SetTarget(player);

        Vector3 spwanPosition = GenerateRandomPosition(newEnemy);

        spwanPosition += cam.transform.position;
        spwanPosition.z = 0;

        newEnemy.transform.position = spwanPosition;
        newEnemy.transform.SetParent(this.gameObject.transform);
    }
}
