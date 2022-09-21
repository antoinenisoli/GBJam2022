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

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Image image = child.GetComponent<Image>();
            if (image)
                image.material = gbMaterial;
        }
    }

    private void Update()
    {
        UpdateRenderers();
    }
}
