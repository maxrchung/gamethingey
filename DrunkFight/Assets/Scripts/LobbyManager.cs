using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LobbyManager : MonoBehaviour {
    NetworkLobbyManager nlm;

	// Use this for initialization
	void Start () {
        nlm = GetComponent<NetworkLobbyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!NetworkClient.active && !NetworkServer.active)
        {

        }
	}

    public void CreateHost()
    {
        nlm.StartHost();
    }

    public void DestroyHost()
    {

    }
}
