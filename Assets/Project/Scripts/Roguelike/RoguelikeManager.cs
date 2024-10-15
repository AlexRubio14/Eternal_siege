using UnityEngine;

public class RoguelikeManager : MonoBehaviour
{
    public static RoguelikeManager instance;

    private void Awake()
    {
        if(instance != null || instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;

        DontDestroyOnLoad(instance);
    }
}
