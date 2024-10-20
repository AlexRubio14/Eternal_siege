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

    [HideInInspector]
    public GameObject gameCanvas;

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
        singlePlayerUpgrades.gameObject.SetActive(true);
    }

    public void Active2PlayersUpgradeCanvas()
    {
        player1Upgrades.gameObject.SetActive(true);
        player2Upgrades.gameObject.SetActive(true);
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
