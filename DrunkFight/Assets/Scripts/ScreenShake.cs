using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
    float amount;
    float duration;
    Vector3 originalPos;
    Camera camera;
    float timer = 0.0f;

    // To use ScreenShake, call ScreenShake's Shake from anywhere
    // and specify a shakeAmount and shakeDuration
    // Defaults have been given if you want an idea of 
    public void Shake(float shakeAmount = 0.5f, float shakeDuration = 1.0f)
    {
        amount = shakeAmount;
        duration = shakeDuration;
        camera = GetComponent<Camera>();
        originalPos = camera.transform.position;
    }

    IEnumerator ShakeCoroutine()
    {
        while (timer <= duration)
        {
            timer += Time.deltaTime;

            int degrees = Random.Range(0, 360);
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, degrees);
            Vector3 direction = new Vector3(Random.Range(0.1f, amount), 0.0f, 0.0f);

            camera.transform.position = originalPos + direction;
            yield return null;
        }
        timer = 0;

        // Z-axis shouldn't change
        originalPos.z = camera.transform.position.z;
        camera.transform.position = originalPos;
    }
}
