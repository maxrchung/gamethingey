using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ItemDropSpawner : NetworkBehaviour {
    public GameObject itemDropPrefab;
    public Vector2[] spawnPoints;

    public override void OnStartServer()
    {
        for (int i = 0; i < spawnPoints.Length; ++i)
        {
            var pos = spawnPoints[i];
            var newItemDrop = (GameObject)Instantiate(itemDropPrefab,
                pos,
                Quaternion.identity);
            NetworkServer.Spawn(newItemDrop);
        }
    }
}
