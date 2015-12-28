using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraFollow : NetworkBehaviour {
    public float dampTime = 0.5f;
    public float maxLurchDistance = 3;
    public float minLurchDistance = 2;
    public float nextLurchLimit = 2;
    float nextLurchTime;
    float timer;
    Vector2 velocity = Vector3.zero;
    Vector2 lurch = Vector2.zero;
    GameObject player;
    Camera camera;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            Movement nb = p.GetComponent<Movement>();
            if (nb.isLocalPlayer)
            {
                player = p;
                camera = GetComponent<Camera>();
                nextLurchTime = nextLurchLimit;
                timer = nextLurchTime;
                SetLurch();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            SetLurch();
            Vector2 targetPos = player.transform.position;
            targetPos += lurch;
            Vector2 cameraPos = camera.transform.position;
            Vector2 damp = Vector2.SmoothDamp(cameraPos, targetPos, ref velocity, dampTime);
            camera.transform.position = new Vector3(damp.x, damp.y, -10);
        }
    }

    void SetLurch()
    {
        timer += Time.deltaTime;
        Debug.Log("Lurch: " + lurch.ToString());
        Debug.Log("timer: " + timer.ToString());
        Debug.Log("nextLurchTime: " + nextLurchTime.ToString());
        if (timer >= nextLurchTime ||
            camera.transform.position == player.transform.position + (Vector3)lurch)
        {
            timer = 0;
            nextLurchTime = Random.Range(0.5f * nextLurchLimit, nextLurchLimit);
            float degrees = Random.Range(0, 360);
            Quaternion rotation = Quaternion.Euler(0, 0, degrees);
            lurch = rotation * new Vector3(Random.Range(minLurchDistance, maxLurchDistance), 0, 0);
            Debug.Log("Lurch: " + lurch.ToString());
        }
    }
}
