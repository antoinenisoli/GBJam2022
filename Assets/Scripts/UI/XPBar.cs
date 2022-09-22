using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] RectTransform fillTransform;
    [SerializeField] float maxWidth;
    [SerializeField] Text levelText;
    PlayerExperience xp;

    private void Start()
    {
        xp = GameplayManager.Instance.PlayerEXP;
    }

    private void Update()
    {
        levelText.text = "Level " + xp.currentLevel;
        float diff = (float)xp.Experience / (float)xp.GetNextLevel().stepAmount;
        Vector2 rect = fillTransform.sizeDelta;
        rect.x = maxWidth * diff; 
        fillTransform.sizeDelta = rect;
    }
}
