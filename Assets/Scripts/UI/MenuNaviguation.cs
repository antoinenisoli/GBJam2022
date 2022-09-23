using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
struct FakeButton
{
    public Transform myButton;
    public UnityEvent selectEvent;
}

public class MenuNaviguation : MonoBehaviour
{
    [SerializeField] Transform arrow;
    [SerializeField] int index;
    [SerializeField] FakeButton[] fakeButtons;
    float timer;

    private void OnEnable()
    {
        StartCoroutine(UnscaledUpdate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    int Index 
    { 
        get => index;
        set
        {
            if (value < 0)
                value = fakeButtons.Length - 1;
            if (value > fakeButtons.Length - 1)
                value = 0;

            index = value;
        }
    }

    void Naviguation(float time)
    {
        timer += time;
        int axisInput = (int)Input.GetAxisRaw("Vertical");
        if (axisInput != 0)
        {
            if (timer > 0.2f)
            {
                timer = 0;
                Index -= axisInput;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
            fakeButtons[Index].selectEvent.Invoke();
    }

    IEnumerator UnscaledUpdate()
    {
        while (true)
        {
            yield return null;
            //print("unscaled update");
            Naviguation(Time.unscaledDeltaTime);
            MoveArrow();
        }
    }

    void MoveArrow()
    {
        Vector2 buttonPosition = arrow.position;
        buttonPosition.y = fakeButtons[Index].myButton.position.y;
        arrow.position = buttonPosition;
    }
}
