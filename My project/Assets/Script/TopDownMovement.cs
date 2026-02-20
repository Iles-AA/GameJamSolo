using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private Animator animator;

    public SpriteRenderer spriteRenderer;
    public PlayerHealth playerHealth;

    public Rigidbody2D physics;
    private Vector2 direction;

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
        if (playerHealth == null) playerHealth = GetComponent<PlayerHealth>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    private void OnEnable() 
    {
        if (moveAction != null && moveAction.action != null) 
            moveAction.action.Enable();
    }

    private void OnDisable() 
    {
        if (moveAction != null && moveAction.action != null) 
            moveAction.action.Disable();
    }

    private void Update()
    {
        if (playerHealth != null && playerHealth.isAlive)
        {
            float magnitude = moveAction.action.ReadValue<Vector2>().sqrMagnitude;
            direction = moveAction.action.ReadValue<Vector2>();

            if (animator != null) animator.SetFloat("Speed", magnitude);

            if (spriteRenderer != null)
            {
                if (direction.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (direction.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
        else 
        {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        physics.MovePosition(physics.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);    
    }
}