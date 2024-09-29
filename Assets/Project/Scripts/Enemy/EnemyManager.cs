using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnTimer;
    [SerializeField] private GameObject[] player;

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
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.GetComponent<Enemy>().SetTarget(player);

        Vector3 spwanPosition = GenerateRandomPosition(newEnemy);

        spwanPosition += cam.transform.position;
        spwanPosition.z = 0;

        newEnemy.transform.position = spwanPosition;
        newEnemy.transform.SetParent(this.gameObject.transform);

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
}
