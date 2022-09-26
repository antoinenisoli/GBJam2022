using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Dialog), menuName = "Dialogs/" + nameof(Dialog))]
public class Dialog : ScriptableObject
{
    public Sprite PortraitSprite, Illustration;
    public float Delay;
    public bool startCuscene;
    [TextArea] public string[] Lines;
}
