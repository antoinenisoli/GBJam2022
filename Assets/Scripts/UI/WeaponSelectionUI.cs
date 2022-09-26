using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionUI : MonoBehaviour
{
    public Weapon myWeapon;
    [SerializeField] Image weaponImage;
    [SerializeField] Text descriptionText;
    [SerializeField] RectTransform levelContainer;
    [SerializeField] Sprite[] levelSprites;

    private void Awake()
    {
        SetLevelSprites();
    }

    public void SetLevelSprites()
    {
        for (int i = 0; i < levelContainer.childCount; i++)
        {
            Transform child = levelContainer.GetChild(i);
            child.GetChild(0).GetComponentInChildren<Image>().sprite = levelSprites[i];
        }
    }

    public void UpdateLevels(int level)
    {
        for (int i = 0; i < levelContainer.childCount; i++)
        {
            Transform child = levelContainer.GetChild(i);
            child.gameObject.SetActive(i < (level + 1));
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        myWeapon = weapon;
        weaponImage.sprite = weapon.icon;
        descriptionText.text = weapon.description;

        //check if the weapon is already unlocked or not
        if (ContainsWeaponName(weapon, out Weapon unlockedWeapon))
            UpdateLevels(unlockedWeapon.CurrentLevel + 1);
        else
            UpdateLevels(weapon.CurrentLevel);
    }

    bool ContainsWeaponName(Weapon weaponToCompare, out Weapon sameWeapon)
    {
        foreach (var unlockedWeapon in WeaponContainer.Instance.UnlockedWeapons)
            if (weaponToCompare.WeaponName == unlockedWeapon.WeaponName)
            {
                sameWeapon = unlockedWeapon;
                return true;
            }

        sameWeapon = null;
        return false;
    }
}
