using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MeleEnemy : Enemy
{
    private void Awake()
    {
        Initialize();
    }
    protected override void Update()
    {
        base.Update();
    }
}
