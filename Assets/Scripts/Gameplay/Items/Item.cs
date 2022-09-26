using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract void Effect(PlayerController player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponentInParent<PlayerController>();
        if (player)
        {
            SoundManager.Instance.PlayAudio("itemSound");
            Effect(player);
            Destroy(gameObject);
        }
    }
}
