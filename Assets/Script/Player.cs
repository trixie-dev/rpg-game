using UnityEngine;

public class Player : Mover
{
    public PlayerData playerData;
    private TargetDetect targetDetect;
    private Animator animator;

    private int isWalkingHash;
    private int isRunningHash;
    private int swingHash;
    private string whichDirection;

    private float horizontal;
    private float vertical;
    private bool isFocusedOnTarget;
    public void Start()
    {
        animator = GetComponent<Animator>();
        targetDetect = GetComponent<TargetDetect>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        
        lastAttack -= cooldown;
        // TODO: implement player data for save/load system
        playerData = new PlayerData
        {
            name = "spark",
            level = 2,
            goldAmount = 163
        };

    }
    void FixedUpdate()
    {
        // Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(!isGrounded){
            horizontal/=2;
            vertical/=2;
        }


            
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        // Jump
        if(Input.GetButtonDown("Jump"))
        {   
            Jump(horizontal, vertical);
            animator.SetTrigger("Jump");
        }
        
        // Swing
        if(Time.time - lastAttack > cooldown 
            && Input.GetMouseButtonDown(0))
        {
            animator.SetFloat("SwingSpeed", 1/cooldown);
            animator.SetTrigger("Swing");
            lastAttack = Time.time;
        }

        // Move
        if((horizontal != 0 || vertical != 0) 
            && Time.time - lastAttack > 5f*cooldown)
        
        {  
            animator.SetBool(isWalkingHash, true);
            if (Input.GetKey(KeyCode.LeftShift)
                && !isFocusedOnTarget)
            {
                if(fighterData.currStamina > 1){
                    Move(horizontal, vertical, fighterData.moveSpeed * 2);
                    animator.SetBool(isRunningHash, true);
                    ChangeStamina(-fighterData.runCost);
                }
                else
                {
                    Move(horizontal, vertical, fighterData.moveSpeed);
                    animator.SetBool(isRunningHash, false);
                }
                    
                
                

            }
            else
            {
                animator.SetBool(isRunningHash, false);
                ChangeStamina(fighterData.runCost*2);
                Move(horizontal, vertical, fighterData.moveSpeed);
            }
        }
        else
        {
            ChangeStamina(fighterData.runCost*2);
            animator.SetBool(isRunningHash, false);
            animator.SetBool(isWalkingHash, false);
        }    
              
    }
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.F) 
            && targetDetect.SelectTarget() != null
            && isFocusedOnTarget != true){
                isFocusedOnTarget = true;
                animator.SetBool("isFocused", true);
            }
            

        else if(Input.GetKeyDown(KeyCode.F) 
            || targetDetect.SelectTarget() == null){

                isFocusedOnTarget = false;
                animator.SetBool("isFocused", false);
            }
            
            
        // Rotation
        if(!isFocusedOnTarget)
        {
            Rotation(horizontal, vertical);
            PlayerManager.instance.HUD.ResetFocusMark();
        }
        else
        {
            
            Vector3 targetPosition = targetDetect.SelectTarget().transform.position;
            PlayerManager.instance.HUD.SetFocusMark(targetPosition);
            Vector3 direction = targetPosition - transform.position;
            
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float angel = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, lookRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angel, 0f);
        }
        
    }
    
    
}
