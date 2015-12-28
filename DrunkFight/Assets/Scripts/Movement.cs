using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Movement : NetworkBehaviour
{
    private Dictionary<string, Acceleration> accelerations;
    //public float Speed = 0f;
    //private float movex = 0f;
    //private float movey = 0f;

    // Use this for initialization
    void Start()
    {
        accelerations = new Dictionary<string, Acceleration>();
        accelerations.Add("Movement", new Acceleration(null, null, 10));
        accelerations.Add("Friction", new Acceleration(0.0f, 0.0f, 20));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        //GetComponent<Rigidbody2D>().velocity = new Vector2 (movex * Speed, movey * Speed);

        //var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePosition = Input.mousePosition;
        mousePosition.z = -10.0f;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 180);
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        // Handle movement
        accelerations["Movement"].x = null;
        accelerations["Movement"].y = null;
        float moveSpeed = 3.5f;

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            moveSpeed = moveSpeed / Mathf.Sqrt(2.0f);

        if (Input.GetKey(KeyCode.A))
        {
            accelerations["Movement"].x = -moveSpeed;
            accelerations["Friction"].x = null;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            accelerations["Movement"].x = moveSpeed;
            accelerations["Friction"].x = null;
        }
        else
        {
            accelerations["Movement"].x = null;
            accelerations["Friction"].x = 0.0f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            accelerations["Movement"].y = moveSpeed;
            accelerations["Friction"].y = null;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            accelerations["Movement"].y = -moveSpeed;
            accelerations["Friction"].y = null;
        }
        else
        {
            accelerations["Movement"].y = null;
            accelerations["Friction"].y = 0.0f;
        }

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
            Debug.Log(new Vector3(targetX, targetY));
        }
    }
}

public class Acceleration
{
    public float? x;
    public float? y;
    public float accel;
    public int currFrame;

    public Acceleration(float? x, float? y, float accel)
    {
        this.x = x;
        this.y = y;
        this.accel = accel;
        this.currFrame = 0;
    }
}
