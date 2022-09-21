using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AutoAssignGBMaterial : MonoBehaviour
{
    [SerializeField] Material gbMaterial;

    [ContextMenu(nameof(UpdateRenderers))]
    private void UpdateRenderers()
    {
        if (!gbMaterial)
            return;

        foreach (Image image in GetComponentsInChildren<Image>())
            image.material = gbMaterial;
    }

    private void Update()
    {
        UpdateRenderers();
    }
}
