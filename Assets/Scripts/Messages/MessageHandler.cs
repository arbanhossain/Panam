using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MessageHandler : MonoBehaviour
{
    [HideInInspector]
    public NetworkInGameMessages _NetworkInGameMessages;

    public int MaxMessages = 25;

    public GameObject ChatPanel;
    public TMP_Text TextObject;

    public TMP_InputField InputField;

    public Color ChatColor, SystemColor;

    [SerializeField]
    List<Message> MessageList = new List<Message>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (InputField.text != "" && InputField.isFocused) {
        //     if (Input.GetKeyDown(KeyCode.Return)) {
        //         SendMessageToChat(InputField.text);
        //         InputField.text = "";
        //     }
        // }

        if (Input.GetKeyDown(KeyCode.Return) && !InputField.isFocused) {
            InputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && InputField.isFocused) {
            InputField.DeactivateInputField();

        }
        
    }

    void OnEnable() {
        InputField.onEndEdit.AddListener(delegate {
            _NetworkInGameMessages.SendInGameMessages(NetworkPlayer.Local.Nickname.ToString(), InputField.text);
            InputField.text = "";
        });
    }

    public void ReceiveMessageFromRpc(string Text, Message.Type MessageType) {
        SendMessageToChat(Text, MessageType);
    }

    public void SendMessageToChat(string Text, Message.Type MessageType) {
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
