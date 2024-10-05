using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    [SerializeField] private SpawnEnemy spawnEnemy;
    public int index;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            spawnEnemy.SetCornerCheck(true, index);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
        {
            spawnEnemy.SetCornerCheck(false, index);
        }
    }

}
