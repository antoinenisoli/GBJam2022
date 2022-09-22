using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePush : Projectile
{
    [Header(nameof(ProjectilePush))]
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float areaRadius, pushForce;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }

    public override void Collision(Collider2D collision)
    {
        return;
    }

    private void FixedUpdate()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, areaRadius, targetLayer);
        foreach (var item in targets)
        {
            Enemy enemy = item.GetComponentInParent<Enemy>();
            if (enemy)
            {
                enemy.TakeDmg(damageAmount);
                Vector2 dir = enemy.transform.position - transform.position;
                enemy.Push(pushForce, dir);
            }
        }
    }
}
