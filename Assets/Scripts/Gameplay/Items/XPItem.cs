using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPItem : Item
{
    [SerializeField] int xpAmount = 5;

    public override void Effect(PlayerController player)
    {
        GameplayManager.Instance.PlayerEXP.AddXP(xpAmount);
    }
}
