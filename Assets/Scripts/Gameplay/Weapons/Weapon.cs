using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponLevel
{
    public float rate, damage;
}

public abstract class Weapon : ScriptableObject
{
    [Header(nameof(Weapon))]
    public Sprite icon;
    [SerializeField] protected WeaponLevel[] levels = new WeaponLevel[3];
    [SerializeField] int currentLevel;
    protected WeaponManager manager;
    float timer = 0;

    public WeaponLevel LevelData => levels[CurrentLevel];

    public int CurrentLevel 
    { 
        get => currentLevel; 
        set
        {
            if (value < 0)
                value = 0;
            else if (value > levels.Length)
                value = levels.Length - 1;

            currentLevel = value;
        }
    }

    public void Init(WeaponManager manager)
    {
        this.manager = manager;
    }

    public void NextLevel()
    {
        CurrentLevel++;
    }

    public abstract void Execute();

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > LevelData.rate)
        {
            timer = 0;
            Execute();
        }
    }
}
