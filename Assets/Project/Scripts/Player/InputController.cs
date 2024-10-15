using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

 

public class InputController : MonoBehaviour
{
    public static InputController Instance;

    private PlayerInput inputSystem;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inputSystem = FindObjectOfType<PlayerInput>();
    }

    public void ChangeActionMap(string _nextActionMap)
    {
        if (inputSystem)
            inputSystem.SwitchCurrentActionMap(_nextActionMap);
    }
}
