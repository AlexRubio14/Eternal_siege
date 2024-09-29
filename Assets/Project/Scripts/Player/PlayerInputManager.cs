using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputManager : MonoBehaviour
{

    private PlayerInput playerinput;

    private PlayerController playerController;


    private void Awake()
    {
        playerinput = GetComponent<PlayerInput>();
        int playerIndex = playerinput.playerIndex;
        var playerControllers = FindObjectsOfType<PlayerController>();
        playerController = playerControllers.FirstOrDefault(playerController => playerController.GetPlayerIndex() == playerIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
