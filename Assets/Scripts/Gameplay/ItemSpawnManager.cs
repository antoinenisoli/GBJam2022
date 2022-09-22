using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootQuality
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
        public LootQuality Quality;
    }

    [SerializeField] ItemData[] xpItems;
    public static ItemSpawnManager Instance;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject SpawnXPItem(LootQuality quality)
    {
        foreach (var item in xpItems)
            if (item.Quality == quality)
                return item.Prefab;

        return null;
    }
}
