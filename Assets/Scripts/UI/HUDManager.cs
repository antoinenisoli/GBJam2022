using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] GameObject weaponMenu, weaponSelection, gameOver;
    float timer;

    private void Start()
    {
        EventManager.Instance.onPlayerNextLevel.AddListener(OpenWeaponSelection);
        EventManager.Instance.onNewWeapon.AddListener(CloseWeaponSelection);

        EventManager.Instance.onSelectButton.AddListener(OpenWeaponMenu);
        EventManager.Instance.onQuitSelect.AddListener(CloseWeaponMenu);

        EventManager.Instance.onGameOver.AddListener(OpenGameOver);
        gameOver.SetActive(false);
        weaponMenu.SetActive(false);
        weaponSelection.SetActive(false);
    }

    public void OpenGameOver()
    {
        gameOver.SetActive(true);
    }

    public void OpenWeaponMenu()
    {
        weaponMenu.SetActive(true);
    }

    public void CloseWeaponMenu()
    {
        weaponMenu.SetActive(false);
    }

    public void OpenWeaponSelection()
    {
        weaponSelection.SetActive(true);
    }

    public void CloseWeaponSelection()
    {
        weaponSelection.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("00:00");
    }
}
