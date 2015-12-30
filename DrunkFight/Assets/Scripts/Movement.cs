using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Movement : NetworkBehaviour
{
    [SyncVar]
    public GameObject prefab;

    public List<GameObject> prefabs;

    public float moveSpeed = 3.5f;
    public float turnSpeed = 7.0f;
    public int moveAccel = 10;
    public int frictionAccel = 20;
    public int sidewaysMoveThreshold = 100;
    public int backwardsMoveThreshold = 145;
    public float sidewaysMoveFraction = 0.7f;
    public float backwardsMoveFraction = 0.3f;
    private List<Vector3> spawnLocations = new List<Vector3>();
    public float respawnTimer = 3.0f;

    private float slowCounter;
    private float slow;

    [SyncVar]
    public float startingHealth = 100.0f;
    public float health;
	[SyncVar]
	public int numDrinks=5;

    private Dictionary<string, Acceleration> accelerations;

    [HideInInspector]
    public string playerId;

    [HideInInspector]
    [SyncVar]
    public bool isDead = false;

    private GameObject playerStartPositions;
    
    // Use this for initialization
    void Start()
    {
        health = startingHealth;
        accelerations = new Dictionary<string, Acceleration>();
        accelerations.Add("Movement", new Acceleration(null, null, moveAccel));
        accelerations.Add("Friction", new Acceleration(0.0f, 0.0f, frictionAccel));
        slowCounter = 0.0f;
        playerId = netId.ToString();
        Debug.Log(playerId);

        playerStartPositions = GameObject.FindGameObjectWithTag("PlayerStartPositions");
        if (playerStartPositions != null)
        {
             foreach (Transform child in playerStartPositions.transform)
             {
                  spawnLocations.Add(child.position);
             }
        }
    }

	bool winner()
	{
		if (isDead)
			return false;
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			if (player.GetComponent<Movement> ().playerId == playerId) {
				if (!player.GetComponent<Movement> ().isDead) {
					return false;
				}
			}
		}

		return true;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
          if (playerStartPositions == null)
          {
               playerStartPositions = GameObject.FindGameObjectWithTag("PlayerStartPositions");
               if (playerStartPositions != null)
               {
                    foreach (Transform child in playerStartPositions.transform)
                    {
                         spawnLocations.Add(child.position);
                    }
               }
          }
		if(winner())
		{
			Debug.Log ("WWFOIEIJOWEIO");
		}
        if (isDead)
        {
            return;
        }

        if (!isLocalPlayer)
        {
            return;
        }

        if(isClient) {
            //Debug.Log(playerId + " " + health);
        }

        // Rotate player towards mouse
        if (Camera.main != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion lookDirection = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, lookDirection, turnSpeed);
            transform.rotation = lookDirection;
        }

        // Handle movement
        float currMoveSpeed = moveSpeed;
        if (slowCounter > 0.0f)
            currMoveSpeed = slow * moveSpeed;
        slowCounter -= Time.fixedDeltaTime;

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

    public void ApplyHit (string hitOrigin, float damage, Vector3 knockback, float slow, float slowDuration)
    {
        if (isLocalPlayer && isClient)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>().Shake();
        }
        //Debug.Log("Player " + this.playerId + " was hit by an attack from Player " + hitOrigin);
        if(isServer) {
            health -= damage;

            if (health <= 0)
            {
				int donated = numDrinks / 2 + 1;
				GameObject killer;
				GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
				foreach (GameObject p in players) {
					if (p.GetComponent<Movement> ().playerId == hitOrigin) {
						p.GetComponent<Movement> ().numDrinks += donated;
						break;
					}
				}
				if (donated >= numDrinks) {
					//Lose Here
					GameObject.FindGameObjectWithTag("MainPanel").GetComponent<ConnectionPanel>().OnClickQuitGame();
					Debug.Log("Death");
				} else {
					numDrinks -= donated;
				}
                health = startingHealth;
                //Debug.Log("Respawning");
                RpcRespawn();
				Debug.Log (numDrinks);
            }
        }
        Debug.Log("Health: " + health);

        // Apply knockback
        GetComponent<Rigidbody2D>().velocity = knockback;

        // Apply slow
        this.slow = slow;
        this.slowCounter = slowDuration;

        //Debug.Log("Hit for " + damage + " damage");
        //Debug.Log("Knocked back for " + knockback);
        //Debug.Log("Slowed by " + slow);
    }

    [Command]
    void CmdRespawn() {
        RpcRespawn();
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)];
        }
    }

    IEnumerator RespawnPlayer()
    {
        isDead = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(respawnTimer);
        transform.position = spawnLocations[Random.Range(0, spawnLocations.Count)];
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        isDead = false;
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
