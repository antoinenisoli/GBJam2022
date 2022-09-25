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
    [SerializeField] bool vertical = true;
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
        int axisInput;
        if (vertical)
            axisInput = (int)-Input.GetAxisRaw("Vertical");
        else
            axisInput = (int)Input.GetAxisRaw("Horizontal");

        if (axisInput != 0)
        {
            if (timer > 0.2f)
            {
                timer = 0;
                SoundManager.Instance.PlayAudio("moveArrow");
                Index += axisInput;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SoundManager.Instance.PlayAudio("selectOption");
            fakeButtons[Index].selectEvent.Invoke();
        }
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
        if (vertical)
            buttonPosition.y = fakeButtons[Index].myButton.position.y;
        else
            buttonPosition.x = fakeButtons[Index].myButton.position.x;

        arrow.position = buttonPosition;
    }
}
