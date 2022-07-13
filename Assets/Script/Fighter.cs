using UnityEngine;
public class Fighter : MonoBehaviour
{
    public float cooldown;
    protected float lastAttack;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;
    public FighterData fighterData;

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
}
