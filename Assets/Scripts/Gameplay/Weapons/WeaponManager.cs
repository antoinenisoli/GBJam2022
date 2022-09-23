using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    Dictionary<string, Weapon> storedWeapons = new Dictionary<string, Weapon>();

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
            storedWeapons.Add(weapon.name, weapon);
            weapon.UnlockLevel();
            copiedData.Add(weapon);
        }

        weapons = copiedData;
        foreach (var item in weapons)
            item.Init(this);
    }

    public void NewWeapon(Weapon newWeapon)
    {
        if (storedWeapons.TryGetValue(newWeapon.name, out Weapon foundWeapon))
            foundWeapon.UnlockLevel();
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
