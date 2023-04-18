using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    bool PublicJoinedMessageSent = false;

    NetworkInGameMessages _NetworkInGameMessages;

    public TextMeshProUGUI PlayerNicknameTM;

    [SerializeField]
    Camera LeftCam;
    [SerializeField]
    Camera RightCam;
    [SerializeField]
    Camera CenterCam;


    [Networked
    (OnChanged = nameof(OnNicknameChanged))]
    public NetworkString<_16> Nickname { get; set; }

    public static NetworkPlayer Local { get; set; }

    public Transform PlayerModel;

    void Awake() {
        _NetworkInGameMessages = GetComponent<NetworkInGameMessages>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.platform);
    }

    public override void Spawned() {
        if (Object.HasInputAuthority) {
            Local = this;

            Utils.SetRenderLayerInChildren(PlayerModel, LayerMask.NameToLayer("LocalPlayerModel"));

            // if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor){
            //     FPSCam.gameObject.SetActive(false);
            // } else if (Application.platform == RuntimePlatform.Android) {
            //     OVRCam.gameObject.SetActive(false);
            // }



            // Camera.main.gameObject.SetActive(false); // For PC Build

            // For VR Build
            // LeftCam.gameObject.SetActive(false);
            // RightCam.gameObject.SetActive(false);
            // CenterCam.gameObject.SetActive(false);

            RPC_SetNickname(PlayerPrefs.GetString("PlayerNickname"));

            Debug.Log($"Local Player Spawned");
        } else {
            // var LocalCam;
            // if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor){
            //     Camera LocalCam = GetComponentInChildren<Camera>();
            //     LocalCam.enabled = false;
            // } else if (Application.platform == RuntimePlatform.Android) {
            //     OVRCameraRig LocalCam = GetComponentInChildren<OVRCameraRig>();
            //     LocalCam.enabled = false;
            // }

            // For PC Build
            // Camera LocalCam = GetComponentInChildren<Camera>();
            // LocalCam.enabled = false;

            // For VR Build
            LeftCam.enabled = false;
            RightCam.enabled = false;
            CenterCam.enabled = false;

            AudioListener LocalAudioListener = GetComponentInChildren<AudioListener>();
            LocalAudioListener.enabled = false;

            Debug.Log($"Remote Player Spawned");
        }
    }

    public void PlayerLeft(PlayerRef player) {
        if (Object.HasStateAuthority) {
            _NetworkInGameMessages.RPC_InGameMessage($"<color=green><b>{Nickname}</b> has left the game.</color>");
        }

        if (player == Object.InputAuthority) {
            Runner.Despawn(Object);
        }

        SceneManager.LoadScene(0); 
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

        if (!PublicJoinedMessageSent) {
            PublicJoinedMessageSent = true;
            _NetworkInGameMessages.RPC_InGameMessage($"<color=green><b>{Nickname}</b> has joined the game.</color>");
        }
    }
}
