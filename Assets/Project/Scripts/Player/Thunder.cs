using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEngine.CompositeCollider2D;

public class Thunder : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float durationTime;
    [SerializeField] private float durationGeneration;

    float maxScale;
    private float currentTime;
    private float generationTime;
    private bool isGenerating;

    private void Start()
    {
        currentTime = 0;
        generationTime = 0;
        isGenerating = true;
    }

    private void Update()
    {
        if(isGenerating)
        {
            generationTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxScale, 0.1f, maxScale), generationTime/durationGeneration);
            if(generationTime > durationGeneration) 
            {
                isGenerating = false;
            }
        }
        currentTime += Time.deltaTime;
        if (currentTime > durationTime)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision is BoxCollider)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(DamageEnemy(enemy, 1f));
        }
    }

    private IEnumerator DamageEnemy(Enemy enemy, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (enemy != null)
        {
            enemy.ReceiveDamage(damage);
        }
    }

    public void SetScale(float scale)
    {
        maxScale = scale;
    }
}
