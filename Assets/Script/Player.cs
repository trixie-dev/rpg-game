using UnityEngine;
using System.Linq;

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
    private bool isRunning;
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
            level = 1,
            goldAmount = 163,
            levelTable = new int[] { 100, 200, 300, 500, 700, 1000, 1500, 2000, 3000, 5000},

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
        PlayerJump();
        
        // Swing
        PlayerAttack();

        // Move
        PlayerMovement(); 

        StaminaRegen();
              
    }
    protected override void Update()
    {
        base.Update();
        // Set Target
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton11))
            && targetDetect.SelectTarget(fighterData.searchTargetRadius) != null
            && isFocusedOnTarget != true){
                isFocusedOnTarget = true;
                animator.SetBool("isFocused", true);
            }

        else if(Input.GetKeyDown(KeyCode.F) 
            || Input.GetKeyDown(KeyCode.JoystickButton11)
            || targetDetect.SelectTarget(fighterData.searchTargetRadius) == null){

                isFocusedOnTarget = false;
                animator.SetBool("isFocused", false);
            } 
            
        // Rotation
        if(!isFocusedOnTarget)
        {
            Rotation(new Vector3(horizontal, 0, vertical));
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

    private void PlayerJump(){
        if(Input.GetButtonDown("Jump")  || Input.GetKeyDown(KeyCode.JoystickButton1))
        {   
            Jump(horizontal, vertical);
            animator.SetTrigger("Jump");
        }
    }
    private void PlayerAttack(){
        if(Time.time - lastAttack > cooldown
            && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton5))
            && fighterData.currStamina >= weapon.weaponData.staminaCost)
        {
            animator.SetFloat("SwingSpeed", attackDuration/cooldown);
            animator.SetTrigger("Swing");
            Invoke("Attack", cooldown/2);
            ChangeStamina(-weapon.weaponData.staminaCost);
            lastAttack = Time.time;
        }
    }

    private void PlayerMovement(){
        if((horizontal != 0 || vertical != 0) 
            && Time.time - lastAttack > cooldown)
        {  
            animator.SetBool(isWalkingHash, true);
            if ((Input.GetKey(KeyCode.LeftShift)  || Input.GetKeyDown(KeyCode.JoystickButton2))
                && !isFocusedOnTarget)
            {
                if(fighterData.currStamina > 1){
                    Move(new Vector3(horizontal, 0, vertical), fighterData.moveSpeed * 2);
                    animator.SetBool(isRunningHash, true);
                    ChangeStamina(-fighterData.runCost);
                    isRunning = true;
                }
                else
                {
                    Move(new Vector3(horizontal, 0, vertical), fighterData.moveSpeed);
                    animator.SetBool(isRunningHash, false);
                }
            }
            else
            {
                isRunning = false;
                animator.SetBool(isRunningHash, false);
                //ChangeStamina(fighterData.runCost*2);
                Move(new Vector3(horizontal, 0, vertical), fighterData.moveSpeed);
            }
        }

        else
        {
            animator.SetBool(isRunningHash, false);
            animator.SetBool(isWalkingHash, false);
        } 
    }

    private void StaminaRegen(){
        if (Time.time - lastAttack > 2*cooldown
            && !isRunning) {
            ChangeStamina(fighterData.staminaRegen);
        }
    }
    public void AddExp(int xp)
    {
        playerData.experience += xp;
        playerData.level = GetCurrentLevel();
    }

    public int GetCurrentLevel(){

        int r = 0;
        int add = 0;

        while (playerData.experience >= add)
        {
            add += playerData.levelTable[r];
            r++;
            if(r == playerData.levelTable.Length)
            {
                return r;
            }
        }
        return r;
    }

    public int GetXpToLevel(int level){
        int r = 0;
        int xp = 0;

        while (r < level){
            xp += playerData.levelTable[r];
            r++;
        }
        return xp;
    }
}
