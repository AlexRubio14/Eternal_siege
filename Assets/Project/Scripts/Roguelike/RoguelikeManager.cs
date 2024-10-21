using System.Collections.Generic;
using UnityEngine;

public class RoguelikeManager : MonoBehaviour
{
    public static RoguelikeManager instance;

    [SerializeField] private GameObject onePlayerCanvas;
    [SerializeField] private GameObject twoPlayersCanvas;
    [SerializeField] private GameObject singlePlayerUpgrades;
    [SerializeField] private GameObject player1Upgrades;
    [SerializeField] private GameObject player2Upgrades;

    [HideInInspector] public GameObject gameCanvas;

    [SerializeField] private List<bool> playersHaveSelectedUpgradeList;

    public int numOfPlayers { get; private set; }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;

        DontDestroyOnLoad(instance);



        numOfPlayers = PlayerLobbyManager.instance.GetInputsList().Count;

        switch (numOfPlayers) 
        {
            case 1: 
                gameCanvas = onePlayerCanvas;

                break;
            case 2: 
                gameCanvas = twoPlayersCanvas; 
                break;
            default: 
                break;
        }

        for(int i = 0; i < numOfPlayers; i++)
        {
            playersHaveSelectedUpgradeList.Add(false);
        }
    }

    public void CheckIfAllPlayersHaveSelectedUpgrade()
    {
        foreach(bool hasSelectedUpgrade in playersHaveSelectedUpgradeList)
        {
            if(!hasSelectedUpgrade)
            {
                return;
            }
        }

        RoguelikeCanvas.instance.ReturnToGameplay();
        PlayersManager.instance.ChangeActionMap("Player");

        for(int i = 0; i <playersHaveSelectedUpgradeList.Count; i++)
        {
            playersHaveSelectedUpgradeList[i] = false;
        }
    }

    public void Active1PlayerUpgradeCanvas()
    {
        onePlayerCanvas.gameObject.SetActive(true);
        singlePlayerUpgrades.gameObject.SetActive(true);

        SetRandomValue(singlePlayerUpgrades);
    }

    public void Active2PlayersUpgradeCanvas()
    {
        twoPlayersCanvas.gameObject.SetActive(true);
        player1Upgrades.gameObject.SetActive(true);
        player2Upgrades.gameObject.SetActive(true);

        SetRandomValue(player1Upgrades);
        SetRandomValue(player2Upgrades);
    }

    private void SetRandomValue(GameObject _playerUpgrades)
    {
        int randomValue = UnityEngine.Random.Range(0, 6);
        _playerUpgrades.transform.GetChild(0).GetComponent<RoguelikeUpgrade>().SetCurrentUpgrade(randomValue);
        _playerUpgrades.transform.GetChild(0).GetComponent<RoguelikeUpgrade>().SetText();

        while (randomValue == _playerUpgrades.transform.GetChild(0).GetComponent<RoguelikeUpgrade>().GetCurrentUpgrade())
        {
            randomValue = UnityEngine.Random.Range(0, 6);
            _playerUpgrades.transform.GetChild(1).GetComponent<RoguelikeUpgrade>().SetCurrentUpgrade(randomValue);
            _playerUpgrades.transform.GetChild(1).GetComponent<RoguelikeUpgrade>().SetText();
        }

        while (randomValue == _playerUpgrades.transform.GetChild(0).GetComponent<RoguelikeUpgrade>().GetCurrentUpgrade()
            || randomValue == _playerUpgrades.transform.GetChild(1).GetComponent<RoguelikeUpgrade>().GetCurrentUpgrade())
        {
            randomValue = UnityEngine.Random.Range(0, 6);
            _playerUpgrades.transform.GetChild(2).GetComponent<RoguelikeUpgrade>().SetCurrentUpgrade(randomValue);
            _playerUpgrades.transform.GetChild(2).GetComponent<RoguelikeUpgrade>().SetText();
        }
    }

    public void DeactiveUpgradeCanvas()
    {
        player1Upgrades.gameObject.SetActive(false);
        player2Upgrades.gameObject.SetActive(false);
    }

    public List<bool> GetPlayersHaveSelectedUpgradeList()
    {
        return playersHaveSelectedUpgradeList;
    }

    public void SetPlayersHaveSelectedUpgradeList(int index,bool value)
    {
        playersHaveSelectedUpgradeList[index] = value;
    }

    public void SetNumOfPlayers(int _numOfPlayers)
    {
        numOfPlayers = _numOfPlayers;
    }
}
