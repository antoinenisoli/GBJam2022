using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Health MyHealth;
    [SerializeField] protected float hitDuration = 1f;
    float hitTimer;

    public virtual void TakeDmg(float amount)
    {
        if (hitTimer < hitDuration)
            return;

        MyHealth.CurrentHealth -= amount;
        hitTimer = 0;
        if (MyHealth.isDead)
            Destroy(gameObject);
    }

    public virtual void DoUpdate() { }

    public virtual void Start()
    {
        hitTimer = hitDuration;
        MyHealth.Initialize();
    }

    public void Update()
    {
        hitTimer += Time.deltaTime;
        DoUpdate();
    }
}
