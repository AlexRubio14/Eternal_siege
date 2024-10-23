using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playersList = new List<GameObject>();
    [SerializeField] private List<PlayerInformation> playerInformation;
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private List<RoguelikeUpgrade> roguelikeUpgrades;

    [SerializeField] private List<GameObject> card;

    public static PlayersManager instance;

    [SerializeField] public List<Transform> posToSpawnList;

    [SerializeField] private CameraController cameraController;

    private PlayerInput inputSystem;

   

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
        player.GetComponent<PlayerInput>().SwitchCurrentControlScheme(PlayerLobbyManager.instance.GetInputsList()[index]);
        if (PlayerLobbyManager.instance.GetInputsList()[index] is Gamepad gamepad)
        {
            player.GetComponent<PlayerController>().SetPlayerGamePad(gamepad);
        }
        for (int i = 0; i< roguelikeUpgrades.Count; i++) 
        {
            if (roguelikeUpgrades[i].GetIndex() == index)
            {
                roguelikeUpgrades[i].SetPlayer(player);
            }
        }
        player.transform.position = posToSpawnList[index].position;
        SetCard(player);
        playersList.Add(player);
        Collider collider = player.GetComponent<Collider>();
        cameraController.AddPlayerIntoList(collider);
        playerInformation[playersList.Count - 1].gameObject.SetActive(true);
        playersList[playersList.Count - 1].GetComponent<PlayerController>().SetPlayerInformation(playerInformation[playersList.Count - 1]);
    }

    private void SetCard(GameObject _player)
    {
        if (PlayerLobbyManager.instance.GetTypeCharacter().Count == 1)
        {
            List<GameObject> _card = new List<GameObject>(3) { card[0], card[1], card[2] };
            _player.GetComponent<ChooseUpgrade>().SetCard(_card);
        }
        else
        { 
            if(playersList.Count == 0)
            {
                List<GameObject> _card = new List<GameObject>(3) { card[3], card[4], card[5] };
                _player.GetComponent<ChooseUpgrade>().SetCard(_card);
            }
            else
            {
                List<GameObject> _card = new List<GameObject>(3) { card[6], card[7], card[8] };
                _player.GetComponent<ChooseUpgrade>().SetCard(_card);
            }
        }
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
        PlayerLobbyManager.instance.ClearPlayers();
        SceneManager.LoadScene("LobbyScene");
        return true;
    }

    public void ChangeActionMap(string _nextActionMap)
    {
        foreach (GameObject player in playersList)
        {
            inputSystem = player.GetComponent<PlayerInput>();

            if (inputSystem)
                inputSystem.SwitchCurrentActionMap(_nextActionMap);
        }
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
