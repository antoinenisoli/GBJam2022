using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] RectTransform fillTransform;
    [SerializeField] float maxWidth;
    PlayerExperience xp;

    private void Start()
    {
        xp = GameplayManager.Instance.PlayerEXP;
    }

    private void Update()
    {
        float diff = xp.experience / xp.GetNextLevel().stepAmount;
        Vector2 rect = fillTransform.sizeDelta;
        rect.x = maxWidth * diff; 
        fillTransform.sizeDelta = rect;
    }
}
