using UnityEngine;
using UnityEngine.AI;

public class Enemy : Mover
{
    private NavMeshAgent agent;
    public int chasingRange = 10;
    public Weapon weapon;
    public int exp;
    public int gold;
    private Animator anim;
    bool isChasing;
    Vector3 startPosition;
    private Vector3 pushBackDir = Vector3.zero;
    private float pushForce = 0;
    Collider target;
    bool dead;
    private bool canWalk = true;
    private bool getImpact;
    public GameObject canvas;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        targetDetect = GetComponent<TargetDetect>();
        anim = GetComponent<Animator>();
        lastAttack = -cooldown;
        agent.speed = fighterData.moveSpeed;
    }
    protected override void Update(){
        if (dead)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            return;
        }

        if (targetDetect.IsTargetInRange(chasingRange))
        {
            target = targetDetect.SelectTarget(chasingRange);
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
            target = null;
        }
            

        
        if(target != null){
            if (Time.time - lastImpact > 0.7f)
            {
                agent.isStopped = false;
                agent.angularSpeed = 120;
                getImpact = false;
            }
            if (canWalk)
            {


                agent.stoppingDistance = 1.7f;
                /*
                if (agent.remainingDistance <= 1.7f && getImpact == false)
                {
                    agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                }*/
                
                
                
                
                
                anim.SetBool("isChasing", true);
                if (getImpact)
                {
                    anim.SetTrigger("GetDamage");
                    agent.destination = (transform.position + pushBackDir * pushForce);
                    agent.angularSpeed = 0;
                    //fighterData.moveSpeed += (int)pushForce;
                    //agent.isStopped = true;
                    
                    //transform.Translate(-pushBackDir * pushForce * Time.deltaTime);
                }
                else
                {
                    //Rotation();
                    //fighterData.moveSpeed = fighterData.baseMoveSpeed;
                    agent.destination = (target.transform.position );
                    if(Vector3.Distance(target.transform.position, transform.position) <= weapon.weaponData.range + 0.5f
                       && Time.time - lastAttack > cooldown
                       && Time.time - lastImpact > impactTime){
                
                        lastAttack = Time.time;
                        Invoke("Attack", cooldown/2);
                        anim.SetTrigger("Attack");
                    }
                }
               
               
            }
            
        }
        else{
            isChasing = false;
            anim.SetBool("isChasing", false);
            //agent.isStopped = true;
        }

        
        
        
        
        
    }

    private void Rotation()
    {
        // реалізувати поворт за напрямком руху navmeshagent
        Vector3 direction = agent.velocity;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, fighterData.rotationSpeed * Time.deltaTime);
        }
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        
        lastImpact = Time.time;
        getImpact = true;
        
        // push back
        if (dmg.pushForce > 0)
        {
            pushForce = dmg.pushForce;
            
            pushBackDir = (transform.position - dmg.origin);
            fighterData.currBash += dmg.bashCount;
            if(fighterData.currBash >= fighterData.maxBash)
            {
                
                fighterData.currBash = 0;
            }
        }
        
        
        GameManager.instance.ShowText((-dmg.damageAmout).ToString(), 20, new Color(1f, 0f, 0f, 1), transform.position + pushBackDir,  (Vector3.up + pushBackDir)* 40, 2.0f);
    }
    protected override void Death()
    {
        dead = true;
        Collider[] objects = targetDetect.SearchTargets(5);
        foreach(Collider c in objects){
            if(c.GetComponent<Player>() != null)
            {
                Player player = c.GetComponent<Player>();
                player.AddExp(exp);
                GameManager.instance.ShowText("+" + exp.ToString() + " exp", 20, new Color(0, 1, 0, 1), transform.position + pushBackDir, (Vector3.up + pushBackDir)* 40, 2.0f);
                player.AddGold(gold);
                GameManager.instance.ShowText("+" + gold + " gold", 20, new Color(1, 1, 0, 1), transform.position + pushBackDir + new Vector3(0, 1.5f, 0), (Vector3.up + pushBackDir)* 40, 2.0f);
                player.Murder();
            }
        }
        anim.SetTrigger("Death");
        Destroy(canvas);
        
        Invoke("DestroyObject", 3);
        
    }
    private void DestroyObject(){
        Destroy(gameObject);
    }
    private void Attack(){
        if(getImpact){
            getImpact = false;
            return;
        }
        weapon.Attack();
    }

    

}
