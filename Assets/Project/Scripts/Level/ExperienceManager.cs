using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;

    [SerializeField] private float initExperience;
    [SerializeField] private float multiplier;
    [SerializeField] private List<PlayerInformation> playerInformation;
    [SerializeField] private List<RoguelikeUpgrade> roguelikeUpgrades;
    private int currentLevel;
    private float experience;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        currentLevel = 0;
        experience = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)) {
            SetExperience(experience + 20);
        }
    }
    public void SetExperience(float _experience)
    {
        experience += _experience;

        if (initExperience < experience)
        {
            experience -= initExperience;
            initExperience *= multiplier;
            currentLevel++;
            for(int i = 0; i < playerInformation.Count; i++) 
            {
                playerInformation[i].SetLevelText(currentLevel);
            }

            SetExperience(0);
            //Selecionar cartas y parar tiempo
            RoguelikeCanvas.Instance.LevelUp();

        }
        for (int i = 0; i < playerInformation.Count; i++)
        {
            playerInformation[i].SetExperienceBar(experience / initExperience);
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
