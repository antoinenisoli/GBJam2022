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

    public virtual void TakeDmg(float amount)
    {
        if (hitTimer < hitDuration)
            return;

        if (characterRenderer)
            Hit();

        MyHealth.CurrentHealth -= amount;
        hitTimer = 0;
        if (MyHealth.isDead)
            Destroy(gameObject);
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

    void Hit()
    {
        StopCoroutine(HitFlash());
        StartCoroutine(HitFlash());
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
