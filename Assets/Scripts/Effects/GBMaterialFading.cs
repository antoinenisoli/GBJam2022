using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GBMaterialFading : MonoBehaviour
{
    public static GBMaterialFading Instance;

    [SerializeField] Material gbMaterial;
    [SerializeField] int fadeIterations = 20;
    [SerializeField] float fadeDelay = 0.1f;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (gbMaterial)
            StartCoroutine(FadeGB(0, 1));
    }

    private void OnApplicationQuit()
    {
        if (gbMaterial)
            gbMaterial.SetFloat("_Fade", 1);
    }

    public void Fade(float startValue, float endValue, UnityAction action = null)
    {
        StartCoroutine(FadeGB(startValue, endValue, action));
    }

    IEnumerator FadeGB(float startValue, float endValue, UnityAction action = null)
    {
        string shaderProperty = "_Fade";
        gbMaterial.SetFloat(shaderProperty, startValue);

        for (int i = 0; i < fadeIterations; i++)
        {
            float step = (float)i / (float)fadeIterations;
            float newValue = Mathf.Lerp(startValue, endValue, step);
            gbMaterial.SetFloat(shaderProperty, newValue);
            yield return new WaitForSecondsRealtime(fadeDelay);
        }

        gbMaterial.SetFloat(shaderProperty, endValue);
        if (action != null)
            action.Invoke();
    }
}
