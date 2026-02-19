using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public SpriteRenderer spriteRenderer;

    private Animator anim; 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
                    Debug.Log("Hit.");
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
