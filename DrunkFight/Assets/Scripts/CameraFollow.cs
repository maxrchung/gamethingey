using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraFollow : NetworkBehaviour {
    public float dampTime = 0.5f;
    public float rotationDegrees = 2.0f;
    int reverse = 1;
    Vector3 velocity = Vector3.zero;
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
            Vector3 targetPos = player.transform.position;
            targetPos += new Vector3(0, 0, -10.0f);
            Vector3 cameraPos = camera.transform.position;
            camera.transform.position = Vector3.SmoothDamp(cameraPos, targetPos, ref velocity, dampTime);
            if (Mathf.Abs(camera.transform.rotation.eulerAngles.z) >= 5 &&
                Mathf.Abs(camera.transform.rotation.eulerAngles.z) <= 355)
            {
                reverse *= -1;
            }
            camera.transform.Rotate(0, 0, rotationDegrees * reverse * Time.deltaTime);
        }
    }
}
