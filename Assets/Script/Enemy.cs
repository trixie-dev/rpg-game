using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision coll)
    {
        Debug.Log(coll.collider.tag);
        if(coll.collider.tag == "Player")
        {
            Damage dmg = new Damage
            {
                origin = transform.position,
                damageAmout = figheterData.attack
                // push force
            };
            coll.gameObject.SendMessage("ReceiveDamage", dmg);
        }
        
    }
}
