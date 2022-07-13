using UnityEngine;

[System.Serializable]
public struct FighterData
{
    public int maxHP;
    public int currHP;

    public int maxMana;
    public int currMana;

    public int maxStamina;
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
    public int goldAmount;
}
public struct Damage
{
    public Vector3 origin;
    public int damageAmout;
    public float pushForce;
}

