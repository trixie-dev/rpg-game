using UnityEngine;

public class Mover : Fighter
{ 
    public CharacterController controller;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isGrounded;

    Vector3 velocity;

    public float rotationSpeed;

    protected override void Update() {
        base.Update();
        // Gravity 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    protected virtual void Move(float horizontal, float vertical, float speed)
    {   
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        controller.Move(direction * speed * Time.deltaTime);        
    }
    protected virtual void Rotation(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float angel = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, lookRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angel, 0f);
        }
    }

    protected virtual void Jump(float horizontal, float vertical){
        if(isGrounded && fighterData.currStamina > 61){
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            ChangeStamina(-60);
        }
    }
    public void ChangeStamina(int count){
        if(count > 0 && fighterData.currStamina < fighterData.maxStamina)
            fighterData.currStamina += count;
        else if(count < 0 && fighterData.currStamina <= fighterData.maxStamina)
            fighterData.currStamina += count;
            
        if(fighterData.currStamina < 0)
            fighterData.currStamina = 0;
        else if(fighterData.currStamina > fighterData.maxStamina)
            fighterData.currStamina = fighterData.maxStamina;
    }
}
