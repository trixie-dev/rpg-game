using UnityEngine;

public class Weapon : TargetDetect{
    public WeaponData weaponData;
    private Fighter fighter;

    
    private void Start() {
        fighter = GetComponentInParent<Fighter>();
    }
    public void Attack(){
        Collider[] targets = SearchTargets(weaponData.range);

        if(targets.Length == 0)
            return;
        
        foreach(Collider coll in targets){
            
            Damage dmg = new Damage
            {
                origin = fighter.transform.position,
                damageAmout = weaponData.attack + fighter.fighterData.attack + Random.Range(-weaponData.attack/10, weaponData.attack/10),
                damageType = "physical",
                pushForce = weaponData.pushForce
            };
            coll.gameObject.SendMessage("ReceiveDamage", dmg);
            
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, weaponData.range);
    }
}