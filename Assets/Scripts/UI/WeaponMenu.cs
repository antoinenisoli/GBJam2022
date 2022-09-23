using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    WeaponUISlot[] slots;
    WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        slots = GetComponentsInChildren<WeaponUISlot>();
        foreach (var item in slots)
            item.gameObject.SetActive(false);
    }

    private void Update()
    {
        foreach (var item in slots)
            item.gameObject.SetActive(false);

        for (int i = 0; i < weaponManager.Weapons.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].SetWeapon(weaponManager.Weapons[i]);
        }
    }
}
