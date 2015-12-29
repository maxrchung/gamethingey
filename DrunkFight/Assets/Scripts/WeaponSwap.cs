using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSwap : MonoBehaviour {
	GameObject mainPlayer;
	public Sprite vomit;
	public Sprite fire;
	public Sprite poop;
	public Sprite fist;

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
		Sprite currentWep= fist;
		int weapon = mainPlayer.GetComponent<WeaponScript> ().currentWeapon;
		if (weapon == 0) {
			currentWep = fist;
		} 
		else if (weapon == 1) {
			currentWep = vomit;
		}
		else if (weapon == 2) {
			currentWep = poop;
		}
		else if (weapon == 3) {
			currentWep = fire;
		}
		GetComponent<Image> ().sprite = currentWep;
	}
}
