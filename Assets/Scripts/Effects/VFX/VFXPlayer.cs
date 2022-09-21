using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayer : MonoBehaviour
{
    [SerializeField] string[] vfxNames;

    public void PlayVFX(int index)
    {
        GameObject vfx = VFXManager.PlayVFX(vfxNames[index], transform.position, true);
        if (vfx && vfx.activeInHierarchy)
            vfx.SetActive(true);
    }
}
