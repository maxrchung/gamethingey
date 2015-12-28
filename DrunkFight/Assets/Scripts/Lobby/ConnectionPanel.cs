using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ConnectionPanel : MonoBehaviour {
    public LobbyManager lobbyManager;
    public RectTransform mainPanel;

    public void OnClickQuitGame()
    {
        lobbyManager.ChangeTo(mainPanel);
        NetworkServer.Shutdown();
    }
}
