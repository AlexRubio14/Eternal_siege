using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeGroup
{
    public GameObject[] upgrades;
}

public class ActiveUpgrades : MonoBehaviour
{
    [SerializeField] private UpgradeGroup[] upgradesGroup;

    public void SearchAbility(int index1, int index2)
    {
        upgradesGroup[index1].upgrades[index2].gameObject.GetComponent<Upgrade>().UpgradeAbility();
    }
}


