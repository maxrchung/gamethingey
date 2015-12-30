using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class LobbyPlayerScript : NetworkLobbyPlayer {

	
	[SyncVar]
	public GameObject player;

	public GameObject lobbyPanel;
	public List<GameObject> playerPrefabs;

	void Start () {
		foreach(GameObject lol in GameObject.FindGameObjectsWithTag("LobbyPanel")) {
			lobbyPanel = lol;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isLocalPlayer) {
			if(lobbyPanel == null) {
				foreach(GameObject lol in GameObject.FindGameObjectsWithTag("LobbyPanel")) {
					lobbyPanel = lol;
				}
			}
			else {
				player = playerPrefabs[(int) lobbyPanel.GetComponent<LobbyPanel>().slider.value];
			}
		}
	}
}
