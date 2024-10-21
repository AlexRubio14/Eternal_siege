using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ChooseUpgrade : MonoBehaviour
{
    private List<GameObject> cards;
    int index;

    private void Start()
    {

        index = 0;
    }
    public void MoveRight(InputAction.CallbackContext obj)
    {
        if(obj.started && index < cards.Count - 1) 
        {
            cards[index].GetComponent<Outline>().enabled = false;
            index++;
            cards[index].GetComponent<Outline>().enabled = true;
        }
    }

    public void MoveLeft(InputAction.CallbackContext obj)
    {
        if (obj.started && index > 0)
        {
            cards[index].GetComponent<Outline>().enabled = false;
            index--;
            cards[index].GetComponent<Outline>().enabled = true;
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
