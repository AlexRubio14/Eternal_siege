using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;

    [SerializeField] private float initExperience;
    [SerializeField] private float multiplier;
    [SerializeField] private List<PlayerInformation> playerInformation;
    [SerializeField] Slider ExpSlider;
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
        ExpSlider.value = 0;
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
            RoguelikeCanvas.instance.LevelUp();

        }
        ExpSlider.value = experience / initExperience;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
