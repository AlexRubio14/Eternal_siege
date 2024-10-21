using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ChooseUpgrade : MonoBehaviour
{
    private List<GameObject> cards;
    private List<float> moveTimer;
    private List<bool> canMove;
    int index;

    private void Start()
    { 
        index = 0;
        moveTimer = new List<float>() { 0,0};
        canMove = new List<bool>() { true, true};
    }

    private void Update()
    {
        for(int i = 0; i < moveTimer.Count; i++)
        {
            if (moveTimer[i] > 0.3f && !canMove[i])
            {
                canMove[i] = true;
            }
            moveTimer[i] += Time.deltaTime;
        }
    }
    public void MoveRight(InputAction.CallbackContext obj)
    {
        if(obj.started && index < cards.Count - 1 && canMove[0]) 
        {
            cards[index].GetComponent<Outline>().enabled = false;
            index++;
            cards[index].GetComponent<Outline>().enabled = true;
            canMove[0] = false;
            moveTimer[0] = 0;
        }
    }

    public void MoveLeft(InputAction.CallbackContext obj)
    {
        if (obj.started && index > 0 && canMove[1])
        {
            cards[index].GetComponent<Outline>().enabled = false;
            index--;
            cards[index].GetComponent<Outline>().enabled = true;
            canMove[1] = false;
            moveTimer[1] = 0;
        }
    }

    public void ActiveCard(InputAction.CallbackContext obj)
    {
        if(obj.started)
            cards[index].GetComponent<RoguelikeUpgrade>().ActiveUprade();
    }

    public void SetCard(List<GameObject> _cards)
    {
        cards = new List<GameObject>(3);
        for (int i = 0; i< _cards.Count; i++) 
        {
            cards.Add(_cards[i]);
            cards[i].GetComponent<Outline>().enabled = false;
        }
        cards[index].GetComponent<Outline>().enabled = true;
    }
}
