using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Configuration")]
    public Transform target;
    public float speed = 120f;
    public float nextWpDistance = 1f;
    public float attackRange = 2f;

    public float detectionRange = 5f;
    public float attackCD = 2f;

    [Header("Composants")]
    public Seeker seeker;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Path path;
    private int currWp = 0;
    private float currentCD = 0f;

    public int damage = 1;

    public int maxHealth = 2;
    private int currentHealth;

    private bool isAlive = true;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (isAlive && seeker.IsDone() && Vector2.Distance(transform.position, target.position)<= detectionRange)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currWp = 0;
        }
    }

    void Update() 
    {

        if (!isAlive)
        {
            return;
        }
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);

        if (rb.linearVelocity.x != 0)
        {
            spriteRenderer.flipX = rb.linearVelocity.x < 0;
        }

        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (path == null || currWp >= path.vectorPath.Count || !isAlive)
        {
            return;
        }

        float playerDistance = Vector2.Distance(target.position, transform.position);

        if (playerDistance > attackRange)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currWp] - rb.position).normalized;
            Vector2 smoothDirection = Vector2.Lerp(rb.linearVelocity.normalized, direction, 0.1f);
            Vector2 velocity = smoothDirection * speed * Time.fixedDeltaTime;

            rb.linearVelocity = velocity;

            float distanceToWp = Vector2.Distance(rb.position, path.vectorPath[currWp]);
            if (distanceToWp < nextWpDistance)
            {
                currWp++;
            }
        } 
        else
        {
            rb.linearVelocity = Vector2.zero;

            if (currentCD <= 0)
            {
                Attack(); 
            }
        }
    }

    void Attack()
    {
        currentCD = attackCD;
        animator.SetTrigger("Attack");
        animator.SetBool("isAttacking", true);
    }

    void EndOfAttack()
    {
        animator.SetBool("isAttacking", false);

        if(Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            if (target != null) 
            {
                target.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }

        public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;

            if(currentHealth<= 0)
            {
                isAlive = false;
                animator.SetTrigger("Die");
                Destroy(gameObject, 3f);
            } else
            {
                animator.SetTrigger("Hit");
                currentCD = attackCD;
            }
        }
    }
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }
}