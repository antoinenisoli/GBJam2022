using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPItem : MonoBehaviour
{
    [SerializeField] int xpAmount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponentInParent<PlayerController>();
        if (player)
        {
            GameplayManager.Instance.PlayerEXP.AddXP(xpAmount);
            Destroy(gameObject);
        }
    }
}
