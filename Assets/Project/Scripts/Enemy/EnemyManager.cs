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
        Vector2 direction = new Vector2(enemies[0].transform.localPosition.x - position.x, enemies[0].transform.localPosition.z - position.y);
        Vector2 enemyPosition = new Vector2(enemies[0].transform.localPosition.x, enemies[0].transform.localPosition.z);

        for (int i = 1; i < enemies.Count; i++)
        {
            Vector2 distancePosition1 = new Vector2(enemies[i].transform.localPosition.x - position.x, enemies[i].transform.localPosition.z - position.y);

            if(distancePosition1.magnitude < direction.magnitude)
            {
                direction = distancePosition1;
                enemyPosition = new Vector2(enemies[i].transform.localPosition.x, enemies[i].transform.localPosition.z);
            }
        }
        _direction = direction.normalized;
        _enemy = enemyPosition;
    }
}
