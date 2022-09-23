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
            child.gameObject.SetActive(i < level);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        myWeapon = weapon;
        weaponImage.sprite = weapon.icon;
        descriptionText.text = weapon.description;
        UpdateLevels(weapon.CurrentLevel);
    }
}
