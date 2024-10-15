using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SetInformation : MonoBehaviour
{
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<GameObject> backGrounds;
    [SerializeField] private List<GameObject> backGroundSquares;
    [SerializeField] private List<GameObject> playerSprites;
    [SerializeField] private List<GameObject> playerCharcterSelection;
    [SerializeField] private List<GameObject> playerUpgrades;

    [SerializeField] private TextMeshProUGUI playGame;
    [SerializeField] private float maxTime;
    [SerializeField] private GameObject secondPlayerCharacter;
    [SerializeField] private GameObject upgrades;
    [SerializeField] private List<GameObject> texts;

    private List<LobbyController> players;
    private int currentPlayersReady;
    private int currentBackGround;
    private float time;
    private bool startTime;

    private void Start()
    {
        players = new List<LobbyController>(2);
        currentPlayersReady = 0;
        currentBackGround = 0;
        time = maxTime;
        secondPlayerCharacter.SetActive(false);
    }

    private void Update()
    {
        if(startTime)
        {
            time -= Time.deltaTime;
            playGame.text = "The game starts in " + Mathf.FloorToInt(time).ToString();
            if(time <= 0)
            {
                for(int i = 0; i < players.Count; i++) 
                {
                    PlayerLobbyManager.instance.SetController(players[i].GetPlayerSelected(), players[i].GetComponent<PlayerInput>().devices[0]);
                }
                SceneManager.LoadScene("GamePlay3DScene");
            }
        }
        if(players.Count > 1)
        {
            secondPlayerCharacter.SetActive(true);
        }
    }

    public void SetText()
    {
        if (currentPlayersReady != players.Count)
        {
            playGame.text = "Prepared players " + currentPlayersReady.ToString() + "/" + players.Count.ToString();
            startTime = false;
        }
        else
        {
            time = maxTime;
            startTime = true;
        }
    }

    public List<Color> GetColors()
    {
        return colors;
    }

    public List<GameObject> GetBackGrounds()
    {
        return backGrounds;
    }

    public List<GameObject> GetBackGroundSquares()
    {
        return backGroundSquares;
    }

    public GameObject GetPlayerSprite()
    {
        for(int i = 0; i<playerSprites.Count; i++)
        {
            if (!playerSprites[i].activeSelf)
            {
                return playerSprites[i];
            }

        }
        return null;
    }

    public GameObject GetPlayerCharcterSelection()
    {
        for (int i = 0; i < playerCharcterSelection.Count; i++)
        {
            if (!playerCharcterSelection[i].activeSelf)
            {
                return playerCharcterSelection[i];
            }
        }
        return null;
    }

    public GameObject GetPlayerUpgrades()
    {
        for (int i = 0; i < playerUpgrades.Count; i++)
        {
            if (!playerUpgrades[i].activeSelf)
            {
                return playerUpgrades[i];
            }
        }
        return null;
    }

    public GameObject GetTexts()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            if (!texts[i].activeSelf)
            {
                return texts[i];
            }
        }
        return null;
    }

    public int GetCurrentBackGround()
    {
        return currentBackGround;
    }

    public GameObject GetUpgrades()
    {
        return upgrades;
    }

    public void AddPlayer(LobbyController _player)
    {
        players.Add(_player);
    }

    public void SetCurrentPlayersReady(int state)
    {
        currentPlayersReady += state;
    }

    public void SetCurrentBackGround(int state)
    {
        currentBackGround += state;
    }

}
