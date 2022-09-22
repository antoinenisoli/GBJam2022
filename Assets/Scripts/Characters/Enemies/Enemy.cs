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
        characterRenderer.flipX = transform.position.x > targetPos.x;
    }

    private void OnDestroy()
    {
        characterRenderer.material = spriteMat;
        animator.transform.parent = null;
        animator.enabled = true;
        animator.StartAnim("Death");
        animator.AutoDestroy();

        GameplayManager.Instance.RemoveEnemy(this);
    }
}
