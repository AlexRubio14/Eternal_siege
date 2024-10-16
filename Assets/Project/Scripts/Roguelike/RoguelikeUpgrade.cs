using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguelikeUpgrade : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private List<int> lvlUpgrade;
    private int upgradeIndex;

    private GameObject player;

    private void Start()
    {
        upgradeIndex = 0;
    }

    public void ActiveUprade()
    {
        if (lvlUpgrade.Count > upgradeIndex)
        {
            switch (lvlUpgrade[upgradeIndex])
            {
                case 0:
                    AddHP();
                    break;
                case 1:
                    AddArmor();
                    break;
                case 2:
                    AddAttackSpeed();
                    break;
                case 3:
                    AddSpeed();
                    break;
            }
        }
    }

    private void AddHP()
    {
        player.GetComponent<Character>().AddHealth(50);
    }

    private void AddArmor()
    {
        player.GetComponent<Character>().AddArmor(10);
    }

    private void AddAttackSpeed()
    {
        player.GetComponent<Character>().AddAttackSpeed(0.5f);
    }

    private void AddSpeed()
    {
        player.GetComponent<Character>().AddSpeed(150);
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    public int GetIndex() 
    { 
        return index; 
    }

    public void AddUpgradeIndex()
    {
        upgradeIndex++;
    }
}
