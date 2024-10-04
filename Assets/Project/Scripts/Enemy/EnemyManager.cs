using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Enemies")]
    [SerializeField] private List<GameObject> typesOfEnemies;
    [SerializeField] private List<GameObject> enemies;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public GameObject GenerateEnemy()
    {
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                for(int i = 0; i< UnityEngine.Random.Range(2, 4); i++)
                {
                    return typesOfEnemies[0];
                }
                break;
            case 1:
                for (int i = 0; i < UnityEngine.Random.Range(1, 2); i++)
                {
                    return typesOfEnemies[1];
                }
                break;
            case 2:
                for (int i = 0; i < UnityEngine.Random.Range(1, 2); i++)
                {
                    return typesOfEnemies[2];
                }
                break;
        }

        return null;
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
