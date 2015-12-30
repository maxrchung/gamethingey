using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraFollow : NetworkBehaviour {
    public float dampTime = 0.5f;
    public float rotationDegrees = 2.0f;
    public float lurchAmount = 0.03f;
    public float lurchTimerMax = 2.0f;
    float lurchTimer = 0.0f;
    Vector3 lurch;
    int lurchReverse = 1;
    int rockingReverse = 1;
    public Vector3 velocity = Vector3.zero;
    GameObject player;
    Camera cam;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            Movement nb = p.GetComponent<Movement>();
            if (nb.isLocalPlayer)
            {
                player = p;
                cam = GetComponent<Camera>();
                cam.transform.position = player.transform.position;
                cam.transform.position += new Vector3(0, 0, -10.0f);
                lurchTimer = lurchTimerMax;
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
            Vector3 cameraPos = cam.transform.position;
            cam.transform.position = Vector3.SmoothDamp(cameraPos, targetPos, ref velocity, dampTime);
            if (Mathf.Abs(cam.transform.rotation.eulerAngles.z) >= 5 &&
                Mathf.Abs(cam.transform.rotation.eulerAngles.z) <= 355)
            {
                rockingReverse *= -1;
            }
            cam.transform.Rotate(0, 0, rotationDegrees * rockingReverse * Time.deltaTime);
            cam.transform.position += lurch * Time.deltaTime;
        }
    }

    void SetLurch()
    {
        lurchTimer += Time.deltaTime;
        if (lurchTimer >= lurchTimerMax)
        {
            lurchTimer = 0.0f;
            lurch = new Vector3(0, 0, lurchReverse * lurchAmount);
            lurchReverse *= -1;
        }
    }
}
