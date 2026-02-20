using UnityEngine;
using System.Collections; 

public class GameController : MonoBehaviour
{
    private Vector2 startPos;
    private PlayerHealth playerHealth;

    private void Start()
    {
        startPos = transform.position;
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                if (playerHealth.isAlive)
                {
                    Respawn();
                }
            }
        }
    }

    private void Update()
    {
        if(playerHealth != null && playerHealth.isAlive == false)
        {
            Debug.Log("Le joueur est mort, arrêt du GameController.");
    }
    }
    public void Respawn()
    {
        transform.position = startPos;
    }
}