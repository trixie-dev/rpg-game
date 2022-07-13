using UnityEngine;


public class Player : Mover
{
    public PlayerData playerData;
    

    
    public void Start()
    {
        
        // not implemented player data!!!!!!!! It'll use with save/load system
        playerData = new PlayerData
        {
            name = "spark",
            level = 2,
            gold = 163
        };

    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        ObjectMove(horizontal, vertical);
    }
    
}
