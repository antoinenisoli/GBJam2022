using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Health MyHealth;
    [SerializeField] protected Material hitMat;
    [SerializeField] protected SpriteRenderer characterRenderer;
    [SerializeField] protected float hitDuration = 1f;
    protected SimpleAnimator animator;
    float hitTimer;
    protected Material spriteMat;

    public void TakeDmg(float amount)
    {
        if (hitTimer < hitDuration)
            return;

        Hit(amount);
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDmgDelayed(float amount, float delay)
    {
        StartCoroutine(DelayedHit(amount, delay));
    }

    IEnumerator DelayedHit(float amount, float delay)
    {
        yield return new WaitForSeconds(delay);
        TakeDmg(amount);
    }

    public virtual void Heal(float amount)
    {
        MyHealth.CurrentHealth += amount;
    }

    public virtual void DoUpdate() { }

    public virtual void Start()
    {
        animator = GetComponentInChildren<SimpleAnimator>();
        spriteMat = characterRenderer.material;
        hitTimer = hitDuration;
        MyHealth.Initialize();
    }

    protected virtual void Hit(float amount)
    {
        if (characterRenderer)
        {
            StopCoroutine(HitFlash());
            StartCoroutine(HitFlash());
        }

        MyHealth.CurrentHealth -= amount;
        hitTimer = 0;
        if (MyHealth.isDead)
            Death();
    }

    IEnumerator HitFlash()
    {
        characterRenderer.material = hitMat;
        yield return new WaitForSeconds(0.2f);
        characterRenderer.material = spriteMat;
    }

    public void Update()
    {
        hitTimer += Time.deltaTime;
        DoUpdate();
    }
}
