using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float commonProb, specialProb, rareProb;
    [SerializeField] Weapon[] allWeapons;
    List<Weapon> availableWeapons = new List<Weapon>();
    WeaponSelectionUI[] weaponSelectors;

    private void Awake()
    {
        weaponSelectors = GetComponentsInChildren<WeaponSelectionUI>();

        List<Weapon> randomWeapons = RandomWeapons();
        for (int i = 0; i < weaponSelectors.Length; i++)
            weaponSelectors[i].SetWeapon(randomWeapons[i]);
    }

    List<Weapon> WeaponsByQuality(WeaponQuality quality)
    {
        List<Weapon> weapons = availableWeapons.ToList();
        weapons.RemoveAll(s => s.Quality != quality);
        return weapons;
    }

    public List<Weapon> RandomWeapons()
    {
        List<Weapon> randomWeapons = new List<Weapon>();
        availableWeapons = allWeapons.ToList();

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

            while (true)
            {
                int randomIndex = Random.Range(0, weaponList.Count);
                Weapon randomWeapon = weaponList[randomIndex];
                if (!ContainsWeaponName(randomWeapon, randomWeapons))
                {
                    randomWeapons.Add(randomWeapon);
                    availableWeapons.Remove(randomWeapon);
                    break;
                }
            }
        }

        return randomWeapons;
    }

    bool ContainsWeaponName(Weapon weapon, List<Weapon> weaponList)
    {
        return weaponList.Any(n => n.WeaponName == weapon.WeaponName);
    }

    public void SelectWeapon(int i) //event method
    {
        WeaponManager.Instance.NewWeapon(weaponSelectors[i].myWeapon);
        EventManager.Instance.onNewWeapon.Invoke();
    }
}
