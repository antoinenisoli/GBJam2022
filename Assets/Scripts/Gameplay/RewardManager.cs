using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    [SerializeField] List<Weapon> allWeapons = new List<Weapon>();

    public List<Weapon> AllWeapons { get => allWeapons; }

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
