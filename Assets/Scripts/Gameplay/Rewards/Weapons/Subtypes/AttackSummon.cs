using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AttackSummon), menuName = "Weapons/" + nameof(AttackSummon))]
public class AttackSummon : Weapon
{
    [Header(nameof(AttackSummon))]
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float spawnRadius = 30f, attackRadius = 5f;
    [SerializeField] string fxName = "Strike", soundName;

    Vector2 RandomPosition()
    {
        Vector2 circle = Random.insideUnitCircle * spawnRadius;
        return (Vector2)manager.transform.position + circle;
    }

    public override void Execute()
    {
        Vector2 randomPos = RandomPosition();
        VFXManager.PlayVFX(fxName, randomPos);
        SoundManager.Instance.PlayAudio(soundName);

        Collider2D[] targets = Physics2D.OverlapCircleAll(randomPos, attackRadius, targetLayer);
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
