using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        Light2D[] allLights = GameObject.FindObjectsByType<Light2D>(FindObjectsSortMode.None);
        foreach (Light2D l in allLights)
        {
            if (l.lightType == Light2D.LightType.Global)
            {
                l.intensity = 0f;
            }
        }
    }
}