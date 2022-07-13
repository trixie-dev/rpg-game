using UnityEngine;

public class Enemy : Mover
{
    private void OnCollisionEnter(Collision coll)
    {
        Debug.Log(coll.collider.tag);
        if(coll.collider.tag == "Player")
        {
            Damage dmg = new Damage
            {
                origin = transform.position,
                damageAmout = fighterData.attack
                // push force
            };
            coll.gameObject.SendMessage("ReceiveDamage", dmg);
        }
        
    }

}
