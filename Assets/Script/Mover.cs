using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Fighter
{
    private Camera cam;
    //private NavMeshAgent agent;

    public CharacterController controller;
    [SerializeField] private float speed = 6f;
    [SerializeField] float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    void Start()
    {
        cam = Camera.main;
        //agent = GetComponent<NavMeshAgent>();
    }
    protected virtual void ObjectMove(float horizontal, float vertical)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }

        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        controller.Move(direction * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        /*
        if (Input.GetMouseButton(1))
        {
            
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            { 
                agent.SetDestination(hit.point);
            }
        }
        */
    }
}
