using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsMenu : MonoBehaviour
{
    MenuNaviguation menu;

    private void OnEnable()
    {
        if (!menu)
            menu = FindObjectOfType<MenuNaviguation>();

        menu.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
