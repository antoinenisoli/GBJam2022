using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public UnityEvent onPlayerNextLevel = new UnityEvent();
    public UnityEvent onPlayerHeal = new UnityEvent();
    public UnityEvent onPlayerHit = new UnityEvent();
    public UnityEvent onNewWeapon = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
