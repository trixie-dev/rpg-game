using UnityEngine;

public class Player : Mover
{
    public PlayerData playerData;
    private Animator animator;

    private int isWalkingHash;
    private int isRunningHash;
    private int swingHash;
    private string whichDirection;

    private float horizontal;
    private float vertical;

    public void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        

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
        

        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        
        if(Time.time - lastAttack > cooldown 
            && Input.GetMouseButtonDown(0))
        {
            animator.SetFloat("SwingSpeed", 1/cooldown);
            lastAttack = Time.time;
            animator.SetTrigger("Swing");
        }

        if((horizontal != 0 || vertical != 0) 
            && Time.time - lastAttack > 5f*cooldown)
        
        {  
            animator.SetBool(isWalkingHash, true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool(isRunningHash, true);
                Move(horizontal, vertical, fighterData.moveSpeed * 2);

            }
            else
            {
                animator.SetBool(isRunningHash, false);
                Move(horizontal, vertical, fighterData.moveSpeed);
            }
        }
        else
        {
            animator.SetBool(isRunningHash, false);
            animator.SetBool(isWalkingHash, false);
        }            
    }
    protected override void Update()
    {
        base.Update();
        Rotation(horizontal, vertical);

    }


    
    
    
}
