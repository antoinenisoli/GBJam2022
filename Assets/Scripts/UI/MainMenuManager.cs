using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject startMenu, heroSelection;

    public void PlayGame() //event method
    {
        heroSelection.SetActive(true);
        startMenu.SetActive(false);
    }

    public void QuitGame() //event method
    {
        Application.Quit();
    }

    public void SetPlayerPref(int index) //event method
    {
        PlayerPrefs.SetInt("SelectedHero", index);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
