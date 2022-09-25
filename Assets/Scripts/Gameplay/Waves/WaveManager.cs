using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] string totalMinutes;
    [SerializeField] Wave[] waves;
    Queue<Wave> waveQueue = new Queue<Wave>();
    Wave currentWave;

    private void OnValidate()
    {
        float minutes = 0;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].Name = "wave" + i;
            minutes += waves[i].duration;
        }

        minutes /= 60;
        totalMinutes = minutes.ToString("");
    }

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
        print($"new wave started : {newWave.Name} for {newWave.duration} minutes");
        yield return new WaitForSeconds(newWave.startDelay);
        currentWave = newWave;
    }

    void Spawning()
    {
        if (currentWave == null)
            return;

        currentWave.ManageSpawning();
        if (currentWave.Update() && waveQueue.Count > 0)
            StartCoroutine(NewWave());
    }

    private void Update()
    {
        Spawning();
    }
}
