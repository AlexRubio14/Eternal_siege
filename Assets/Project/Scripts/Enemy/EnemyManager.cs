using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private List<GameObject> typesOfEnemies;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float secondsMiniBoss;

    Camera cam;
    float timer;
    Vector2 cameraBorder;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        timer = 0;
        cameraBorder = new Vector2(cam.aspect * cam.orthographicSize, cam.orthographicSize);

        Invoke("SpawnMiniBoss", secondsMiniBoss);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0 && PlayersManager.instance.GetPlayersList().Count > 0)
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    private void SpawnEnemy()
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
                return typesOfEnemies[0];
            case 1:
                return typesOfEnemies[1];
        }
        return null;
    }

    private void CreateEnemy(GameObject _enemy)
    {
        GameObject newEnemy = null;

        if (_enemy == typesOfEnemies[0])
        {
            for (int i = 0; i < UnityEngine.Random.Range(3, 6); i++)
            {
                newEnemy = Instantiate(_enemy);
                newEnemy.GetComponent<Enemy>().SetTarget(PlayersManager.instance.GetPlayersList());

                Vector3 spawnPosition = GenerateRandomPosition(newEnemy);

                spawnPosition += cam.transform.position;
                spawnPosition.z = 0;

                newEnemy.transform.position = spawnPosition;
                newEnemy.transform.SetParent(this.gameObject.transform);

                enemies.Add(newEnemy);

            }
        }
        else if (_enemy == typesOfEnemies[1])
        {
            for (int i = 0; i < UnityEngine.Random.Range(1, 2); i++)
            {
                newEnemy = Instantiate(_enemy);
                newEnemy.GetComponent<Enemy>().SetTarget(PlayersManager.instance.GetPlayersList());

                Vector3 spawnPosition = GenerateRandomPosition(newEnemy);

                spawnPosition += cam.transform.position;
                spawnPosition.z = 0;

                newEnemy.transform.position = spawnPosition;
                newEnemy.transform.SetParent(this.gameObject.transform);

                enemies.Add(newEnemy);

            }
        }

    }

    private void SpawnMiniBoss()
    {
        GameObject newEnemy = null;

        newEnemy = Instantiate(typesOfEnemies[2]);
        newEnemy.GetComponent<Enemy>().SetTarget(PlayersManager.instance.GetPlayersList());

        Vector3 spwanPosition = GenerateRandomPosition(newEnemy);

        spwanPosition += cam.transform.position;
        spwanPosition.z = 0;

        newEnemy.transform.position = spwanPosition;
        newEnemy.transform.SetParent(this.gameObject.transform);

        enemies.Add(newEnemy);

        Invoke("SpawnMiniBoss", 60);
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public Vector2 GetNearestEnemyDirection(Vector2 position)
    {
        Vector2 direction = ((Vector2)enemies[0].transform.localPosition - position);

        for (int i = 1; i < enemies.Count; i++)
        {
            Vector2 distancePosition1 = (Vector2)enemies[i].transform.localPosition - position;

            if(distancePosition1.magnitude < direction.magnitude)
            {
                direction = distancePosition1;
            }
        }

        return direction.normalized;

    }
}
