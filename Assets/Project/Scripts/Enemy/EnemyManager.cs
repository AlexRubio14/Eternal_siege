using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    public GameObject GenerateEnemy(int index)
    {
        return typesOfEnemies[index];
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public void GetNearestEnemyDirection(Vector2 position, out Vector2 _direction, out Vector2 _enemy)
    {
        Vector2 direction = ((Vector2)enemies[0].transform.localPosition - position);
        Vector2 enemyPosition = enemies[0].transform.localPosition;

        for (int i = 1; i < enemies.Count; i++)
        {
            Vector2 distancePosition1 = (Vector2)enemies[i].transform.localPosition - position;

            if(distancePosition1.magnitude < direction.magnitude)
            {
                direction = distancePosition1;
                enemyPosition = enemies[i].transform.localPosition;
            }
        }
        _direction = direction.normalized;
        _enemy = enemyPosition;
    }
}
