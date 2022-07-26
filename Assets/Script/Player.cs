using UnityEngine;

public class Player : Mover
{
    public PlayerData playerData;

    private Animator animator;
    private Weapon weapon;

    public Collider[] targets;

    private int isWalkingHash;
    private int isRunningHash;


    private float horizontal;
    private float vertical;
    private bool isFocusedOnTarget;
    private bool isAttacking;
    private float attackDuration = 3.43f;



    public void Start()
    {
        animator = GetComponent<Animator>();
        targetDetect = GetComponent<TargetDetect>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        weapon = CharacterManager.instance.weapon;
        

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
            Invoke("Attack", 1);
            lastAttack = Time.time;
        }

        // Move
        if((horizontal != 0 || vertical != 0) 
            && Time.time - lastAttack > attackDuration * cooldown)
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

        // Set Target
        if (Input.GetKeyDown(KeyCode.F) 
            && targetDetect.SelectTarget(fighterData.searchTargetRadius) != null
            && isFocusedOnTarget != true){
                isFocusedOnTarget = true;
                animator.SetBool("isFocused", true);
            }

        else if(Input.GetKeyDown(KeyCode.F) 
            || targetDetect.SelectTarget(fighterData.searchTargetRadius) == null){

                isFocusedOnTarget = false;
                animator.SetBool("isFocused", false);
            } 
            
        // Rotation
        if(!isFocusedOnTarget)
        {
            Rotation(horizontal, vertical);
            CharacterManager.instance.HUD.ResetFocusMark();
        }
        else
        {
            Vector3 targetPosition = targetDetect.SelectTarget(fighterData.searchTargetRadius).transform.position;
            CharacterManager.instance.HUD.SetFocusMark(targetPosition);
            Vector3 direction = targetPosition - transform.position;
            
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float angel = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, lookRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angel, 0f);
        }
        
    } 

    private void Attack(){
        weapon.Attack();
    }

}
