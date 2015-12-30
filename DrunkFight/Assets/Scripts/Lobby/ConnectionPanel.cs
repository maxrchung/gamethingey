using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ConnectionPanel : MonoBehaviour {
    public LobbyManager lobbyManager;
    public RectTransform mainPanel;

    public void OnClickQuitGame()
    {
        Debug.Log("Quitting game!");
        lobbyManager.ChangeTo(mainPanel);
        lobbyManager.Randomize();
        lobbyManager.StopHost();
        lobbyManager.StopClient();
        lobbyManager.isHost = false;
    }
}
