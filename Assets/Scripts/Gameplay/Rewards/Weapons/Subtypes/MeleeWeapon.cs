using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MeleeWeapon), menuName = "Weapons/" + nameof(MeleeWeapon))]
public class MeleeWeapon : Weapon
{
    [Header(nameof(MeleeWeapon))]
    [SerializeField] float attackRadius = 2f;
    [SerializeField] string fxName = "Slash", soundName;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] bool flipX, flipY;
    [SerializeField] Vector2[] attackPositions;
    int index = 0;

    public override void Execute()
    {
        Vector2 attackPos = attackPositions[index];
        Attack(attackPos);
        index++;
        index %= attackPositions.Length;
    }

    void Attack(Vector2 attackPos)
    {
        Vector2 worldPos = (Vector2)manager.transform.position + attackPos;
        GameObject fx = VFXManager.PlayVFX(fxName, worldPos, false, manager.transform);
        SoundManager.Instance.PlayAudio(soundName);

        if (worldPos.x < manager.transform.position.x)
            fx.GetComponentInChildren<SpriteRenderer>().flipX = true;
        if (worldPos.y < manager.transform.position.y)
            fx.GetComponentInChildren<SpriteRenderer>().flipY = true;

        if (flipX)
            fx.GetComponentInChildren<SpriteRenderer>().flipX = worldPos.x < manager.transform.position.x;
        if (flipY)
            fx.GetComponentInChildren<SpriteRenderer>().flipX = worldPos.y > manager.transform.position.y;

        Collider2D[] targets = Physics2D.OverlapCircleAll(worldPos, attackRadius, targetLayer);
        foreach (var item in targets)
        {
            Enemy enemy = item.transform.GetComponentInParent<Enemy>();
            if (enemy)
            {
                VFXManager.PlayVFX("HitSpark", enemy.transform.position);
                if (delayBeforeHit > 0)
                    enemy.TakeDmgDelayed(LevelData.damage, delayBeforeHit);
                else
                    enemy.TakeDmg(LevelData.damage);
            }
        }
    }
}
