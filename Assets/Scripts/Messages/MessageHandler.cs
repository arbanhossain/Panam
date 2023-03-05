using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MessageHandler : MonoBehaviour
{
    public int MaxMessages = 25;

    public GameObject ChatPanel;
    public TMP_Text TextObject;

    public TMP_InputField InputField;

    [SerializeField]
    List<Message> MessageList = new List<Message>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputField.text != "") {
            if (Input.GetKeyDown(KeyCode.Return)) {
                SendMessageToChat(InputField.text);
                InputField.text = "";
            }
        } else {
            if (InputField.isFocused) {
                if (Input.GetKeyDown(KeyCode.Return)) {
                    InputField.text = "";
                }
            }
        }

        if (!InputField.isFocused) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                SendMessageToChat("Pressed Space");
            }
        }
        
    }

    public void SendMessageToChat(string Text) {
        if (MessageList.Count >= MaxMessages) {
            Destroy(MessageList[0]._Text.gameObject);
            MessageList.RemoveAt(0);
        }
        Message NewMessage = new Message();
        NewMessage.MessageText = Text;

        TMP_Text NewText = Instantiate(TextObject, ChatPanel.transform);
        NewMessage._Text = NewText;
        NewMessage._Text.text = Text;

        // NewText.text = NewMessage.MessageText;
        MessageList.Add(NewMessage);
    }
}

[System.Serializable]
public class Message
{
    public string MessageText;
    public TMP_Text _Text;
}
