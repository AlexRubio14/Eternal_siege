using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyManager : MonoBehaviour
{
    public static PlayerLobbyManager instance;

    private List<int> playersList = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetController(int typePlayer)
    {
        playersList.Add(typePlayer);
    }

    public List<int> GetTypeCharacter()
    {
        return playersList;
    }
}
