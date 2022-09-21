using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public override void Start()
    {
        base.Start();
        GameplayManager.Instance.AddEnemy(this);
    }

    public void Move(Vector2 targetPos, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }

    private void OnDestroy()
    {
        GameplayManager.Instance.RemoveEnemy(this);
    }
}
