using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Health MyHealth;

    public void TakeDmg(float amount)
    {
        MyHealth.CurrentHealth -= amount;
        if (MyHealth.isDead)
            Destroy(gameObject);
    }

    public virtual void Start()
    {
        MyHealth.Initialize();
    }
}
