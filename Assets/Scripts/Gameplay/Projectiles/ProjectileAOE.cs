using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAOE : Projectile
{
    [Header(nameof(ProjectileAOE))]
    [SerializeField] float areaRadius;
    [SerializeField] string destroySound;
    [SerializeField] LayerMask targetLayer;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }

    public override void Collision(Collider2D collision)
    {
        if (!collision.GetComponent<Enemy>())
            return;

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, areaRadius, targetLayer);
        if (targets.Length > 0)
        {
            VFXManager.PlayVFX("ExplosionFX", transform.position);
            SoundManager.Instance.PlayAudio(destroySound);

            foreach (var item in targets)
            {
                Enemy enemy = item.GetComponentInParent<Enemy>();
                if (enemy)
                    enemy.TakeDmg(damageAmount);
            }

            Destroy(gameObject);
        }
    }
}
