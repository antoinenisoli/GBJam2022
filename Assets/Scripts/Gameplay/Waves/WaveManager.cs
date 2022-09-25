using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    Queue<Wave> waveQueue = new Queue<Wave>();
    Wave currentWave;

    private void Awake()
    {
        waveQueue = new Queue<Wave>(waves);
        foreach (var item in waveQueue)
            item.Init();

        StartCoroutine(NewWave());
    }

    IEnumerator NewWave()
    {
        currentWave = null;
        Wave newWave = waveQueue.Dequeue();
        yield return new WaitForSeconds(newWave.startDelay);
        currentWave = newWave;
    }

    void Spawning()
    {
        if (waveQueue.Count <= 0 || currentWave == null)
            return;

        currentWave.ManageSpawning();
        if (currentWave.Update())
            StartCoroutine(NewWave());
    }

    private void Update()
    {
        Spawning();
    }
}
