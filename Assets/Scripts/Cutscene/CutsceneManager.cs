using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Dialog[] dialogs;
    CutsceneUI cutsceneUI;
    Animator animator;

    private void Awake()
    {
        cutsceneUI = FindObjectOfType<CutsceneUI>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        EventManager.Instance.onCutsceneEnd.AddListener(CutsceneEnd);
    }

    public void EndFade()
    {
        GBMaterialFading.Instance.Fade(1, 0, ()=> { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); });
    }

    void CutsceneEnd()
    {
        animator.SetTrigger("exit");
    }

    public void StartDialog()
    {
        cutsceneUI.PlayDialog(dialogs);
    }
}
