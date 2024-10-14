using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    [SerializeField] private SpawnEnemy spawnEnemy;
    public int index;

    private void Update()
    {
        if (IsInFrustum(Camera.main, transform.position))
        {
            spawnEnemy.SetCornerCheck(true, index);
        }
        else
        {
            spawnEnemy.SetCornerCheck(false, index);
        }
    }

    private bool IsInFrustum(Camera cam, Vector3 position)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds);
    }

}
