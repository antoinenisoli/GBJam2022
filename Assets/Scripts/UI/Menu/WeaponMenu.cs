using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenu : MonoBehaviour
{
    [SerializeField] RectTransform slotContainer;
    WeaponUISlot[] slots;
    WeaponContainer weaponManager => WeaponContainer.Instance;

    private void Awake()
    {
        slots = GetComponentsInChildren<WeaponUISlot>();
        foreach (var item in slots)
            item.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (var item in slots)
            item.gameObject.SetActive(false);

        for (int i = 0; i < weaponManager.UnlockedWeapons.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].SetWeapon(weaponManager.UnlockedWeapons[i]);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(slotContainer);
    }

    /*private void Update()
    {
        foreach (var item in slots)
            item.gameObject.SetActive(false);

        for (int i = 0; i < weaponManager.UnlockedWeapons.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].SetWeapon(weaponManager.UnlockedWeapons[i]);
        }
    }*/
}
