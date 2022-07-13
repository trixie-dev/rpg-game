using UnityEngine;

[System.Serializable]
public struct FigheterData
{
    public int maxHP;
    public int maxMana;
    public int maxStamina;
    public int currHP;
    public int currMana;
    public int currStamina;

    public int moveSpeed;
    public int attack;
    public int agility;
    public int intellect;
    public float physicalDefence; // float: 0-1 
    public float magicalDefence; // float: 0-1 
}
[System.Serializable]
public struct PlayerData
{
    public string name;
    public int experience;
    public int level;
    public int gold;
}
public struct Damage
{
    public Vector3 origin;
    public int damageAmout;
    public float pushForce;


}