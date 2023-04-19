using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomSpawnPoint() {
        return new Vector3(Random.Range(358, 389), 4, Random.Range(370, 378));
    }

    public static void SetRenderLayerInChildren(Transform _Transform, int LayerNumber) {
        foreach(Transform T in _Transform.GetComponentInChildren<Transform>(true)) {
            T.gameObject.layer = LayerNumber;
        }
    }
}
