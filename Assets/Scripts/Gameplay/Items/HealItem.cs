using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    [SerializeField] int healAmount = 5;

    public override void Effect(PlayerController player)
    {
        player.Heal(healAmount);
    }
}
