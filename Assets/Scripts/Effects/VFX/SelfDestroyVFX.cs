using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyVFX : MonoBehaviour
{
    ParticleSystem fx;

    public ParticleSystem FX
    {
        get
        {
            if (!fx)
                fx = GetComponent<ParticleSystem>();

            return fx;
        }
    }

    IEnumerator Start()
    {
        FX.Play();
        yield return new WaitForSeconds(FX.main.duration + FX.main.startLifetimeMultiplier);
        Destroy(gameObject);
    }
}
