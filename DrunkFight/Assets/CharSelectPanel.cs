using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CharSelectPanel : NetworkBehaviour {
    public LobbyManager lobbyManager;
    public RectTransform lobbyPanel;
    public GameObject startButton;
    public Slider playerSelection;
    public GameObject hostCover;
    public List<GameObject> playerPrefabs;
    public int index;
    public GameObject prefab;

    public void OnClickReady() {
    	Debug.Log("PLAYER SELECTED: " + playerSelection.value);
    	index = (int) playerSelection.value;
    	prefab = playerPrefabs[(int) playerSelection.value];
    	Debug.Log(prefab);
    	if(lobbyManager.isHost) {
    		lobbyManager.StartHost();
        	startButton.SetActive(true);
        	hostCover.SetActive(false);
    	}
    	else {
            lobbyManager.StartClient();
            startButton.SetActive(false);
            hostCover.SetActive(true);
    	}
    }

    
}
