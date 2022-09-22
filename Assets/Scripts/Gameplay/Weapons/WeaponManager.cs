using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;

    private void Awake()
    {
        foreach (var item in weapons)
            item.Init(this);
    }

    private void Update()
    {
        foreach (var item in weapons)
            item.Update();
    }
}
