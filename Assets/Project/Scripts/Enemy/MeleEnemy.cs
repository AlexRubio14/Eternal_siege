using UnityEngine;

public class MeleEnemy : Enemy
{
    [SerializeField] private bool hasToDropCard;
    private void Awake()
    {
        Initialize();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void Die()
    {
        base.Die();
        if(hasToDropCard)
        {
            RoguelikeCanvas.instance.LevelUp();
        }
    }
}
