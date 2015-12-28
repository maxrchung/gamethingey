using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Movement : NetworkBehaviour {
    private Dictionary<string, Acceleration> accelerations;
	//public float Speed = 0f;
	//private float movex = 0f;
	//private float movey = 0f;

    // Use this for initialization
    void Start()
    {
        accelerations = new Dictionary<string, Acceleration>();
        accelerations.Add("Movement", new Acceleration(0.0f, 0.0f, 5));
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (!isLocalPlayer)
        {
            return;
        }
		//GetComponent<Rigidbody2D>().velocity = new Vector2 (movex * Speed, movey * Speed);

        var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition,Vector3.forward);
		
		transform.rotation = rot;
		transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z);
		GetComponent<Rigidbody2D>().angularVelocity = 0;

        // Handle movement
        accelerations["Movement"].x = null;
        accelerations["Movement"].y = null;

        if (Input.GetKey(KeyCode.A))
            accelerations["Movement"].x = -2.0f;
        else if (Input.GetKey(KeyCode.D))
            accelerations["Movement"].x = 2.0f;
        else
            accelerations["Movement"].x = 0.0f;

        if (Input.GetKey(KeyCode.W))
            accelerations["Movement"].y = 2.0f;
        else if (Input.GetKey(KeyCode.S))
            accelerations["Movement"].y = -2.0f;
        else
            accelerations["Movement"].y = 0.0f;

        // Set Animator flag
        if (accelerations["Movement"].y == 0.0f && accelerations["Movement"].x == 0.0f)
            GetComponent<Animator>().SetBool("Moving", false);
        else
            GetComponent<Animator>().SetBool("Moving", true);

        // Apply all accelerations
        foreach (Acceleration accel in accelerations.Values)
        {
            float targetX = GetComponent<Rigidbody2D>().velocity.x;
            float targetY = GetComponent<Rigidbody2D>().velocity.y;

            // Let null values not affect the player's velocity
            if (accel.x.HasValue)
                targetX = accel.x.Value;
            if (accel.y.HasValue)
                targetY = accel.y.Value;

            // Apply linear interpolation to player velocity
            GetComponent<Rigidbody2D>().velocity = Vector3.MoveTowards(GetComponent<Rigidbody2D>().velocity, new Vector3(targetX, targetY), Time.fixedDeltaTime * accel.accel);
        }
	}
}

public class Acceleration
{
    public float? x;
    public float? y;
    public float accel;
    public int currFrame;

    public Acceleration (float? x, float? y, float accel)
    {
        this.x = x;
        this.y = y;
        this.accel = accel;
        this.currFrame = 0;
    }
}
