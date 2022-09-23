using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class SpawnData
{
    [Range(0, 1)] public float Probability = 0.5f;
    public ItemQuality Quality;

    public void TrySpawn(Vector2 position)
    {
        float f = Random.Range(0f, 1f);
        if (f < Probability)
        {
            GameObject xpItem = ItemSpawnManager.Instance.SpawnXPItem(Quality);
            if (xpItem)
                Object.Instantiate(xpItem, position, Quaternion.identity);
        }
    }
}

public class Enemy : Entity
{
    [Header(nameof(Enemy))]
    [SerializeField] SpawnData spawnData;
    Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        GameplayManager.Instance.AddEnemy(this);
    }

    public void Push(float force, Vector2 direction)
    {
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    public void Move(Vector2 targetPos, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        characterRenderer.flipX = transform.position.x > targetPos.x;
    }

    void DetachSprite()
    {
        characterRenderer.material = spriteMat;
        animator.transform.parent = null;
        animator.enabled = true;
        animator.StartAnim("Death");
        animator.AutoDestroy();
    }

    public override void Death()
    {
        DetachSprite();
        spawnData.TrySpawn(transform.position);
        GameplayManager.Instance.RemoveEnemy(this);

        base.Death();
    }
}
