using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int spawnCount = 3;
    [SerializeField] float positionRandomOffset = 3f;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnParent;
    [SerializeField] Vector2 randomCooldown = new Vector2(50, 60);
    float cooldown;
    float timer;

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.DrawWireSphere(transform.position, positionRandomOffset);

        Color c = Color.red;
        c.a = 0.2f;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, positionRandomOffset);
#endif
    }

    private void Start()
    {
        timer = 0;
        cooldown = Random.Range(randomCooldown.x, randomCooldown.y);
    }

    void ResetTimer()
    {
        Start();
        Spawn();
    }

    public Vector2 RandomPosition()
    {
        Vector2 circle = Random.insideUnitCircle;
        Vector2 random = new Vector2(circle.x, circle.y) * positionRandomOffset;
        return random;
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 newPos = (Vector2)transform.position + RandomPosition();
            Instantiate(enemyPrefab, newPos, Quaternion.identity, spawnParent);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
            ResetTimer();
    }
}
