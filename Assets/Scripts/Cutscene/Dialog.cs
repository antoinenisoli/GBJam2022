using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Dialog), menuName = "Dialogs/" + nameof(Dialog))]
public class Dialog : ScriptableObject
{
    public Sprite PortraitSprite;
    public float Delay;
    [TextArea] public string[] Lines;
}
