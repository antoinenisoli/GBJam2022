using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    public int heroIndex;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject[] heroesPrefab;

    private void Awake()
    {
        int index = PlayerPrefs.GetInt("SelectedHero");
        GameObject hero = heroesPrefab[index];
        Instantiate(hero, spawnPoint.transform.position, Quaternion.identity);
    }

    [ContextMenu(nameof(SetPlayerPref))]
    public void SetPlayerPref()
    {
        PlayerPrefs.SetInt("SelectedHero", heroIndex);
        print(PlayerPrefs.GetInt("SelectedHero"));
    }
}
