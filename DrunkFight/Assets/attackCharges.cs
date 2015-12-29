using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class attackCharges : MonoBehaviour {
	GameObject mainPlayer;
	// Use this for initialization
	void Start () {
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{
			if (player.GetComponent<Movement>().isLocalPlayer)
				mainPlayer = player;
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*int charges = mainPlayer.GetComponent<WeaponScript> ().charges;
		if (charges == 0)
			charges = 999;
		string s = "x"+charges;
		GetComponent<Text> ().text = s;
		*/
	}
}
