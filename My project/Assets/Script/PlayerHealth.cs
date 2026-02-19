using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Transform healthBarUI;
    public GameObject hpPrefab;
    public Animator animator;
    public bool isAlive = true;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    void Start()
    {
        UpdateHealthbarUI();
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;
            UpdateHealthbarUI();

            if(currentHealth <= 0)
            {
                isAlive = false;
                if(animator != null) animator.SetTrigger("Die");
            }
        }
    }

    public void UpdateHealthbarUI()
    {
        if (healthBarUI == null) return;
        foreach (Transform child in healthBarUI)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(hpPrefab, healthBarUI);
        }
    }

    public void DisablePlayerVisual()
    {
        if(spriteRenderer != null) spriteRenderer.enabled = false;
    }
}