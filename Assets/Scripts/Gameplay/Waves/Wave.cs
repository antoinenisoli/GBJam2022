using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string Name;
    public float startDelay;
    [SerializeField] float duration = 1f;
    [SerializeField] Vector2 randomCooldown = new Vector2(5, 6);
    [SerializeField] EnemySpawner[] workingSpawners;
    [SerializeField] GameObject[] enemiesToSpawn;
    float cooldown;
    float waveTimer, spawnTimer;

    public void Init()
    {
        cooldown = Random.Range(randomCooldown.x, randomCooldown.y);
    }

    public bool Update()
    {
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
        foreach (var item in workingSpawners)
        {
            item.Spawn(enemiesToSpawn[0]);
        }
    }
}