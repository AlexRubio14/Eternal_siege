using UnityEngine;

public class RoguelikeManager : MonoBehaviour
{
    public static RoguelikeManager instance;

    [SerializeField] private GameObject onePlayerCanvas;
    [SerializeField] private GameObject twoPlayersCanvas;

    public GameObject gameCanvas;

    private void Awake()
    {
        if(instance != null || instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;

        DontDestroyOnLoad(instance);

        switch (Input.GetJoystickNames().Length) 
        {
            case 1: 
                gameCanvas = onePlayerCanvas;
                break;
            case 2: 
                gameCanvas = twoPlayersCanvas; 
                break;
            default: 
                break;
        }
    }
}
