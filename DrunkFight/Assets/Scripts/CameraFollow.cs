using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraFollow : NetworkBehaviour {
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
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
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            //Vector2 targetPos = player.transform.position;
            //Vector2 cameraPos = camera.transform.position;
            //Vector2 diff = targetPos - cameraPos;
            //camera.transform.position = Vector2.SmoothDamp(targetPos, cameraPos, dampTime)

            Vector3 moveBack = new Vector3(0, 0, -10);
            camera.transform.position = player.transform.position + moveBack;

            //Vector3 point = camera.WorldToViewportPoint(transform.position);
            //Vector3 delta = transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            //Vector3 destination = transform.position + delta;
            //camera.transform.position = Vector3.SmoothDamp(camera.transform.position, destination, ref velocity, dampTime);
        }
    }
}
