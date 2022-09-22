using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MeleeAttack), menuName = "Weapons/" + nameof(MeleeAttack))]
public class MeleeAttack : Weapon
{
    [SerializeField] float attackRadius = 2f, damage = 10f;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Vector2[] attackPositions;
    int index = 0;

    public override void Execute()
    {
        Debug.Log(index);
        Vector2 attackPos = attackPositions[index];
        Attack(attackPos);
        index++;
        index %= attackPositions.Length;
    }

    void Attack(Vector2 attackPos)
    {
        Vector2 worldPos = (Vector2)manager.transform.position + attackPos;
        GameObject fx = VFXManager.PlayVFX("Slash", worldPos, false, manager.transform);
        if (worldPos.x < manager.transform.position.x)
            fx.GetComponentInChildren<SpriteRenderer>().flipX = true;

        Collider2D[] targets = Physics2D.OverlapCircleAll(worldPos, attackRadius, targetLayer);
        foreach (var item in targets)
        {
            Enemy enemy = item.transform.GetComponentInParent<Enemy>();
            if (enemy)
            {
                VFXManager.PlayVFX("HitSpark", enemy.transform.position);
                enemy.TakeDmg(damage);
            }
        }
    }
}
