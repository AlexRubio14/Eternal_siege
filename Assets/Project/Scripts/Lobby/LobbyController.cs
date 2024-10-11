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
    private GameObject playerUpgrade;
    private GameObject textInformation;
    private GameObject upgrades;

    private bool playActive;

    private int currentChamp;
    private int currentUpgradeHorizontal;
    private int currentUpgradeVertical;
    private int playerSelected;

    private void Start()
    {
        SetInitValues();
        SetInformationValues();
        ActiveValues();
    }

    private void SetInitValues()
    {
        playerSelected = 0;
        currentChamp = 0;
        currentUpgradeHorizontal = 0;
        currentUpgradeVertical = 0;

        information = GameObject.Find("Information").GetComponent<SetInformation>();
        information.AddPlayer(this);

        backGrounds = new List<GameObject>(information.GetBackGrounds().Count);
        backGroundSquares = new List<GameObject>(information.GetBackGroundSquares().Count);
        colors = new List<Color>(information.GetColors().Count);
    }
    private void SetInformationValues()
    {

        backGrounds = information.GetBackGrounds();
        backGroundSquares = information.GetBackGroundSquares();
        colors = information.GetColors();
        sprite = information.GetPlayerSprite();
        playerCharacterSelection = information.GetPlayerCharcterSelection();
        playerUpgrade = information.GetPlayerUpgrades();
        textInformation = information.GetTexts();
        upgrades = information.GetUpgrades();
    }
    private void ActiveValues()
    {
        textInformation.SetActive(true);
        sprite.SetActive(true);
        playerCharacterSelection.SetActive(true);
        playerUpgrade.SetActive(true);

        playActive = false;
    }
    private void SetPlayerSelected()
    {
        if (currentChamp == 1 && playerSelected == 0)
        {
            playerSelected = 1;
            sprite.transform.GetChild(0).gameObject.SetActive(false);
            sprite.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (currentChamp == 0 && playerSelected == 1)
        {
            playerSelected = 0;
            sprite.transform.GetChild(0).gameObject.SetActive(true);
            sprite.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void ActiveGame()
    {
        if (!playActive)
        {
            playActive = true;
            information.SetCurrentPlayersReady(1);
            information.SetText();
        }
        else
        {
            playActive = false;
            information.SetCurrentPlayersReady(-1);
            information.SetText();
        }
    }

    private void MoveBackGround(int index)
    {
        backGrounds[information.GetCurrentBackGround()].SetActive(false);
        backGroundSquares[information.GetCurrentBackGround()].GetComponent<Image>().color = colors[0];
        backGroundSquares[information.GetCurrentBackGround()].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        information.SetCurrentBackGround(index);
        backGrounds[information.GetCurrentBackGround()].SetActive(true);
        backGroundSquares[information.GetCurrentBackGround()].GetComponent<Image>().color = colors[1];
        backGroundSquares[information.GetCurrentBackGround()].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }
    public void MoveRight(InputAction.CallbackContext obj)
    {
        if (obj.started && information.GetCurrentBackGround() != backGrounds.Count - 1)
        {
            MoveBackGround(1);
        }
    }

    public void MoveLeft(InputAction.CallbackContext obj)
    {
        if (obj.started && information.GetCurrentBackGround() != 0)
        {
            MoveBackGround(-1);
        }
    }

    public void StartGame(InputAction.CallbackContext obj)
    {
        if(obj.started && information.GetCurrentBackGround() == 0)
        {
            ActiveGame();
        }
    }

    public void JoystickRight(InputAction.CallbackContext obj)
    {
        if(obj.started && currentChamp != 1 && information.GetCurrentBackGround() == 1)
        {
            playerCharacterSelection.transform.localPosition = new Vector3(playerCharacterSelection.transform.localPosition.x + 200, playerCharacterSelection.transform.localPosition.y,0);
            currentChamp++;
            textInformation.GetComponent<SetCharacterText>().SetText(currentChamp);
        }

        if(obj.started && currentUpgradeHorizontal != 2 &&  information.GetCurrentBackGround() == 2)
        {
            playerUpgrade.transform.localPosition = new Vector3(playerUpgrade.transform.localPosition.x + 550, playerUpgrade.transform.localPosition.y, 0);
            currentUpgradeHorizontal++;
        }
    }

    public void JoystickLeft(InputAction.CallbackContext obj)
    {
        if (obj.started && currentChamp != 0 && information.GetCurrentBackGround() == 1)
        {
            playerCharacterSelection.transform.localPosition = new Vector3(playerCharacterSelection.transform.localPosition.x - 200, playerCharacterSelection.transform.localPosition.y,0);
            currentChamp--;
            textInformation.GetComponent<SetCharacterText>().SetText(currentChamp);
        }

        if (obj.started && currentUpgradeHorizontal != 0 && information.GetCurrentBackGround() == 2)
        {
            playerUpgrade.transform.localPosition = new Vector3(playerUpgrade.transform.localPosition.x - 550, playerUpgrade.transform.localPosition.y, 0);
            currentUpgradeHorizontal--;
        }
    }

    public void JoystickUp(InputAction.CallbackContext obj)
    {
        if (obj.started && currentUpgradeVertical != 0 && information.GetCurrentBackGround() == 2)
        {
            playerUpgrade.transform.localPosition = new Vector3(playerUpgrade.transform.localPosition.x, playerUpgrade.transform.localPosition.y + 270, 0);
            currentUpgradeVertical--;
        }
    }

    public void JoystickDown(InputAction.CallbackContext obj)
    {
        if (obj.started && currentUpgradeVertical != 2 && information.GetCurrentBackGround() == 2)
        {
            playerUpgrade.transform.localPosition = new Vector3(playerUpgrade.transform.localPosition.x, playerUpgrade.transform.localPosition.y - 270, 0);
            currentUpgradeVertical++;
        }
    }

    public void SelectPlayer(InputAction.CallbackContext obj)
    {
        if(obj.started)
        {
            if(information.GetCurrentBackGround() == 1)
            {
                SetPlayerSelected();
            }
            else if(information.GetCurrentBackGround() == 2)
            {
                upgrades.GetComponent<ActiveUpgrades>().SearchAbility(currentUpgradeVertical, currentUpgradeHorizontal);
            }
        }

    }
    
    public int GetPlayerSelected()
    {
        return playerSelected;
    }

}
