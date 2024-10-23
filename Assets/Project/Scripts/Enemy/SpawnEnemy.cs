using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float spawnDistance;
    [SerializeField] private CameraController midPoint;

    [Header("Camera")]
    private bool[] cornerCheck;
    Camera cam;

    [SerializeField] private Transform midPointTransform;

    private void Start()
    {
        cam = Camera.main;

        cornerCheck = new bool[4];
        for (int i = 0; i < cornerCheck.Length; i++)
        {
            cornerCheck[i] = false;
        }
    }

    public void CreateEnemy(int index)
    {
        GameObject newEnemy = Instantiate(EnemyManager.instance.GenerateEnemy(index));

        newEnemy.GetComponent<Enemy>().SetTarget(PlayersManager.instance.GetPlayersList());

        Vector3 spawnPosition = GenerateRandomPosition();

        spawnPosition.y = -2.5f;

        newEnemy.transform.localPosition = spawnPosition;
        newEnemy.transform.SetParent(this.gameObject.transform);

        EnemyManager.instance.GetEnemies().Add(newEnemy);
    }

    private Vector3 GenerateRandomPosition()
    {
        float radians = SetAngle() * Mathf.Deg2Rad;

        float xOffset = Mathf.Cos(radians) * spawnDistance;
        float zOffset = Mathf.Sin(radians) * spawnDistance;

        Vector3 spawnPosition = new Vector3(midPointTransform.position.x + xOffset, midPointTransform.position.y, midPointTransform.position.z + zOffset);

        if (spawnPosition.z < -26 || spawnPosition.z > 25 || spawnPosition.x < -50 || spawnPosition.x > 50)
            spawnPosition = Vector3.zero;
        return spawnPosition;
    }

    private float SetAngle()
    {
        // 0 = left, 1 = right, 2 = down, 3 = top
        if (cornerCheck[0])
        {
            if(cornerCheck[2])
            {
                return Random.Range(90f, 0f);
            }
            else if(cornerCheck[3])
            {
                return Random.Range(0f, -90f);
            }
            return Random.Range(90f, -90f);
        }
        else if (cornerCheck[1])
        {
            if (cornerCheck[2])
            {
                return Random.Range(90f, 180f);
            }
            else if (cornerCheck[3])
            {
                return Random.Range(180f, 270f);
            }
            return Random.Range(90f, 270f);
        }
        else if (cornerCheck[2])
        {
            return Random.Range(0f, 180f);
        }
        else if (cornerCheck[3])
        {
            return Random.Range(0f, -180f);
        }

        return Random.Range(0f, 360f);
    }

    public void SetCornerCheck(bool state, int _index)
    {
        cornerCheck[_index] = state;
    }
}
