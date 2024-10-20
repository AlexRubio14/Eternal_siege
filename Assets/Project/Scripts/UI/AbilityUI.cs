using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    private Character character;
    [SerializeField] private bool isUltimate;
    [SerializeField] private bool isPlayer1;
    private float cooldown;
    private float timer;
    private bool timerIsInited;

    private void Awake()
    {   
        timerIsInited = false;
        timer = 0;
    }

    private void Update()
    {
        if (timerIsInited)
        {
            timer += (Time.deltaTime / cooldown) * TimeManager.instance.GetPaused();
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(1, 0, timer), transform.localScale.z);
            if (timer >= 1)
            {
                timer = 0;
                timerIsInited = false;
            }
        }
    }

    public void InitTimer()
    {
        timerIsInited = true;

        if (isPlayer1) 
            character = PlayersManager.instance.GetPlayersList()[0].GetComponent<Character>();
        else
            character = PlayersManager.instance.GetPlayersList()[1].GetComponent<Character>();

        if (isUltimate)
            cooldown = character.GetUltimateCooldown();
        else
            cooldown = character.GetAbilityCooldown();
    }
}
