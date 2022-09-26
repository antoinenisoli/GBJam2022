using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CutsceneUI : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] Text dialogText;
    [SerializeField] Image portrait;
    [SerializeField] float typeDelay = 0.2f;
    bool isTyping;
    string currentLine;
    Dialog dialog;
    Queue<Dialog> dialogQueue = new Queue<Dialog>();
    Queue<string> dialogLines = new Queue<string>();

    public void PlayDialog(Dialog[] dialogs)
    {
        dialogQueue = new Queue<Dialog>(dialogs);
        dialog = dialogQueue.Dequeue();

        container.gameObject.SetActive(true);
        portrait.sprite = dialog.PortraitSprite;
        dialogLines = new Queue<string>(dialog.Lines);
        StartDialog();
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

    void StartDialog()
    {
        currentLine = dialogLines.Dequeue();
        StartCoroutine(TypeText(currentLine));
    }

    IEnumerator NewDialog()
    {
        dialog = dialogQueue.Dequeue();
        portrait.sprite = dialog.PortraitSprite;
        dialogLines = new Queue<string>(dialog.Lines);
        container.gameObject.SetActive(false);

        yield return new WaitForSeconds(dialog.Delay);
        container.gameObject.SetActive(true);
        StartDialog();
    }

    void ManageCutscene()
    {
        if (Input.GetKeyDown(KeyCode.Return) && container.activeInHierarchy)
        {
            if (isTyping)
                StopTyping();
            else
            {
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

                StartDialog();
            }
        }
    }

    private void Update()
    {
        ManageCutscene();
    }
}
