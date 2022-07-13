using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text HPText, ManaText, levelText, XPText, goldText;

    public RectTransform HPBar;
    public RectTransform ManaBar;
    public RectTransform XPBar;

    private Fighter fighter;
    // Updateting HUD
    public void Start()
    {
        fighter = PlayerManager.instance.player;

        HPText.text = fighter.figheterData.currHP.ToString() + " / " + fighter.figheterData.maxHP.ToString();
        ManaText.text = fighter.figheterData.currMana.ToString() + " / " + fighter.figheterData.maxMana.ToString();

        levelText.text = PlayerManager.instance.player.playerData.level.ToString() + " lvl";
        XPText.text = PlayerManager.instance.player.playerData.experience.ToString();
        goldText.text = "G: " + PlayerManager.instance.player.playerData.gold.ToString();

        HPBar.localScale = new Vector3((float)fighter.figheterData.currHP / (float)fighter.figheterData.maxHP, 1, 1);
        ManaBar.localScale = new Vector3((float)fighter.figheterData.currMana / (float)fighter.figheterData.maxMana, 1, 1);
        
    }
    public void UpdateHP()
    {
        
        if (fighter.figheterData.currHP <= 0)
        {
            HPText.text =  "0 / " + fighter.figheterData.maxHP.ToString();
            HPBar.localScale = Vector3.zero;
        }
            
        else
        {
            HPText.text = fighter.figheterData.currHP.ToString() + " / " + fighter.figheterData.maxHP.ToString();
            HPBar.localScale = new Vector3((float)fighter.figheterData.currHP / (float)fighter.figheterData.maxHP, 1, 1);
        }
            
    }

    public void UpdateMana(string x)
    {
        ManaText.text = x;
        ManaBar.localScale = new Vector3((float)fighter.figheterData.currMana / (float)fighter.figheterData.maxMana, 1, 1);
    }
    public void UpdateXP(string x)
    {
        XPText.text = x;
    }
    public void UpdateGold(string x)
    {
        goldText.text = x;
    }
    // and other that not implemented
}

