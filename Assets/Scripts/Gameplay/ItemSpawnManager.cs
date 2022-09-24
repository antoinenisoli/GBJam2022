using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemQuality
{
    Low,
    Medium,
    Large,
}

public class ItemSpawnManager : MonoBehaviour
{
    [System.Serializable]
    struct ItemData
    {
        public GameObject Prefab;
        public ItemQuality Quality;
    }

    [System.Serializable]
    struct SpawnData
    {
        public GameObject Prefab;
        [Range(0,1)] public float Probability;
    }

    [SerializeField] float itemSpawnRadius = 150f;
    [SerializeField] Vector2 randomDelay;
    [SerializeField] SpawnData[] items;
    float spawnTimer, spawnDelay;

    [SerializeField] ItemData[] xpItems;
    public static ItemSpawnManager Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject SpawnXPItem(ItemQuality quality)
    {
        foreach (var item in xpItems)
            if (item.Quality == quality)
                return item.Prefab;

        return null;
    }

    void ManageItemSpawn()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            spawnTimer = 0;
            spawnDelay = Random.Range(randomDelay.x, randomDelay.y);
            float random = Random.Range(0, 1);
            int randomIndex = Random.Range(0, items.Length);

        }
    }

    private void Update()
    {
        ManageItemSpawn();
    }
}
