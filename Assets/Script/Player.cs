using UnityEngine;

public class Player : Mover
{
    public PlayerData playerData;
    private Animator animator;
    private int isWalkingHash;
    private int isRunningHash;
    private int swingHash;
    private bool isAttaking = false;
 
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        
        if(Time.time - lastAttack > cooldown 
            && Input.GetMouseButtonDown(0))
        {
            animator.SetFloat("SwingSpeed", 1/cooldown);
            lastAttack = Time.time;
            animator.SetTrigger("Swing");
        }

        if((horizontal != 0 || vertical != 0) 
            && Time.time - lastAttack > 4f*cooldown)
        
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
        /*
        if(horizontal==1 && vertical==0)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else if(horizontal==-1 && vertical==0)
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        else if(horizontal==0 && vertical==1)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(horizontal==0 && vertical==-1)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        */
       switch (horizontal, vertical)
        {
            case (1, 0):
                float angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 90, Time.deltaTime * 100);
                transform.localEulerAngles = new Vector3(0, angel, 0);
                //Quaternion.Euler(0, 90, 0);
                break;
            case (-1, 0):
                angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, -90, Time.deltaTime * 100);
                transform.localEulerAngles = new Vector3(0, angel, 0);
                //transform.localRotation = Quaternion.Euler(0, -90, 0);
                break;
            case (0, 1):
                angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 0, Time.deltaTime * 100);
                transform.localEulerAngles = new Vector3(0, angel, 0);
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case (0, -1):
                angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 180, Time.deltaTime * 100);
                transform.localEulerAngles = new Vector3(0, angel, 0);
                //transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            case (1,1):
                angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 45, Time.deltaTime * 100);
                transform.localEulerAngles = new Vector3(0, angel, 0);
                //transform.localRotation = Quaternion.Euler(0, 45, 0);
                break;
            case (-1,1):
                angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, -45, Time.deltaTime * 100);
                transform.localEulerAngles = new Vector3(0, angel, 0);
                //transform.localRotation = Quaternion.Euler(0, -45, 0);
                break;
            case (1,-1):
                angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, 135, Time.deltaTime * 100);
                transform.localEulerAngles = new Vector3(0, angel, 0);
                //transform.localRotation = Quaternion.Euler(0, 135, 0);
                break;
            case (-1,-1):
                angel = Mathf.MoveTowardsAngle(transform.localRotation.eulerAngles.y, -135, Time.deltaTime * 100);
                transform.localEulerAngles = new Vector3(0, angel, 0);
                //transform.localRotation = Quaternion.Euler(0, -135, 0);
                break;
        
            default :
            
                break;
        }
            
        
    }

    
    
    
}
