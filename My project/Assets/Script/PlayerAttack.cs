using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 1;
    public SpriteRenderer spriteRenderer;
    public PlayerHealth playerHealth; 

    public int knockbackForce = 5;

    private Animator anim; 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerHealth != null && playerHealth.isAlive)
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        if (anim != null) 
        {
            anim.SetTrigger("Attack"); 
        }

        Vector2 attackDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach(Collider2D collider in hitColliders)
        {
            if(collider.CompareTag("Ennemy")) 
            {
                Vector2 directionToEnemy = (collider.transform.position - transform.position).normalized;
                
                if(Vector2.Dot(attackDirection, directionToEnemy) > 0)
                {
                    EnemyAI enemyScript = collider.GetComponent<EnemyAI>();
                    enemyScript.TakeDamage(attackDamage);

                    Vector2 knockback = (collider.transform.position - transform.position).normalized;
                    enemyScript.rb.AddForce(knockback * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
