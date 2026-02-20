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

    private Rigidbody2D physics;
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

        if (playerHealth != null && !playerHealth.isAlive)
        {
            direction = Vector2.zero;
            if (animator != null) animator.SetFloat("Speed", 0);
            return;
        }

        direction = moveAction.action.ReadValue<Vector2>();
        float magnitude = direction.sqrMagnitude;

        if (animator != null) animator.SetFloat("Speed", magnitude);

        if (spriteRenderer != null)
        {
            if (direction.x < 0) spriteRenderer.flipX = true;
            else if (direction.x > 0) spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        if (playerHealth != null && playerHealth.isAlive && direction != Vector2.zero)
        {
            physics.MovePosition(physics.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            physics.linearVelocity = Vector2.zero; 
        }
    }
}