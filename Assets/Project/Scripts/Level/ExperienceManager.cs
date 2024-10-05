using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;

    [SerializeField] private List<float> experienceCuantity;
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

    public void SetExperience(float _experience)
    {
        experience += _experience;

        if (experienceCuantity[currentLevel] < experience)
        {
            experience -= experienceCuantity[currentLevel];
            currentLevel++;
            PlayerInformation.instance.SetLevelText(currentLevel);

            //Selecionar cartas y parar tiempo

        }

        PlayerInformation.instance.SetExperienceBar(experience / experienceCuantity[currentLevel]);
    }
}
