using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetInformation : MonoBehaviour
{
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<GameObject> backGrounds;
    [SerializeField] private List<GameObject> backGroundSquares;
    [SerializeField] private List<GameObject> playerSprites;
    [SerializeField] private List<GameObject> playerCharcterSelection;

    [SerializeField] private TextMeshProUGUI playGame;
    [SerializeField] private float maxTime;
    [SerializeField] private GameObject secondPlayerCharacter;
    [SerializeField] private List<GameObject> texts;

    private List<GameObject> players;
    private int currentPlayersReady;
    private float time;

    private void Start()
    {
        players = new List<GameObject>(2);
        currentPlayersReady = 0;
        time = maxTime;
        secondPlayerCharacter.SetActive(false);
    }

    private void Update()
    {
        if(players.Count > 0)
        {
            SetText();
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
            time = maxTime;
        }
        else
        {
            time -= Time.deltaTime;
            playGame.text = "The game starts in " + Mathf.FloorToInt(time).ToString(); 
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

    public void AddPlayer(GameObject _player)
    {
        players.Add(_player);
    }

    public void SetCurrentPlayersReady(int state)
    {
        currentPlayersReady += state;
    }


}
