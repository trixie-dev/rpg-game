using UnityEngine;

public class Enemy : Mover
{
    public int chasingRange = 10;
    public Weapon weapon;
    private Animator anim;
    bool isChasing;
    Vector3 startPosition;
    Collider target;
    bool dead;
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
                && Time.time - lastAttack > cooldown){
                
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
        anim.SetTrigger("GetDamage");
    }
    protected override void Death()
    {
        dead = true;
        anim.SetTrigger("Death");
        Invoke("DestroyObject", 3);
    }
    private void DestroyObject(){
        Destroy(gameObject);
    }
    private void Attack(){
        weapon.Attack();
    }

    

}
