using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    public static WeaponContainer Instance;
    [SerializeField] List<Weapon> unlockedWeapons = new List<Weapon>();

    public List<Weapon> UnlockedWeapons { get => unlockedWeapons; }
    List<Weapon> AllWeapons => RewardManager.Instance.AllWeapons;

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
            if (item.LevelMax())
                AllWeapons.RemoveAll(s => s.WeaponName == item.WeaponName);
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
