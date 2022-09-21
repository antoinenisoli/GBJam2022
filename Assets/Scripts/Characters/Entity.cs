using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Health MyHealth;

    public virtual void Start()
    {
        MyHealth.Initialize();
    }
}
