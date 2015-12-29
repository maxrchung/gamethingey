using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LobbyPanel : MonoBehaviour {
    public LobbyManager lobbyManager;
    public RectTransform connectionPanel;
    public RectTransform mainPanel;
    public Text playersText;

    // Update is called once per frame
    void Update()
    {
        if (playersText != null && GameObject.FindGameObjectsWithTag("LobbyPlayer") != null)
        {
            playersText.text = "Players: " + GameObject.FindGameObjectsWithTag("LobbyPlayer").Length;
        }
    }

    public void OnClickStartGame()
    {
        if (lobbyManager.isHost)
        {
            lobbyManager.ServerChangeScene(lobbyManager.playScene);
        }
    }

    public void OnClickQuitLobby()
    {
        lobbyManager.ChangeTo(mainPanel);
        lobbyManager.StopHost();
        lobbyManager.StopClient();
        lobbyManager.isHost = false;
    }
}
