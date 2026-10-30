using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpVelocity = 1.0f;
    [SerializeField] float gravityMultiplier = 1.0f;
    [SerializeField] float deathHeight = -4.0f;

    Vector3 velocity;
    CharacterController controller;

    Vector3 initPos;

    float input;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        initPos = transform.position;
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        var newInput = context.ReadValue<float>();

        input = newInput;
    }
    public void JumpInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        TryJump();
    }

    private void Update()
    {
        if (!controller.isGrounded) velocity.y += Physics.gravity.y * gravityMultiplier;
        
        velocity.x = input * moveSpeed;
        
        controller.Move(velocity * Time.deltaTime);

        if (transform.position.y <= deathHeight) transform.position = initPos;
    }

    void TryJump()
    {
        if (controller.isGrounded) velocity.y = jumpVelocity;
    }
}
