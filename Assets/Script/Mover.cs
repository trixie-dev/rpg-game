using UnityEngine;

public class Mover : Fighter
{ 
    public CharacterController controller;
    [SerializeField] float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    protected virtual void Move(float horizontal, float vertical, float speed)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        controller.Move(direction * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
