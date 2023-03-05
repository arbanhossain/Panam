using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fusion;

public class NetworkInGameMessages : NetworkBehaviour
{
    MessageHandler Handler;
    // Start is called before the first frame update
    void Awake()
    {
        Handler = GameObject.Find("MessageHandler").GetComponent<MessageHandler>();
        Handler._NetworkInGameMessages = this;
    }

    public void SendInGameMessages(string Nickname, string Message) {
        RPC_InGameMessage($"<color=blue><b>{Nickname}</b></color>: {Message}");
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_InGameMessage(string _Message, RpcInfo Info = default) {
        Debug.Log($"[RPC] InGameMessage {_Message}");

        Handler.ReceiveMessageFromRpc(_Message, Message.Type.Chat);
    }
}
