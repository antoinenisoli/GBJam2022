using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    [SerializeField] List<Weapon> weapons = new List<Weapon>();

    public List<Weapon> Weapons { get => weapons; }

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        List<Weapon> copiedData = new List<Weapon>();
        for (int i = 0; i < weapons.Count; i++)
        {
            Weapon weapon = Instantiate(weapons[i]);
            copiedData.Add(weapon);
        }

        weapons = copiedData;
        foreach (var item in weapons)
            item.Init(this);
    }

    Dictionary<string, Weapon> SortWeapons()
    {
        Dictionary<string, Weapon> _storedWeapons = new Dictionary<string, Weapon>();
        foreach (var item in weapons)
            _storedWeapons.Add(item.WeaponName, item);

        return _storedWeapons;
    }

    public void NewWeapon(Weapon newWeapon)
    {
        if (SortWeapons().TryGetValue(newWeapon.WeaponName, out Weapon foundWeapon))
            foundWeapon.IncreaseLevel();
        else
        {
            Weapon weapon = Instantiate(newWeapon);
            weapon.Init(this);
            weapons.Add(weapon);
        }
    }

    private void Update()
    {
        foreach (var item in weapons)
            item.Update();
    }
}
