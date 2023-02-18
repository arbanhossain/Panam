using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

using Fusion;
using Fusion.Sockets;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner p_NetRunnerPrefab;

    NetworkRunner _NetRunner;

    // Start is called before the first frame update
    void Start()
    {
        _NetRunner = Instantiate(p_NetRunnerPrefab);
        _NetRunner.name = "Network Runner";

        var _ClientTask = InitializeNetworkRunner(_NetRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);

        Debug.Log($"Server Network Runner Initialized");
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner _Runner, GameMode _Mode, NetAddress _Address, SceneRef _Scene, Action<NetworkRunner> _Initialized)
    {
        var _SceneManager = _Runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();    

        if (_SceneManager == null) {
            _SceneManager = _Runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        _Runner.ProvideInput = true;

        return _Runner.StartGame(new StartGameArgs {
            GameMode = _Mode,
            Address = _Address,
            Scene = _Scene,
            SessionName = "Test Session",
            Initialized = _Initialized,
            SceneManager = _SceneManager
        });
    } 

}
