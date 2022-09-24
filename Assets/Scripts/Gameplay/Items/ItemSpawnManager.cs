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

    [Header("Item spawn")]
    [SerializeField] Transform itemParent;
    [SerializeField] float itemSpawnRadius = 150f;
    [SerializeField] Vector2 randomDelay;
    [SerializeField] SpawnData[] items;
    float spawnTimer, spawnDelay;

    [Header("XP items")]
    [SerializeField] ItemData[] xpItems;
    public static ItemSpawnManager Instance;

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            Gizmos.DrawWireSphere(PlayerController.Instance.transform.position, itemSpawnRadius);
        else
            Gizmos.DrawWireSphere(transform.position, itemSpawnRadius);
    }

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

    void TrySpawnItem()
    {
        while (true)
        {
            float random = Random.Range(0f, 1f);
            int randomIndex = Random.Range(0, items.Length);
            SpawnData data = items[randomIndex];
            if (random < data.Probability)
            {
                Vector2 position = GameDevHelper.RandomPosition(itemSpawnRadius) + (Vector2)PlayerController.Instance.transform.position;
                Instantiate(data.Prefab, position, Quaternion.identity, itemParent);
                break;
            }
        }
    }

    void ManageItemSpawn()
    {
        if (items.Length <= 0 || PlayerController.Instance)
            return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            spawnTimer = 0;
            spawnDelay = Random.Range(randomDelay.x, randomDelay.y);
            TrySpawnItem();
        }
    }

    private void Update()
    {
        ManageItemSpawn();
    }
}
