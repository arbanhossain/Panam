using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fusion;
using Fusion.Sockets;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPlayer p_NetworkPlayerPrefab;

    public CharacterInputHandler _InputHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Following implementations are required by INetworkRunnerCallbacks and copied from the official Fusion documentation
    // hence the reason for inconsistent naming and bracket conventions

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
        if (runner.IsServer) {
            Debug.Log($"Player Joined. We are server. Spawning player");
            runner.Spawn(p_NetworkPlayerPrefab, Utils.GetRandomSpawnPoint(), Quaternion.identity, player);
        } else {
            Debug.Log($"Player Joined. We are client. Not spawning player");
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }

    public void OnInput(NetworkRunner runner, NetworkInput input) {
        if (_InputHandler == null && NetworkPlayer.Local != null) {
            _InputHandler = NetworkPlayer.Local.GetComponent<CharacterInputHandler>();
        }

        if (_InputHandler != null) {
            input.Set(_InputHandler.GetNetworkInput());
        }
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { Debug.Log($"Shut Down"); }

    public void OnConnectedToServer(NetworkRunner runner) { Debug.Log($"Connected to Server"); }

    public void OnDisconnectedFromServer(NetworkRunner runner) { Debug.Log($"Disconnected from Server"); }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { Debug.Log($"Connect Request"); }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { Debug.Log($"Connect Failed"); }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }
}
