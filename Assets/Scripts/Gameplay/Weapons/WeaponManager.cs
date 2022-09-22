using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Weapon[] weapons;

    private void Awake()
    {
        Weapon[] copiedData = new Weapon[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
            copiedData[i] = Instantiate(weapons[i]);

        weapons = copiedData;
        foreach (var item in weapons)
            item.Init(this);
    }

    private void Update()
    {
        foreach (var item in weapons)
            item.Update();
    }
}
