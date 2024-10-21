using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private AbilityUI abilityCover1;
    [SerializeField] private AbilityUI ultimateCover1;
    [SerializeField] private AbilityUI abilityCover2;
    [SerializeField] private AbilityUI ultimateCover2;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public void InitTimer(bool isUltimate, Character character)
    {
        if (!isUltimate && character == PlayersManager.instance.GetPlayersList()[0].GetComponent<Character>())
            abilityCover1.InitTimer();
        else if (!isUltimate)
            abilityCover2.InitTimer();
        else if(character == PlayersManager.instance.GetPlayersList()[0].GetComponent<Character>())
            ultimateCover1.InitTimer();
        else
            ultimateCover2.InitTimer();
    }


}
