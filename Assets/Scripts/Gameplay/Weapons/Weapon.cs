using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public float rate = 0.2f;
    float timer = 0;
    protected WeaponManager manager;

    public void Init(WeaponManager manager)
    {
        this.manager = manager;
    }

    public abstract void Execute();

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > rate)
        {
            timer = 0;
            Execute();
        }
    }
}
