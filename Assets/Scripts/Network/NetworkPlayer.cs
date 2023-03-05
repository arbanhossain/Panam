using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public TextMeshProUGUI PlayerNicknameTM;

    [Networked
    (OnChanged = nameof(OnNicknameChanged))]
    public NetworkString<_16> Nickname { get; set; }

    public static NetworkPlayer Local { get; set; }

    public Transform PlayerModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Spawned() {
        if (Object.HasInputAuthority) {
            Local = this;

            Utils.SetRenderLayerInChildren(PlayerModel, LayerMask.NameToLayer("LocalPlayerModel"));

            Camera.main.gameObject.SetActive(false);

            RPC_SetNickname(PlayerPrefs.GetString("PlayerNickname"));

            Debug.Log($"Local Player Spawned");
        } else {
            Camera LocalCam = GetComponentInChildren<Camera>();
            LocalCam.enabled = false;

            AudioListener LocalAudioListener = GetComponentInChildren<AudioListener>();
            LocalAudioListener.enabled = false;

            Debug.Log($"Remote Player Spawned");
        }
    }

    public void PlayerLeft(PlayerRef player) {
        if (player == Object.InputAuthority) {
            Runner.Despawn(Object);
        }
    }

    static void OnNicknameChanged(Changed<NetworkPlayer> _Changed) {
        Debug.Log($"{Time.time} OnMPChanged Value {_Changed.Behaviour.Nickname}");

        _Changed.Behaviour.OnNicknameChanged();
    }

    private void OnNicknameChanged() {
        Debug.Log($"Nickname Changed for player {gameObject.name}: {Nickname}");

        PlayerNicknameTM.text = Nickname.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickname(string _Nickname, RpcInfo Info = default) {
        Debug.Log($"[RPC] SetNickname {_Nickname}");
        this.Nickname = _Nickname;
    }
}
