using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float durationTime;
    [SerializeField] private float durationGeneration;
    [SerializeField] private float timeToDamage;

    private List<Enemy> enemies;
    float maxScale;
    private float timer;
    private float currentTime;
    private float generationTime;
    private bool isGenerating;

    private void Awake()
    {
        enemies = new List<Enemy>();
        timer = 0;
        currentTime = 0;
        generationTime = 0;
        isGenerating = true;
    }

    private void Update()
    {
        timer += Time.deltaTime * TimeManager.instance.GetPaused();
        currentTime += Time.deltaTime * TimeManager.instance.GetPaused();

        if(isGenerating)
        {
            generationTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxScale, 0.1f, maxScale), generationTime/durationGeneration);
            if(generationTime > durationGeneration) 
            {
                isGenerating = false;
            }
        }

        if (timer >= timeToDamage)
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

        if (currentTime > durationTime)
            Destroy(gameObject);
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

    public void SetScale(float scale)
    {
        maxScale = scale;
    }
}
