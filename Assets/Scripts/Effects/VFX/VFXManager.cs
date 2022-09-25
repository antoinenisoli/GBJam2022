using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class VFXManager 
{
    public static VFXBank FXBank;

    public static void LoadData()
    {
        FXBank = Resources.Load<VFXBank>(nameof(VFXBank));
    }

    /// <summary>
    /// Spawn and play a stored VFX prefab at a given position by calling it by his name.
    /// </summary>
    public static GameObject PlayVFX(string name, Vector3 pos, bool destroy = true, Transform parent = null)
    {
        LoadData();

        if (FXBank == null)
        {
            Debug.LogError("Error, the vfx bank is null");
            return null;
        }

        Dictionary<string, GameObject> effects = FXBank.GetEffects();
        if (effects == null)
        {
            Debug.LogError("Error when loading vfx bank data");
            return null;
        }
        else
        {
            effects.TryGetValue(name, out GameObject prefab);
            if (!prefab)
                Debug.LogError("No vfx found with this name : " + name);
            else
            {
                GameObject newVFX = Object.Instantiate(prefab, pos, prefab.transform.rotation, parent);
                if (destroy)
                    Object.Destroy(newVFX, 1.5f);

                return newVFX;
            }

            return null;
        }
    }
}
