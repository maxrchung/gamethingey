using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LobbyManager : NetworkLobbyManager {
    public RectTransform mainMenuPanel;
    public RectTransform lobbyPanel;
    public RectTransform connectionPanel;

    public List<GameObject> prefabs;

    public List<string> maps;
    private string currentMap;

    [HideInInspector]
    public RectTransform currentPanel;

    [HideInInspector]
    public bool isHost = false;

    void Start()
    {
        ChangeTo(mainMenuPanel);
        GetComponent<Canvas>().enabled = true;
        DontDestroyOnLoad(gameObject);

        Randomize();
    }

    void Update()
    {
        var players = Network.connections;
        for (int i = 0; i < players.Length; ++i)
        {
            Debug.Log("Found connection" + i + ": " + players[i].ipAddress);
        }
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
         base.OnServerSceneChanged(currentMap);

        if (sceneName.Equals(currentMap)) 
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

    public void Randomize() {
        playScene = maps[Random.Range(0,maps.Count)];
        int index = Random.Range(0, prefabs.Count);
        gamePlayerPrefab = prefabs[index];
        gamePlayerPrefab.GetComponent<Movement>().characterIndex = index;
    }

    public override void OnStartHost()
    {
        base.OnStartHost();
        ChangeTo(lobbyPanel);
    }
/*
    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId) {
             GameObject playerPrefab = (GameObject)Instantiate(prefabs[Random.Range(0, 2)]);
             NetworkServer.Spawn(playerPrefab);
             return playerPrefab;
    }
 */

}
