using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateThunder : MonoBehaviour
{
    [SerializeField] private GameObject magicCape;

    [SerializeField] private float distanceToSpawn;
    [SerializeField] private float generateCd;
    [SerializeField] private float currentScale;

    private float currentCdTime;

    private void Start()
    {
        currentCdTime = 0;
    }

    private void Update()
    {
        currentCdTime += Time.deltaTime;

        if(currentCdTime > generateCd) 
        {
            InstanciateGenerateCape();
            currentCdTime = 0;
        }
    }

    private void InstanciateGenerateCape()
    {
        GameObject _magicCape = Instantiate(magicCape);
        _magicCape.GetComponent<Thunder>().SetScale(currentScale);
        _magicCape.transform.parent = null;
        _magicCape.transform.position = GenerateRandomPosition();
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = Vector3.zero;
        Vector2 direction;
        Vector2 enemyPosition;

        EnemyManager.instance.GetNearestEnemyDirection(new Vector2(transform.position.x, transform.position.z), out direction, out enemyPosition);

        Vector3 direction3D = new Vector3(direction.x, 0, direction.y); 

        position = transform.position + direction3D * distanceToSpawn;

        if(position.magnitude > enemyPosition.magnitude) 
        {
            position.x = enemyPosition.x;
            position.z = enemyPosition.y;
        }

        // Fijar la altura de la posición
        position.y = -2.5f;

        return position;
    }

    public void AddScale(float scale)
    {
        currentScale += scale;
    }
}
