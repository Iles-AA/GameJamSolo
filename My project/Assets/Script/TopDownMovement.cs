using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private InputActionReference moveAction;

    private Rigidbody2D physics;
    private Vector2 direction;

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
    }


public void OnMove(InputValue value)
{
    direction = value.Get<Vector2>();
}

    private void OnEnable() => moveAction.action.Enable();
    private void OnDisable() => moveAction.action.Disable();

    private void FixedUpdate()
    {
        physics.MovePosition(physics.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}