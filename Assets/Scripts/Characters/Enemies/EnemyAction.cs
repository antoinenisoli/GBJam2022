using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyAction : MonoBehaviour
{
    protected Enemy myEnemy;

    protected PlayerController player => PlayerController.Instance;

    public void Awake()
    {
        Init(GetComponent<Enemy>());
    }

    public void Init(Enemy myEnemy)
    {
        this.myEnemy = myEnemy;
    }

    public abstract void DoAction();
}
