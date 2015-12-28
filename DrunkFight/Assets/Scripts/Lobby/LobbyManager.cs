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

    public override void OnStartHost()
    {
        base.OnStartHost();
        ChangeTo(lobbyPanel);
    }

}
