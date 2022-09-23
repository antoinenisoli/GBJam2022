using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUISlot : MonoBehaviour
{
    [SerializeField] Image weaponImage;
    [SerializeField] Text levelText;

    public void SetWeapon(Weapon weapon)
    {
        weaponImage.sprite = weapon.icon;
        levelText.text = "Level " + (weapon.CurrentLevel + 1);
    }
}
