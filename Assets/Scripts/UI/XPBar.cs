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
        if (xp.LevelMax())
            levelText.text = "Level " + (xp.CurrentLevel + 1) + " MAX";
        else
            levelText.text = "Level " + (xp.CurrentLevel + 1);

        float diff = (float)xp.Experience / (float)xp.GetNextLevel().stepAmount;
        Vector2 rect = fillTransform.sizeDelta;
        rect.x = maxWidth * diff; 
        fillTransform.sizeDelta = rect;
    }
}
