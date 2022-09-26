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
        EventManager.Instance.onCutsceneStart.AddListener(StartCutscene);
        StartDialog();
    }

    void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndFade()
    {
        GBMaterialFading.Instance.Fade(1, 0, ()=> { LoadGame(); });
    }

    void CutsceneEnd()
    {
        animator.SetTrigger("exit");
    }

    public void StartCutscene()
    {
        animator.SetTrigger("start");
    }

    public void StartDialog()
    {
        cutsceneUI.AssignDialogs(dialogs);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadGame();
    }
}
