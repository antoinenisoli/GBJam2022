using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Transform arrow;
    [SerializeField] Transform buttonContrainer;
    Transform[] buttons;
    [SerializeField] int index;
    float timer;

    int Index 
    { 
        get => index;
        set
        {
            if (value < 0)
                value = buttonContrainer.childCount - 1;

            if (value > buttonContrainer.childCount - 1)
                value = 0;

            index = value;
        }
    }

    private void Awake()
    {
        buttons = new Transform[buttonContrainer.childCount];
        for (int i = 0; i < buttonContrainer.childCount; i++)
            buttons[i] = buttonContrainer.GetChild(i);
    }

    public void PlayGame() //ui button method
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() //ui button method
    {
        Application.Quit();
    }

    void Naviguation()
    {
        timer += Time.deltaTime;
        int axisInput = (int)Input.GetAxisRaw("Vertical");
        if (axisInput != 0)
        {
            if (timer > 0.2f)
            {
                timer = 0;
                Index -= axisInput;
            }
        }
    }

    void MoveArrow()
    {
        Vector2 buttonPosition = arrow.position;
        buttonPosition.y = buttons[Index].position.y;
        arrow.position = buttonPosition;
    }

    private void Update()
    {
        Naviguation();
        MoveArrow();
    }
}
