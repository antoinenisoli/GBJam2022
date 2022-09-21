using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ChasePlayer : EnemyAction
{
    [SerializeField] float speed = 10f;
    [SerializeField] float minDistance;

    public override void DoAction()
    {
        if (player)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance > minDistance)
                myEnemy.Move(player.transform.position, speed);
        }
    }

    private void Update()
    {
        DoAction();
    }
}
