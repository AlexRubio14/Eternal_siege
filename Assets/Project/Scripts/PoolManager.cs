using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject playerBullet;

    private List<GameObject> enemyBulletPool;
    private List<GameObject> playerBulletPool;

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
        enemyBulletPool = new List<GameObject>();
        playerBulletPool = new List<GameObject>();
    }

    public GameObject InstantiateEnemyBullet()
    {
        foreach (GameObject bullet in enemyBulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);  
                return bullet;
            }          
        }

        GameObject _bullet = Instantiate(enemyBullet);
        enemyBulletPool.Add(_bullet);

        return _bullet; 
    }

    public void InstantiatPlayerBullet(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject bullet in playerBulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;

                return;
            }
        }

        GameObject _bullet = Instantiate(playerBullet, position, rotation);
        playerBulletPool.Add(_bullet);

        return;
    }


}
