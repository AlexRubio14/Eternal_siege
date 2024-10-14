using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playersList = new List<GameObject>();
    [SerializeField] private List<PlayerInformation> playerInformation;
    [SerializeField] private List<GameObject> characters;

    public static PlayersManager instance;

    [SerializeField] public List<Transform> posToSpawnList;

    [SerializeField] CameraController cameraController;


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
        for(int i = 0; i < PlayerLobbyManager.instance.GetTypeCharacter().Count; i++)
        {
            AddPlayer(i);
        }
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

    private void AddPlayer(int index)
    {
        GameObject player = Instantiate(characters[PlayerLobbyManager.instance.GetTypeCharacter()[index]]);
        player.transform.position = posToSpawnList[index].position;
        playersList.Add(player);
        Collider collider = player.GetComponent<Collider>();
        cameraController.AddPlayerIntoList(collider);
        playerInformation[playersList.Count - 1].gameObject.SetActive(true);
        playersList[playersList.Count - 1].GetComponent<PlayerController>().SetPlayerInformation(playerInformation[playersList.Count - 1]);
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
