using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    public HUD HUD;

    private void Awake()
    {
        Cursor.visible = false;
        if (CharacterManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        PlayerPrefs.DeleteAll();

        instance = this;
        //SceneManager.sceneLoaded += LoadState;
        //DontDestroyOnLoad(gameObject);
    }

    public Player player;
    public Weapon weapon;
    
    

    

    
}
