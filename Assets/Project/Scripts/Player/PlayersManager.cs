using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> spawnedPlayers = new List<GameObject>();

    public static PlayersManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddPlayer(GameObject player)
    {
        spawnedPlayers.Add(player);
    }
}
