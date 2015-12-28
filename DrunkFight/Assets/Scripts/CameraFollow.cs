using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraFollow : NetworkBehaviour {
    public float dampTime = 0.5f;
    public float maxLurch = 3.0f;
    public float minLurch = 2.0f;
    public float nextLurch = 1.0f;
    float timer;
    Vector3 velocity = Vector3.zero;
    Vector3 lurch = Vector2.zero;
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
                timer = nextLurch;
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
            Vector3 targetPos = player.transform.position;
            targetPos += new Vector3(0, 0, -10.0f);
            Vector3 cameraPos = camera.transform.position;
            camera.transform.position = Vector3.SmoothDamp(cameraPos, targetPos, ref velocity, dampTime);
            camera.transform.position += lurch;
            
        }
    }

    void SetLurch()
    {
        timer += Time.deltaTime;
        if (timer >= nextLurch)
        {
            
            timer = 0;
            Vector3 targetPos = player.transform.position;
            Vector3 cameraPos = camera.transform.position;
            Vector3 diff = targetPos - cameraPos;
            diff.z = 0;
            diff.Normalize();
            diff *= Random.Range(minLurch, maxLurch);
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(5.0f, 20.0f));
            lurch = rotation * (diff + new Vector3(0, 0, Random.Range(minLurch, maxLurch)));
            Debug.Log("lurch: " + lurch.ToString());
            
        }
    }
}
