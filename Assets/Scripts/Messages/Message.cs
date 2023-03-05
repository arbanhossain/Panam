using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

[System.Serializable]
public class Message
{
    public string MessageText;
    public TMP_Text _Text;
    public Type MessageType;

    public enum Type {
        Chat,
        System
    }
}