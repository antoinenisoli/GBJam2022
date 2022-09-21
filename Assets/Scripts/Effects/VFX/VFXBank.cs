using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(VFXBank), menuName = "Corneille/VFX/" + nameof(VFXBank))]
public class VFXBank : ScriptableObject
{
    public VFX[] vfx;

    public Dictionary<string, GameObject> GetEffects()
    {
        Dictionary<string, GameObject> datas = new Dictionary<string, GameObject>();
        foreach (var item in vfx)
            datas.Add(item.name, item.prefab);

        return datas;
    }
}
