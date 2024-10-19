using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguelikeUpgrade : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private List<int> lvlUpgrade;
    private int upgradeIndex;

    [SerializeField] private float healthUpgradeValue;
    [SerializeField] private float speedUpgradeValue;
    [SerializeField] private float attackSpeedUpgradeValue;
    [SerializeField] private float armorUpgradeValue;
    [SerializeField] private float pickUpRadiusValue;
    [SerializeField] private float damageUpgradeValue;

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
                    AddPicUpRadius();
                    break;
                case 2:
                    AddAttackSpeed();
                    break;
                case 3:
                    AddSpeed();
                    break;
            }

            AddUpgradeIndex();

            RoguelikeManager.instance.SetPlayersHaveSelectedUpgradeList(index, true);
            RoguelikeManager.instance.CheckIfAllPlayersHaveSelectedUpgrade();
        }
        else
        {
            AddHP();
            RoguelikeCanvas.Instance.ReturnToGameplay();
            PlayersManager.instance.ChangeActionMap("Player");
        }
    }

    private void AddHP()
    {
        player.GetComponent<Character>().AddHealth(healthUpgradeValue);
    }

    private void AddAttackSpeed()
    {
        player.GetComponent<Character>().AddAttackSpeed(attackSpeedUpgradeValue);
    }

    private void AddSpeed()
    {
        player.GetComponent<Character>().AddSpeed(speedUpgradeValue);
    }

    private void AddPicUpRadius()
    {
        player.GetComponent<SphereCollider>().radius += pickUpRadiusValue;
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
