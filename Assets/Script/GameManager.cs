using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public FloatingTextManager floatingTextManager;

    public Player player;
    public Weapon weapon;
    public HUD HUD;
    
    

    private void Awake()
    {
        Cursor.visible = false;
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        PlayerPrefs.DeleteAll();

        instance = this;
        //SceneManager.sceneLoaded += LoadState;
        //DontDestroyOnLoad(gameObject);
    }

    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    

}
