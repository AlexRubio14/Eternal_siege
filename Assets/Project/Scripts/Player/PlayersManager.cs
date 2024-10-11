using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playersList = new List<GameObject>();

    public static PlayersManager instance;

    [SerializeField] public List<Transform> posToSpawnList;


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
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerController player = playersList[0].GetComponent<PlayerController>();
            player.Die();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerController player = playersList[1].GetComponent<PlayerController>();
            player.Die();
        }
    }

    public void AddPlayer(GameObject player)
    {
        playersList.Add(player);
    }

    public void ErasePlayer(GameObject player)
    {
        playersList.Remove(player);
    }

    public bool CheckIfAllPLayersDead()
    {
        foreach(GameObject player in playersList)
        {
            PlayerController controller = player.GetComponent<PlayerController>();

            if (controller.GetCurrentState() != PlayerController.State.DEAD)
                return false;
        }

        SceneManager.LoadScene("LobbyScene");
        return true;
    }

    public List<GameObject> GetPlayersList()
    {
        return playersList;
    }

    public int GetPlayerIndex()
    {
        return playersList.Count;
    }
}
