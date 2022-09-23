using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float commonProb, specialProb, rareProb;
    [SerializeField] Weapon[] allWeapons;
    WeaponSelectionUI[] weaponSelectors;
    WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        weaponSelectors = GetComponentsInChildren<WeaponSelectionUI>();

        List<Weapon> randomWeapons = RandomWeapons();
        for (int i = 0; i < weaponSelectors.Length; i++)
            weaponSelectors[i].SetWeapon(randomWeapons[i]);
    }

    List<Weapon> WeaponsByQuality(WeaponQuality quality)
    {
        List<Weapon> weapons = new List<Weapon>();
        foreach (var item in allWeapons)
        {
            if (item.Quality == quality)
                weapons.Add(item);
        }

        return weapons;
    }

    public List<Weapon> RandomWeapons()
    {
        List<Weapon> randomWeapons = new List<Weapon>();
        for (int i = 0; i < 3; i++)
        {
            float randomProb = Random.Range(0f, 1f);
            List<Weapon> weaponList = new List<Weapon>();
            if (randomProb < rareProb)
                weaponList = WeaponsByQuality(WeaponQuality.Rare);
            else if (randomProb < specialProb)
                weaponList = WeaponsByQuality(WeaponQuality.Special);
            else if (randomProb < commonProb)
               weaponList = WeaponsByQuality(WeaponQuality.Common);

            int randomIndex = Random.Range(0, weaponList.Count);
            weaponList.Add(weaponList[randomIndex]);
        }

        return randomWeapons;
    }

    public void SelectWeapon(int i)
    {
        weaponManager.NewWeapon(weaponSelectors[i].myWeapon);
        EventManager.Instance.onNewWeapon.Invoke();
    }
}
