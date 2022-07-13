using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fighter : MonoBehaviour
{
    public float cooldown;
    protected float lastAttack;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;
    public FigheterData figheterData;
   

    private void Start()
    { /*
        hitpoint = PlayerManager.instance.characterData.currHP;
        maxHitpoint = PlayerManager.instance.characterData.maxHP;
        */
    }




    protected virtual void ReceiveDamage(Damage dmg)
    {
        
        figheterData.currHP -= dmg.damageAmout;
        //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
        PlayerManager.instance.HUD.UpdateHP();
        //GameManager.instance.ShowText("-" + dmg.damageAmout.ToString(), 20, Color.red, transform.position, Vector3.up * 25, 0.5f);
        if (figheterData.currHP <= 0)
        {
            figheterData.currHP = 0;
            //Death();
        }
        





    }
}
