using System.Collections.Generic;
using UnityEngine;

public class RoguelikeManager : MonoBehaviour
{
    public static RoguelikeManager instance;

    [SerializeField] private GameObject onePlayerCanvas;
    [SerializeField] private GameObject twoPlayersCanvas;

    [HideInInspector]
    public GameObject gameCanvas;

    [SerializeField] private List<bool> playersHaveSelectedUpgradeList;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;

        DontDestroyOnLoad(instance);

        int numOfPlayers = Input.GetJoystickNames().Length;

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

        RoguelikeCanvas.Instance.ReturnToGameplay();
        PlayersManager.instance.ChangeActionMap("Player");

        for(int i = 0; i <playersHaveSelectedUpgradeList.Count; i++)
        {
            playersHaveSelectedUpgradeList[i] = false;
        }
    }

    public List<bool> GetPlayersHaveSelectedUpgradeList()
    {
        return playersHaveSelectedUpgradeList;
    }

    public void SetPlayersHaveSelectedUpgradeList(int index,bool value)
    {
        playersHaveSelectedUpgradeList[index] = value;
    }
}
