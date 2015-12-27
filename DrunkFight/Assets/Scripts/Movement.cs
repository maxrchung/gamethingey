using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float Speed = 0f;
	private float movex = 0f;
	private float movey = 0f;
	// Use this for initialization
	void Update () {

		if (Input.GetKey (KeyCode.A))
			movex = -1;
		else if (Input.GetKey (KeyCode.D))
			movex = 1;
		else
			movex = 0;
		if (Input.GetKey (KeyCode.W))
			movey = 1;
		else if (Input.GetKey (KeyCode.S))
			movey = -1;
		else
			movey = 0;
	}
	// Update is called once per frame
	void FixedUpdate () {
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition,Vector3.forward);
		
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z);
		GetComponent<Rigidbody2D>().angularVelocity = 0;

		GetComponent<Rigidbody2D>().velocity = new Vector2 (movex * Speed, movey * Speed);
	}
}
