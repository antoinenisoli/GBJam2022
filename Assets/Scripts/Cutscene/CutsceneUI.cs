using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CutsceneUI : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] Text dialogText;
    [SerializeField] Image portrait, illustrationImage;
    [SerializeField] float typeDelay = 0.2f;
    bool isTyping;
    string currentLine;
    Dialog dialog;
    Queue<Dialog> dialogQueue = new Queue<Dialog>();
    Queue<string> dialogLines = new Queue<string>();

    public void AssignDialogs(Dialog[] dialogs)
    {
        dialogQueue = new Queue<Dialog>(dialogs);
        container.gameObject.SetActive(true);
        GetNextDialog();
        StartNextDialog();
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogText.text = "";
        for (int i = 0; i < line.Length; i++)
        {
            yield return new WaitForSeconds(typeDelay);
            dialogText.text += line[i];
        }

        StopTyping();
    }

    void StopTyping()
    {
        isTyping = false;
        dialogText.text = currentLine;
        StopAllCoroutines();
    }

    void StartNextDialog()
    {
        currentLine = dialogLines.Dequeue();
        StartCoroutine(TypeText(currentLine));
    }

    void GetNextDialog()
    {
        dialog = dialogQueue.Dequeue();
        portrait.sprite = dialog.PortraitSprite;
        illustrationImage.sprite = dialog.Illustration;

        illustrationImage.gameObject.SetActive(dialog.Illustration);
        portrait.gameObject.SetActive(dialog.PortraitSprite);
        dialogLines = new Queue<string>(dialog.Lines);
    }

    IEnumerator NewDialog()
    {
        if (dialog.startCuscene)
            EventManager.Instance.onCutsceneStart.Invoke();

        GetNextDialog();
        container.gameObject.SetActive(false);

        yield return new WaitForSeconds(dialog.Delay);
        container.gameObject.SetActive(true);
        StartNextDialog();
    }

    void ManageCutscene()
    {
        if (Input.GetKeyDown(KeyCode.Return) && container.activeInHierarchy)
        {
            if (isTyping)
                StopTyping();
            else
            {
                SoundManager.Instance.PlayAudio("menu_select");
                if (dialogLines.Count <= 0)
                {
                    if (dialogQueue.Count > 0)
                        StartCoroutine(NewDialog());
                    else
                    {
                        EventManager.Instance.onCutsceneEnd.Invoke();
                        container.gameObject.SetActive(false);
                    }

                    return;
                }

                StartNextDialog();
            }
        }
    }

    private void Update()
    {
        ManageCutscene();
    }
}
