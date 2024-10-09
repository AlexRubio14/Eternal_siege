using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class LobbyController : MonoBehaviour
{
    private List<Color> colors;
    private List<GameObject> backGrounds;
    private List<GameObject> backGroundSquares;

    private SetInformation information;
    private GameObject sprite;
    private GameObject playerCharacterSelection;
    private GameObject textInformation;

    private bool playActive;

    private int currentBackGround;
    private int currentChamp;
    private int playerSelected;

    private void Start()
    {
        currentBackGround = 0;
        playerSelected = 0;
        currentChamp = 0;
        information = GameObject.Find("Information").GetComponent<SetInformation>();
        information.AddPlayer(gameObject);

        backGrounds = new List<GameObject>(information.GetBackGrounds().Count);
        backGroundSquares = new List<GameObject>(information.GetBackGroundSquares().Count);
        colors = new List<Color>(information.GetColors().Count);

        backGrounds = information.GetBackGrounds();
        backGroundSquares = information.GetBackGroundSquares();
        colors = information.GetColors();
        sprite = information.GetPlayerSprite();
        playerCharacterSelection = information.GetPlayerCharcterSelection();
        textInformation = information.GetTexts()
            ;
        textInformation.SetActive(true);
        sprite.SetActive(true);
        playerCharacterSelection.SetActive(true);

        playActive = false;
    }
    public void MoveRight(InputAction.CallbackContext obj)
    {
        if (currentBackGround != backGrounds.Count - 1)
        {
            backGrounds[currentBackGround].SetActive(false);
            backGroundSquares[currentBackGround].GetComponent<Image>().color = colors[0];
            backGroundSquares[currentBackGround].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            currentBackGround++;
            backGrounds[currentBackGround].SetActive(true);
            backGroundSquares[currentBackGround].GetComponent<Image>().color = colors[1];
            backGroundSquares[currentBackGround].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }

    public void MoveLeft(InputAction.CallbackContext obj)
    {
        if (currentBackGround != 0)
        {
            backGrounds[currentBackGround].SetActive(false);
            backGroundSquares[currentBackGround].GetComponent<Image>().color = colors[0];
            backGroundSquares[currentBackGround].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            currentBackGround--;
            backGrounds[currentBackGround].SetActive(true);
            backGroundSquares[currentBackGround].GetComponent<Image>().color = colors[1];
            backGroundSquares[currentBackGround].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }

    public void StartGame(InputAction.CallbackContext obj)
    {
        if(currentBackGround == 0)
        {
            if(!playActive)
            {
                playActive = true;
                information.SetCurrentPlayersReady(1);
            }
            else
            {
                playActive = false;
                information.SetCurrentPlayersReady(-1);
            }
        }
    }

    public void JoystickRight(InputAction.CallbackContext obj)
    {
        if(currentChamp != 1 && currentBackGround == 1)
        {
            playerCharacterSelection.transform.localPosition = new Vector3(playerCharacterSelection.transform.localPosition.x + 200, playerCharacterSelection.transform.localPosition.y,0);
            currentChamp++;
            textInformation.GetComponent<SetCharacterText>().SetText(currentChamp);
        }
    }

    public void JoystickLeft(InputAction.CallbackContext obj)
    {
        if (currentChamp != 0 && currentBackGround == 1)
        {
            playerCharacterSelection.transform.localPosition = new Vector3(playerCharacterSelection.transform.localPosition.x - 200, playerCharacterSelection.transform.localPosition.y,0);
            currentChamp--;
            textInformation.GetComponent<SetCharacterText>().SetText(currentChamp);
        }
    }

    public void SelectPlayer(InputAction.CallbackContext obj)
    {
        if(currentChamp == 1 && playerSelected == 0)
        {
            playerSelected = 1;
            sprite.transform.GetChild(0).gameObject.SetActive(false);
            sprite.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if(currentChamp == 0 && playerSelected == 1)
        {
            playerSelected = 0;
            sprite.transform.GetChild(0).gameObject.SetActive(true);
            sprite.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

}
