using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    public int maxHealth = 3;
    public int currentHealth;
    public bool isAlive = true;
    private bool isInvincible = false;
    [SerializeField] private float deathAnimDuration = 1.5f;

    [Header("References")]
    public Transform healthBarUI;
    public GameObject hpPrefab;
    public SpriteRenderer spriteRenderer;
    
    private Vector2 startPos;
    private Animator anim;

    void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        UpdateHealthbarUI();
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive || isInvincible) return;

        currentHealth -= damage;
        UpdateHealthbarUI();

        if (currentHealth <= 0)
        {
            StartCoroutine(DieRoutine());
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    IEnumerator DieRoutine()
    {
        isAlive = false;
        if (anim != null) anim.SetTrigger("Die");

        yield return new WaitForSeconds(deathAnimDuration);

        Respawn();
    }

    public void Respawn()
    {
        transform.position = startPos;
        currentHealth = maxHealth;
        isAlive = true;
        
        if (anim != null) 
        {
            anim.SetTrigger("Recover");
        }

        UpdateHealthbarUI();
    }

    public void UpdateHealthbarUI()
    {
        if (healthBarUI == null) return;
        foreach (Transform child in healthBarUI) Destroy(child.gameObject);
        for (int i = 0; i < currentHealth; i++) 
        {
            if(hpPrefab != null) Instantiate(hpPrefab, healthBarUI);
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        if(spriteRenderer != null) spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(1f);
        if(spriteRenderer != null) spriteRenderer.color = Color.white;
        isInvincible = false;
    }
}