using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float commonProb, specialProb, rareProb;
    List<Weapon> availableWeapons = new List<Weapon>();
    WeaponSelectionUI[] weaponSelectors;

    private void Awake()
    {
        weaponSelectors = GetComponentsInChildren<WeaponSelectionUI>();
    }

    private void OnEnable()
    {
        SetNewWeapons();
    }

    void SetNewWeapons()
    {
        List<Weapon> randomWeapons = RandomWeapons();
        foreach (var item in weaponSelectors)
            item.gameObject.SetActive(false);

        for (int i = 0; i < randomWeapons.Count; i++)
        {
            weaponSelectors[i].gameObject.SetActive(true);
            weaponSelectors[i].SetWeapon(randomWeapons[i]);
        }
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
        availableWeapons = new List<Weapon>(RewardManager.Instance.AllWeapons);
        int count = 3;
        if (availableWeapons.Count < count)
            count = availableWeapons.Count;

        for (int i = 0; i < count; i++)
        {
            List<Weapon> weaponList = new List<Weapon>();
            while (weaponList.Count <= 0)
            {
                float randomProb = Random.Range(0f, 1f);
                if (randomProb < rareProb)
                    weaponList = WeaponsByQuality(WeaponQuality.Rare);
                else if (randomProb < specialProb)
                    weaponList = WeaponsByQuality(WeaponQuality.Special);
                else if (randomProb < commonProb)
                    weaponList = WeaponsByQuality(WeaponQuality.Common);
            }

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
        WeaponContainer.Instance.NewWeapon(weaponSelectors[i].myWeapon);
        EventManager.Instance.onNewWeapon.Invoke();
    }
}
