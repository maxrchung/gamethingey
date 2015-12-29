using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LobbyManager : NetworkLobbyManager {
    public RectTransform mainMenuPanel;
    public RectTransform lobbyPanel;
    public RectTransform connectionPanel;

    [HideInInspector]
    public RectTransform currentPanel;

    [HideInInspector]
    public bool isHost = false;

    void Start()
    {
        ChangeTo(mainMenuPanel);
        GetComponent<Canvas>().enabled = true;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeTo(RectTransform newPanel)
    {
        if (currentPanel != null)
        {
            currentPanel.gameObject.SetActive(false);
        }

        if (newPanel != null)
        {
            newPanel.gameObject.SetActive(true);
        }

        currentPanel = newPanel;
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);

        if (sceneName.Equals("TestScene")) 
        {
            ChangeTo(connectionPanel);
        }
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        ChangeTo(connectionPanel);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        StopClient();
        ChangeTo(mainMenuPanel);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();
        ChangeTo(lobbyPanel);
    }

}
