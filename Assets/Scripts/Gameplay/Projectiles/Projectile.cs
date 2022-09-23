using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float damageAmount = 1f;
    [SerializeField] protected float speed = 5f, lifeTime = 5f;
    [SerializeField] protected int collisionResistance;
    [SerializeField] protected bool lookAtTarget;
    protected Vector2 trajectory;

    public void Shoot(Vector2 trajectory)
    {
        trajectory -= (Vector2)transform.position;
        this.trajectory = trajectory.normalized;
        Destroy(gameObject, lifeTime);
    }

    public virtual void Collision(Collider2D collision)
    {
        Enemy enemy = collision.GetComponentInParent<Enemy>();
        if (enemy)
        {
            enemy.TakeDmg(damageAmount);
            VFXManager.PlayVFX("HitSpark", transform.position);
            collisionResistance--;
            if (collisionResistance <= 0)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Collision(collision);
    }

    public void SetData(Weapon weapon)
    {
        damageAmount = weapon.LevelData.damage;
    }

    void LookAtTrajectory()
    {
        float angle = Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        if (lookAtTarget)
            LookAtTrajectory();

        transform.position += (Vector3)trajectory * Time.deltaTime * speed;
    }
}
