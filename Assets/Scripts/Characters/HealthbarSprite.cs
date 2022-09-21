using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarSprite : MonoBehaviour
{
    [SerializeField] Transform fillTransform;
    [SerializeField] GameObject barContrainer;
    [SerializeField] bool onlyWhenInjured;
    Health targetHealth;
    Vector3 baseScale;

    private void Start()
    {
        targetHealth = GetComponentInParent<Entity>().MyHealth;
        baseScale = fillTransform.localScale;
    }

    private void Update()
    {
        float diff = targetHealth.GetDifference();
        if (onlyWhenInjured && diff == 1)
        {
            barContrainer.SetActive(false);
            return;
        }
        else
            barContrainer.SetActive(true);

        Vector3 newScale = baseScale;
        newScale.x = Mathf.Lerp(0, baseScale.x, diff);
        fillTransform.localScale = newScale;
    }
}
