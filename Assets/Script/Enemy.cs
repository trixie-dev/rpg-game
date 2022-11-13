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
    Collider target;
    bool dead;
    private bool getImpact;
    public GameObject canvas;

    private void Start() {
        startPosition = transform.position;
        targetDetect = GetComponent<TargetDetect>();
        anim = GetComponent<Animator>();
        lastAttack = -cooldown;
    }
    protected override void Update(){
        if(dead)
            return;
        base.Update();
        if (targetDetect.IsTargetInRange(chasingRange))
            target = targetDetect.SelectTarget(chasingRange);
        else
            target = null;

        
        if(target != null){
            isChasing = true;
            anim.SetBool("isChasing", true);
            Move(target.transform.position - transform.position, fighterData.moveSpeed);
            Rotation((target.transform.position - transform.position).normalized);
            if(Vector3.Distance(target.transform.position, transform.position) <= weapon.weaponData.range + 0.5f
                && Time.time - lastAttack > cooldown
                && Time.time - lastImpact > impactTime){
                
                lastAttack = Time.time;
                Invoke("Attack", cooldown/2);
                anim.SetTrigger("Attack");
            }
        }
        else{
            isChasing = false;
            anim.SetBool("isChasing", false);
        }
        
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        lastImpact = Time.time;
        getImpact = true;
        anim.SetTrigger("GetDamage");
    }
    protected override void Death()
    {
        dead = true;
        Collider[] objects = targetDetect.SearchTargets(5);
        foreach(Collider c in objects){
            if(c.GetComponent<Player>() != null){
                c.GetComponent<Player>().AddExp(exp);
                c.GetComponent<Player>().AddGold(gold);
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
