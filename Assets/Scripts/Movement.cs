using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpVelocity = 1.0f;
    [SerializeField] float gravityMultiplier = 1.0f;

    Vector3 velocity;
    CharacterController controller;

    float input;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
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
        velocity.y += Physics.gravity.y * gravityMultiplier;
        
        velocity.x = input * moveSpeed;
        
        controller.Move(velocity * Time.deltaTime);
    }

    void TryJump()
    {
        if (controller.isGrounded) velocity.y = jumpVelocity;
    }
}
