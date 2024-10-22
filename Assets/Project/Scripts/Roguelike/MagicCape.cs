
using System.Collections.Generic;
using UnityEngine;

public class MagicCape : MonoBehaviour
{
    [SerializeField] private float initScale;
    [SerializeField] private float augmentScale;
    [SerializeField] private float damage;
    [SerializeField] private float timeToActivate;

    private List<Enemy> enemies;
    private float timer;

    private int level;

    private void Awake()
    {
        level = 1;
        timer = 0;
        transform.localScale = new Vector3(initScale, transform.localScale.y, initScale);
        enemies = new List<Enemy>();
    }

    private void Update()
    {
        timer += Time.deltaTime * TimeManager.instance.GetPaused();

        if(timer >= timeToActivate)
        {
            timer = 0;
            for (int i = 0; i < enemies.Count - 1; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
                else
                    enemies[i].ReceiveDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other is BoxCollider)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (!enemies.Contains(enemy))
                enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other is BoxCollider)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemies.Remove(enemy);
        }
    }

    public void LevelUp()
    {
        level += 1;

        if (level < 5)
        {
            transform.localScale = new Vector3(
                transform.localScale.x + augmentScale, 
                transform.localScale.y, 
                transform.localScale.z + augmentScale
                );
        }
        else
        {
            transform.localScale = new Vector3(
                transform.localScale.x + augmentScale * 2,
                transform.localScale.y,
                transform.localScale.z + augmentScale * 2
                );
        }
    }
    
}
