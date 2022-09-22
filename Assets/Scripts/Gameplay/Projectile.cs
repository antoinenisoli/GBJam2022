using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damageAmount = 1f;
    [SerializeField] float speed = 5f;
    [SerializeField] float lifeTime = 5f;
    Vector2 trajectory;

    public void Shoot(Vector2 trajectory)
    {
        trajectory -= (Vector2)transform.position;
        this.trajectory = trajectory.normalized;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponentInParent<Enemy>();
        if (enemy)
        {
            enemy.TakeDmg(damageAmount);
            VFXManager.PlayVFX("HitSpark", transform.position);
            Destroy(gameObject);
        }
    }

    void LookAtTrajectory()
    {
        float angle = Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        LookAtTrajectory();
        transform.position += (Vector3)trajectory * Time.deltaTime * speed;
    }
}
