using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
    float amount;
    float duration;
    Vector3 originalPos;
    Camera cam;
    float timer = 0.0f;

    // To use ScreenShake, call ScreenShake's Shake from anywhere
    // and specify a shakeAmount and shakeDuration
    // Defaults have been given if you want an idea of 
    public void Shake(float shakeAmount = 0.5f, float shakeDuration = 1.0f)
    {
        amount = shakeAmount;
        duration = shakeDuration;
        cam = GetComponent<Camera>();
        originalPos = cam.transform.position;
    }

    IEnumerator ShakeCoroutine()
    {
        while (timer <= duration)
        {
            timer += Time.deltaTime;

            int degrees = Random.Range(0, 360);
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, degrees);
            Vector3 direction = rotation * new Vector3(Random.Range(0.1f, amount), 0.0f, 0.0f);

            cam.transform.position = originalPos + direction;
            yield return null;
        }
        timer = 0;

        // Z-axis shouldn't change
        originalPos.z = cam.transform.position.z;
        cam.transform.position = originalPos;
    }
}
