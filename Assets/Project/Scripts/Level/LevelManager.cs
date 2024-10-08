using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Event Times")]
    [SerializeField] private List<float> eventTime;

    [Header("Enemies")]
    [SerializeField] private SpawnEnemy enemy;
    [SerializeField] private List<float> enemyTime;
    [SerializeField] private List<int> cuantity;
    [SerializeField] private int initCuantity;

    private float[] quantity;
    private float[] spawnTime;
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
        currentTime = 0;

        quantity = new float[cuantity.Count];
        spawnTime = new float[cuantity.Count];
        spawnEnemyTime = new float[cuantity.Count];

        quantity[0] = initCuantity;
        spawnTime[0] = 60 / quantity[0];
        spawnEnemyTime[0] = 0;
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
            
            for(int i = 0; i<eventTime.Count; i++)
            {
                ActiveEvent(i);
            }
            MinuteChange();
        }
    }

    private void ManageEnemy(int index)
    {
        if (quantity[index] > 0 && spawnEnemyTime[index] < 0)
        {
            enemy.CreateEnemy(index);
            spawnEnemyTime[index] = spawnTime[index];
        }
    }

    private void ActiveEvent(int index)
    {
        if (currentTime >= eventTime[index] && !GenerateEvent.instance.eventActive[index])
        {
            GenerateEvent.instance.SpawnEvent(index);
            GenerateEvent.instance.eventActive[index] = true;
        }

    }

    private void MinuteChange()
    {
        if ((int)(time / 60) > currentTime)
        {
            currentTime++;
            if(currentTime < enemyTime[0])
            {
                SetTimes(0, cuantity[0]);
            }
            else if (currentTime < enemyTime[1])
            {
                SetTimes(1, cuantity[1]);
            }
            else if(currentTime < enemyTime[2])
            {
                SetTimes(2, cuantity[2]);
            }
        }
    }

    private void SetTimes(int index, int _quantity)
    {
        quantity[index] += _quantity;
        spawnTime[index] = 60 / quantity[index];
        spawnEnemyTime[index] = 0;
    }
    public void SpawnMiniBoss()
    {
        enemy.CreateEnemy(3);
    }

    public int GetTime()
    {
        return (int)time;
    }
}
