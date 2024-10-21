using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoguelikeUpgrade : MonoBehaviour
{
    [SerializeField] private int index;

    [SerializeField] private float healthUpgradeValue;
    [SerializeField] private float speedUpgradeValue;
    [SerializeField] private float attackSpeedUpgradeValue;
    [SerializeField] private float armorUpgradeValue;
    [SerializeField] private float pickUpRadiusValue;
    [SerializeField] private float damageUpgradeValue;
    [SerializeField] private float thunderScaleValue;
    [SerializeField] private GameObject magicCapeObject;

    private int currentUpgrade;
    private GameObject player;

    public void ActiveUprade()
    {
        switch (currentUpgrade)
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

        RoguelikeManager.instance.SetPlayersHaveSelectedUpgradeList(index, true);
        RoguelikeManager.instance.CheckIfAllPlayersHaveSelectedUpgrade();
        transform.parent.gameObject.SetActive(false);
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

        if (generateThunder.enabled)
        {
            generateThunder.AddScale(thunderScaleValue);
        }
        else
        {
            generateThunder.enabled = true;
            return;
        }
    }

    private void UpgradeCape()
    {
        if (player.GetComponent<Character>().GetMagicCapeSpawned())
        {
            player.transform.Find("MagicCape(Clone)").GetComponent<MagicCape>().LevelUp();
        }
        else
        {
            GameObject _magixCape = Instantiate(magicCapeObject);
            _magixCape.transform.SetParent(player.transform, true);
            _magixCape.transform.position = new Vector3(player.transform.position.x, -2.55f, player.transform.position.z);
            player.GetComponent<Character>().SetMagicCapeSpawned(true);
            return;
        }
    }

    public void SetText()
    {
        switch(currentUpgrade)
        {
            case 0:
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Obtain 50 HP";
                break;
            case 1:
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "The expeience radius increase";
                break;
            case 2:
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Increase the attack speed";
                break;
            case 3:
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Run faster";
                break;
            case 4:
                if(!player.GetComponent<GenerateThunder>().enabled)
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Generate swamps that damage the enemies";
                else
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Increase swamps scale";
                break;
            case 5:
                if (!player.GetComponent<Character>().GetMagicCapeSpawned())
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Generate a magic cape under the player that damage the enemies";
                else
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Increase this magic cap scale";
                break;
            }
        }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    public void SetCurrentUpgrade(int _currentUpgrade)
    {
        currentUpgrade = _currentUpgrade;
    }

    public int GetIndex()
    {
        return index;
    }

    public int GetCurrentUpgrade()
    {
        return currentUpgrade;
    }
}
