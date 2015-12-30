using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrackHP : MonoBehaviour {
	GameObject mainPlayer;
	// Use this for initialization
	void Start () {
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{
			Debug.Log(player.GetComponent<Movement>().netId.ToString());
			if (player.GetComponent<Movement>().isLocalPlayer) {
				mainPlayer = player;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (mainPlayer == null)
		{
			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
			{
				if (player.GetComponent<Movement>().isLocalPlayer)
					mainPlayer = player;
			}
		}
		if (mainPlayer != null)
		{
			GetComponent<Slider> ().value = mainPlayer.GetComponent<Movement> ().health/mainPlayer.GetComponent<Movement> ().startingHealth;		
		}
	}
}
