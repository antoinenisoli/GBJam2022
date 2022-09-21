using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarSprite : MonoBehaviour
{
    [SerializeField] Transform fillTransform;
    Health targetHealth;
    Vector3 baseScale;

    private void Start()
    {
        targetHealth = GetComponentInParent<Entity>().MyHealth;
        baseScale = fillTransform.localScale;
    }

    private void Update()
    {
        Vector3 newScale = baseScale;
        newScale.x = Mathf.Lerp(0, baseScale.x, targetHealth.GetDifference());
        fillTransform.localScale = newScale;
    }
}
