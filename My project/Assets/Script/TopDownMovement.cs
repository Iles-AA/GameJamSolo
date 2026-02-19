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
    }

    private void OnEnable() => moveAction.action.Enable();
    private void OnDisable() => moveAction.action.Disable();

    private void Update()
    {

        if (playerHealth.isAlive)
        {
                    float magnitude = moveAction.action.ReadValue<Vector2>().sqrMagnitude;
        direction = moveAction.action.ReadValue<Vector2>();

        animator.SetFloat("Speed", magnitude);

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
    private void FixedUpdate()
    {
        physics.MovePosition(physics.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);    
    }
}