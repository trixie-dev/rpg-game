using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public HUD HUD;

    private void Awake()
    {
        if (PlayerManager.instance != null)
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
    // mb weaapon
    

    private void Start()
    {
    }
    private void Update()
    {
        
    }
    /*
    public void SaveState()
    {

        PlayerPrefs.SetInt("Max HP", characterData.maxHP);
        PlayerPrefs.SetInt("Current HP", characterData.currHP);
        PlayerPrefs.SetInt("Max Mana", characterData.maxMana);
        PlayerPrefs.SetInt("Current Mana", characterData.currMana);
        PlayerPrefs.SetInt("Max XP", characterData.experience);

        PlayerPrefs.SetInt("Move Speed", characterData.moveSpeed);
        PlayerPrefs.SetInt("Attack", characterData.attack);
        PlayerPrefs.SetInt("Agility", characterData.agility);
        PlayerPrefs.SetInt("Intelect", characterData.intellect);

        PlayerPrefs.SetFloat("Physical Defence", characterData.physicalDefence);
        PlayerPrefs.SetFloat("Magical Defence", characterData.magicalDefence);

    }

    public void LoadState()
    {
        characterData.maxHP = PlayerPrefs.GetInt("Max HP");
        characterData.currHP = PlayerPrefs.GetInt("Current HP");
        characterData.maxMana = PlayerPrefs.GetInt("Max Mana");
        characterData.currMana = PlayerPrefs.GetInt("Current Mana");
        characterData.experience = PlayerPrefs.GetInt("Max XP");

        characterData.moveSpeed = PlayerPrefs.GetInt("Move Speed");
        characterData.attack = PlayerPrefs.GetInt("Attack");
        characterData.agility = PlayerPrefs.GetInt("Agility");
        characterData.intellect = PlayerPrefs.GetInt("Intelect");

        characterData.physicalDefence = PlayerPrefs.GetFloat("Physycal Defence");
        characterData.magicalDefence = PlayerPrefs.GetFloat("Magical Defence");
    }
    */
}
