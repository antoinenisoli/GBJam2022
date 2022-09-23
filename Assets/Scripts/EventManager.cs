using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public UnityEvent onPlayerNextLevel = new UnityEvent();
    public UnityEvent onPlayerHeal = new UnityEvent(), onPlayerHit = new UnityEvent(), onPlayerDeath = new UnityEvent();
    public UnityEvent onNewWeapon = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public UnityEvent onSelectButton = new UnityEvent(), onQuitSelect = new UnityEvent();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
