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
    [SerializeField] private float thunderScaleValue;

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
                case 4:
                    UpgradeThunder();
                    break;
                 case 5:
                    UpgradeCape();
                    break;
            }

            AddUpgradeIndex();

            RoguelikeManager.instance.SetPlayersHaveSelectedUpgradeList(index, true);
            RoguelikeManager.instance.CheckIfAllPlayersHaveSelectedUpgrade();
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            AddHP();
            RoguelikeCanvas.instance.ReturnToGameplay();
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

    private void UpgradeThunder()
    {
        GenerateThunder generateThunder = player.GetComponent<GenerateThunder>();

        if (!generateThunder.enabled)
        {
            generateThunder.enabled = true;
        }
        else
        {
            generateThunder.AddScale(thunderScaleValue);
        }
    }

    private void UpgradeCape()
    {
        MagicCape magicCape = player.GetComponent<MagicCape>();

        if (!magicCape.enabled)
        {
            magicCape.enabled = true;
        }
        else
        {
            magicCape.LevelUp();
        }
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
