using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Event Times")]
    [SerializeField] private List<float> eventTime;

    [Header("Enemies")]
    [SerializeField] private SpawnEnemy enemy;
    [SerializeField] private List<float> enemyTime;
    [SerializeField] private List<int> quantity;
    [SerializeField] private int initQuantity;

    [SerializeField] private int bossSpawnTime;


    private float[] totalOfEnemies;
    private float[] spawnTime;
    private float[] spawnEnemyTime;
    private int currentTime;
    private float time;


    private bool enemySpawned;

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

        totalOfEnemies = new float[quantity.Count];
        spawnTime = new float[quantity.Count];
        spawnEnemyTime = new float[quantity.Count];

        totalOfEnemies[0] = initQuantity;
        spawnTime[0] = 60 / totalOfEnemies[0];
        spawnEnemyTime[0] = 0;

        enemySpawned = false;
    }

    private void Update()
    {
        if (PlayersManager.instance.GetPlayersList().Count > 0)
        {
            time += Time.deltaTime * TimeManager.instance.GetPaused();

            for(int i = 0; i<spawnTime.Length; i++) 
            {
                spawnEnemyTime[i] -= Time.deltaTime * TimeManager.instance.GetPaused();
                ManageEnemy(i);
            }
            
            for(int i = 0; i<eventTime.Count; i++)
            {
                ActiveEvent(i);
            }
            MinuteChange();

            if(time > bossSpawnTime * 60 && !enemySpawned)
            {
                SpawnBoss();
                enemySpawned = true;
            }
        }
    }

    private void ManageEnemy(int index)
    {
        if (totalOfEnemies[index] > 0 && spawnEnemyTime[index] < 0)
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
                SetTimes(0, quantity[0]);
            }
            else if (currentTime < enemyTime[1])
            {
                SetTimes(1, quantity[1]);
            }
            else if(currentTime < enemyTime[2])
            {
                SetTimes(2, quantity[2]);
            }
        }
    }

    private void SetTimes(int index, int _quantity)
    {
        totalOfEnemies[index] += _quantity;
        spawnTime[index] = 60 / totalOfEnemies[index];
        spawnEnemyTime[index] = 0;
    }
    public void SpawnMiniBoss()
    {
        enemy.CreateEnemy(3);
    }

    public void SpawnBoss()
    {
        enemy.CreateEnemy(4);
    }

    public int GetTime()
    {
        return (int)time;
    }
}
