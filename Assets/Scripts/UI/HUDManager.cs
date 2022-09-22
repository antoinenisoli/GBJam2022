using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timerText;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("00:00");
    }
}
