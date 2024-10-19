using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLobbyManager : MonoBehaviour
{
    public static PlayerLobbyManager instance;

    private List<int> playersList = new List<int>();

    private List<InputDevice> inputs = new List<InputDevice>();

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

    public void SetController(int typePlayer, InputDevice input)
    {
        playersList.Add(typePlayer);
        inputs.Add(input);
    }

    public void ClearPlayers()
    {
        playersList.Clear();
    }

    public List<int> GetTypeCharacter()
    {
        return playersList;
    }

    public List<InputDevice> GetInputsList()
    {
        return inputs;
    }


}
