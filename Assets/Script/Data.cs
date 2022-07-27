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
    public int runCost;
    public int staminaRegen;

    public int moveSpeed;
    public int attack;
    public int agility;
    public int intellect;
    public float physicalDefence; // float: 0-1 
    public float magicalDefence; // float: 0-1 

    public float searchTargetRadius;
}
[System.Serializable]
public struct PlayerData
{
    public string name;
    public int experience;
    public int level;
    public int goldAmount;
}

[System.Serializable]
public struct WeaponData
{
    public string name;
    public int level;
    public int attack;
    public float range;
    public float weight;
    public int price;
    public float pushForce;
    public int staminaCost;
}

public struct Damage
{
    public Vector3 origin;
    public int damageAmout;
    public float pushForce;
    public string damageType;
}

