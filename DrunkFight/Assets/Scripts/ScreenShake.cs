using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
    float amount;
    float duration;
    Vector3 originalPos;
    Camera cam;
    float timer = 0.0f;
    bool isRunning = false;
    GameObject mainPlayer;

    // To use ScreenShake, call ScreenShake's Shake from anywhere
    // and specify a shakeAmount and shakeDuration
    // Defaults have been given if you want an idea of 
    public void Shake(float shakeAmount = 0.3f, float shakeDuration = 1.0f)
    {
        if (mainPlayer == null)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponent<Movement>().isLocalPlayer)
                    mainPlayer = player;
            }
        }
        if (mainPlayer != null)
        {
            amount = shakeAmount;
            duration = shakeDuration;
            cam = GetComponent<Camera>();

            // If currently shaking, restart duration
            // else start a new shake
            if (isRunning)
            {
                timer = duration - shakeDuration;
            }
            else
            {
                StartCoroutine("ShakeCoroutine");
            }
        }
    }

    IEnumerator ShakeCoroutine()
    {
        isRunning = true;
        while (timer <= duration)
        {
            timer += Time.deltaTime;
            cam.transform.position += new Vector3(Random.Range(-amount, amount),
                                                  Random.Range(-amount, amount),
                                                  Random.Range(-amount, amount));
            yield return null;
        }
        timer = 0;
        isRunning = false;
    }
}
