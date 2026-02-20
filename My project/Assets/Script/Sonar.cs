using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class Sonar : MonoBehaviour 
{
    public Light2D playerLight;
    public float normalRadius = 3f;
    public float maxRevealRadius = 15f;
    public float propagationSpeed = 30f;
    private bool isRevealing = false;

    void Update()
    {
        if (playerLight == null) return; 

        if (Input.GetKeyDown(KeyCode.Space) && !isRevealing)
        {
            StartCoroutine(ShockwavePulse());
        }
    }

    IEnumerator ShockwavePulse()
    {
        isRevealing = true;

        float currentRadius = normalRadius;
        while (currentRadius < maxRevealRadius)
        {
            currentRadius += propagationSpeed * Time.deltaTime;
            playerLight.pointLightOuterRadius = currentRadius;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);

        while (currentRadius > normalRadius)
        {
            currentRadius -= propagationSpeed * 0.5f * Time.deltaTime;
            playerLight.pointLightOuterRadius = Mathf.Max(currentRadius, normalRadius);
            yield return null;
        }

        yield return new WaitForSeconds(5f); 
        isRevealing = false;
    }
}