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

    [SerializeField] private float rotationSpeed;
    private float angel;
    private float horizontal;
    private float vertical;

    private void Update(){
        
    }
    
    protected virtual void Move(float x, float z, float speed)
    {   
        horizontal = x;
        vertical = z;
        
        // Move the character
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 direction = new Vector3(x, 0f, z).normalized;
        controller.Move(direction * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    protected virtual void Rotation(float horizontal, float vertical)
    {
        /*
        if (horizontal != 0 || vertical != 0)
        {
            angel = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angel, 0f);
        }
        */
        // Rotate the player to face the direction they are moving in
        if(horizontal > 0 && vertical == 0){
            angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 90, Time.deltaTime * rotationSpeed);
            transform.localEulerAngles = new Vector3(0, angel, 0);
        }
            
        else if(horizontal < 0 && vertical == 0){
            angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, -90, Time.deltaTime * rotationSpeed);
            transform.localEulerAngles = new Vector3(0, angel, 0);
            //transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
            

        else if (horizontal == 0 && vertical > 0){
            angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 0, Time.deltaTime * rotationSpeed);
            transform.localEulerAngles = new Vector3(0, angel, 0);
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
            
            
        else if (horizontal == 0 && vertical < 0){
            angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 180, Time.deltaTime * rotationSpeed);
            transform.localEulerAngles = new Vector3(0, angel, 0);
            //transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
            
            
        else if(horizontal > 0 && vertical > 0){
            angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 45, Time.deltaTime * rotationSpeed);
            transform.localEulerAngles = new Vector3(0, angel, 0);
            //transform.localRotation = Quaternion.Euler(0, 45, 0);
        }
            
            
        else if(horizontal < 0 && vertical > 0){
            angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, -45, Time.deltaTime * rotationSpeed);
            transform.localEulerAngles = new Vector3(0, angel, 0);
            //transform.localRotation = Quaternion.Euler(0, -45, 0);
        }
            
            
        else if(horizontal > 0 && vertical < 0){
            angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 135, Time.deltaTime * rotationSpeed);
            transform.localEulerAngles = new Vector3(0, angel, 0);
            //transform.localRotation = Quaternion.Euler(0, 135, 0);
        }
            
            
        else if(horizontal < 0 && vertical < 0){
            angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, -135, Time.deltaTime * rotationSpeed);
            transform.localEulerAngles = new Vector3(0, angel, 0);
            //transform.localRotation = Quaternion.Euler(0, -135, 0);
        }
    }
    

    
    
}
