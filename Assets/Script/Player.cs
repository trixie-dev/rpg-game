using UnityEngine;

public class Player : Mover
{
    public PlayerData playerData;
 
    public void Start()
    {
     
        // TODO: implement player data for save/load system
        playerData = new PlayerData
        {
            name = "spark",
            level = 2,
            goldAmount = 163
        };

    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Move(horizontal, vertical);
    }
    
}
