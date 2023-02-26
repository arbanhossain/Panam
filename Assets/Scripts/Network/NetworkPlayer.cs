using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
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
}
