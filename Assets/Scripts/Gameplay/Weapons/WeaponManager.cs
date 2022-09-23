using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    Dictionary<string, Weapon> storedWeapons = new Dictionary<string, Weapon>();

    public List<Weapon> Weapons { get => weapons; }

    private void Awake()
    {
        List<Weapon> copiedData = new List<Weapon>();
        for (int i = 0; i < weapons.Count; i++)
        {
            Weapon weapon = Instantiate(weapons[i]);
            storedWeapons.Add(weapon.name, weapon);
            copiedData.Add(weapon);
        }

        weapons = copiedData;
        foreach (var item in weapons)
            item.Init(this);
    }

    public void NewWeapon(Weapon weapon)
    {
        if (storedWeapons.TryGetValue(weapon.name, out Weapon foundWeapon))
            foundWeapon.CurrentLevel++;
        else
            weapons.Add(Instantiate(weapon));
    }

    private void Update()
    {
        foreach (var item in weapons)
            item.Update();
    }
}
