using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ProjectileShooter), menuName = "Weapons/" + nameof(ProjectileShooter))]
public class ProjectileShooter : Weapon
{
    [Header(nameof(ProjectileShooter))]
    [SerializeField] GameObject projectilePrefab;

    public override void Execute()
    {
        Enemy closest = GameplayManager.Instance.ClosestEnemy();
        if (!closest)
            return;

        GameObject newProjectile = Instantiate(projectilePrefab, manager.transform.position, Quaternion.identity);
        Projectile proj = newProjectile.GetComponent<Projectile>();
        proj.Shoot(closest.transform.position);
        proj.SetData(this);
    }
}
