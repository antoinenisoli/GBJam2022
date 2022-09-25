using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string Name;
    public float startDelay;
    public float duration = 1f;
    public bool infinite;
    [SerializeField] Vector2 randomCooldown = new Vector2(5, 6);
    [SerializeField] EnemySpawner[] workingSpawners;
    [SerializeField] GameObject[] enemiesToSpawn;
    float cooldown;
    float waveTimer, spawnTimer;

    public void Init()
    {
        cooldown = Random.Range(randomCooldown.x, randomCooldown.y);
    }

    float GetTime()
    {
        return 60 * duration;
    }

    public bool Update()
    {
        if (infinite)
            return false;

        waveTimer += Time.deltaTime;
        if (waveTimer > duration)
            return true;

        return false;
    }

    public void ManageSpawning()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > cooldown)
        {
            spawnTimer = 0;
            cooldown = Random.Range(randomCooldown.x, randomCooldown.y);
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        foreach (var spawner in workingSpawners)
        {
            foreach (var enemy in enemiesToSpawn)
                spawner.Spawn(enemy);
        }
    }
}