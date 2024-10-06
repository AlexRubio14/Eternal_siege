using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private SpawnEnemy enemy;

    private int[] quantity;
    private int[] spawnTime;
    private float[] spawnEnemyTime;
    private int currentTime;
    private float time;

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
        time = 0;
        currentTime = 1;

        quantity = new int[3];
        spawnTime = new int[3];
        spawnEnemyTime = new float[3];

        quantity[0] = 20;
        spawnTime[0] = 60 / quantity[0];
        spawnEnemyTime[0] = 0;

        enemy.CreateEnemy(3);
    }

    private void Update()
    {
        if (PlayersManager.instance.GetPlayersList().Count > 0)
        {
            time += Time.deltaTime;

            for(int i = 0; i<spawnTime.Length; i++) 
            {
                spawnEnemyTime[i] -= Time.deltaTime;
                ManageEnemy(i);
            }
            MinuteChange();
        }
    }

    private void ManageEnemy(int index)
    {
        if (quantity[index] > 0 && spawnEnemyTime[index] <= 0)
        {
            enemy.CreateEnemy(index);
            spawnEnemyTime[index] = spawnTime[index];
        }
    }

    private void MinuteChange()
    {
        if (time / 60 > currentTime)
        {
            currentTime++;
            if(currentTime < 5)
            {
                SetTimes(0, 10);
            }
            else if (currentTime < 11)
            {
                SetTimes(1, 2);
            }
            else if(currentTime < 13)
            {
                SetTimes(1, 1);
            }
        }
    }

    private void SetTimes(int index, int _quantity)
    {
        quantity[index] += _quantity;
        spawnTime[index] = 60 / quantity[index];
        spawnEnemyTime[index] = 0;
    }

    public int GetTime()
    {
        return (int)time;
    }
}
