using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset = 0.2f;
    public Vector3 posOffset = new Vector3(0, 0, -10);
    private Vector3 velocity;

    void Update()
    {
        if (player != null) {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        }
    }
}