using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    [SerializeField] List<Weapon> allWeapons = new List<Weapon>();
    [SerializeField] List<Weapon> unlockedWeapons = new List<Weapon>();

    public List<Weapon> UnlockedWeapons { get => unlockedWeapons; }
    public List<Weapon> AllWeapons { get => allWeapons; }

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        List<Weapon> copiedData = new List<Weapon>();
        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            Weapon weapon = Instantiate(unlockedWeapons[i]);
            copiedData.Add(weapon);
        }

        unlockedWeapons = copiedData;
        foreach (var item in unlockedWeapons)
            item.Init(this);
    }

    private void Start()
    {
        EventManager.Instance.onNewWeapon.AddListener(CheckWeapons);
    }

    void CheckWeapons()
    {
        foreach (var item in unlockedWeapons)
        {
            //print(item.WeaponName + " " + item.CurrentLevel);
            if (item.LevelMax())
            {
                Weapon weaponToRemove = SortWeapons()[item.WeaponName];
                AllWeapons.Remove(weaponToRemove);
            }
        }
    }

    Dictionary<string, Weapon> SortWeapons()
    {
        Dictionary<string, Weapon> _storedWeapons = new Dictionary<string, Weapon>();
        foreach (var item in unlockedWeapons)
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
            unlockedWeapons.Add(weapon);
        }
    }

    private void Update()
    {
        foreach (var item in unlockedWeapons)
            item.Update();
    }
}
