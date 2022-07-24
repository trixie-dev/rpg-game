using UnityEngine;

public class Fighter : MonoBehaviour
{
    protected TargetDetect targetDetect;
    [SerializeField] private LineOfHP lineHP;
    public float cooldown;
    protected float lastAttack = 0;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;
    public FighterData fighterData;

    private void Start() {
        targetDetect = GetComponent<TargetDetect>();
        lastImmune = -cooldown;
        
    }
    protected virtual void ReceiveDamage(Damage dmg)
    {
        fighterData.currHP -= dmg.damageAmout;
        //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
        //GameManager.instance.ShowText("-" + dmg.damageAmout.ToString(), 20, Color.red, transform.position, Vector3.up * 25, 0.5f);
        if (fighterData.currHP <= 0)
        {
            fighterData.currHP = 0;
            //Death();
        }

    }
    protected virtual void Update(){
        if(gameObject.tag == "Player")
            return;
        if (targetDetect.IsTargetInRange(fighterData.searchTargetRadius))
        {
            lineHP.DisplayHP();
        }
        else
        {
            lineHP.HideHP();
        }
    }
    

    
    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fighterData.searchTargetRadius);
    }
}
