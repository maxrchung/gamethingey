using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Movement : NetworkBehaviour
{
    public float moveSpeed = 3.5f;
    public float turnSpeed = 7.0f;
    public int moveAccel = 10;
    public int frictionAccel = 20;
    public int sidewaysMoveThreshold = 100;
    public int backwardsMoveThreshold = 145;
    public float sidewaysMoveFraction = 0.7f;
    public float backwardsMoveFraction = 0.3f;

    private Dictionary<string, Acceleration> accelerations;

    // Use this for initialization
    void Start()
    {
        accelerations = new Dictionary<string, Acceleration>();
        accelerations.Add("Movement", new Acceleration(null, null, moveAccel));
        accelerations.Add("Friction", new Acceleration(0.0f, 0.0f, frictionAccel));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // Rotate player towards mouse
        if (Camera.main != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion lookDirection = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDirection, turnSpeed);
        }

        // Handle movement
        float currMoveSpeed = moveSpeed;
        accelerations["Movement"].x = null;
        accelerations["Movement"].y = null;

        // Scale player's movement speed based on angular difference between player's forward direction and movement direction
        // Effectively walks faster in the facing direction
        Vector3 axis;
        float anglediff;
        Quaternion.FromToRotation(transform.up, GetComponent<Rigidbody2D>().velocity).ToAngleAxis(out anglediff, out axis);
        if (anglediff > sidewaysMoveThreshold)
            currMoveSpeed = currMoveSpeed * sidewaysMoveFraction;
        else if (anglediff > backwardsMoveThreshold)
            currMoveSpeed = currMoveSpeed * backwardsMoveFraction;

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            currMoveSpeed = currMoveSpeed / Mathf.Sqrt(2.0f);

        if (Input.GetKey(KeyCode.A))
        {
            accelerations["Movement"].x = -currMoveSpeed;
            accelerations["Friction"].x = null;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            accelerations["Movement"].x = currMoveSpeed;
            accelerations["Friction"].x = null;
        }
        else
        {
            accelerations["Movement"].x = null;
            accelerations["Friction"].x = 0.0f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            accelerations["Movement"].y = currMoveSpeed;
            accelerations["Friction"].y = null;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            accelerations["Movement"].y = -currMoveSpeed;
            accelerations["Friction"].y = null;
        }
        else
        {
            accelerations["Movement"].y = null;
            accelerations["Friction"].y = 0.0f;
        }

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

        // Set Animator flag
        if (Vector2.zero.Equals(GetComponent<Rigidbody2D>().velocity))
            GetComponent<Animator>().SetBool("Moving", false);
        else
            GetComponent<Animator>().SetBool("Moving", true);
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
