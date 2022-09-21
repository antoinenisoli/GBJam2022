using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoShoot : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float shootRate = 2f;
    float timer;

    public void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile proj = newProjectile.GetComponent<Projectile>();
        Enemy closest = GameplayManager.Instance.ClosestEnemy();
        if (closest)
            proj.Shoot(closest.transform.position);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootRate)
        {
            timer = 0;
            ShootProjectile();
        }
    }
}
