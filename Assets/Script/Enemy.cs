using UnityEngine;

public class Enemy : Mover
{
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
    

    private void Start() {
        startPosition = transform.position;
        targetDetect = GetComponent<TargetDetect>();
        anim = GetComponent<Animator>();
        lastAttack = -cooldown;
    }
    protected override void Update(){
        if (dead)
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            return;
        }
            
            
        base.Update();
        if (targetDetect.IsTargetInRange(chasingRange))
            target = targetDetect.SelectTarget(chasingRange);
        else
            target = null;

        
        if(target != null){
            Rotation((target.transform.position - transform.position).normalized);
            if (canWalk)
            {
                isChasing = true;
                anim.SetBool("isChasing", true);
                if (getImpact)
                {
                    Move(transform.position - target.transform.position, pushForce);
                }
                else
                {
                    Move(target.transform.position - transform.position, fighterData.moveSpeed);
                }
               
            
                if(Vector3.Distance(target.transform.position, transform.position) <= weapon.weaponData.range + 0.5f
                   && Time.time - lastAttack > cooldown
                   && Time.time - lastImpact > impactTime){
                
                    lastAttack = Time.time;
                    Invoke("Attack", cooldown/2);
                    anim.SetTrigger("Attack");
                }
            }
            
        }
        else{
            isChasing = false;
            anim.SetBool("isChasing", false);
        }

        if (Time.time - lastImpact > 0.5f)
        {
            
            getImpact = false;
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
            pushBackDir = (transform.position - dmg.origin).normalized;
            
            
            
        }
        
        anim.SetTrigger("GetDamage");
        GameManager.instance.ShowText((-dmg.damageAmout).ToString(), 20, new Color(1f, 0f, 0f, 1), transform.position + pushBackDir, (Vector3.up + pushBackDir)* 20, 1.0f);
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
                GameManager.instance.ShowText("+" + exp.ToString() + " exp", 20, new Color(0, 1, 0, 1), transform.position + pushBackDir, Vector3.up * 20, 2.0f);
                player.AddGold(gold);
                GameManager.instance.ShowText("+" + gold + " gold", 20, new Color(1, 1, 0, 1), transform.position + pushBackDir + new Vector3(0, 1.5f, 0), Vector3.up * 20, 2.0f);
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
