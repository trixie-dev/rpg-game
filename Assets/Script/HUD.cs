using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text HPText, ManaText, levelText, XPText, goldAmountText;
    public RectTransform HPBar, ManaBar, XPBar;

    public struct StatusInfo
    {
        public Text innerText;
        public int maxValue, currValue;
        public RectTransform bar;
    }

    private StatusInfo HPInfo;
    private StatusInfo ManaInfo;

    private Player player;

    public void Start()
    {
        player = PlayerManager.instance.player;

        HPInfo = new StatusInfo
        {
            innerText = HPText,
            bar = HPBar
        };
        ManaInfo = new StatusInfo
        {
            innerText = ManaText,
            bar = ManaBar
        };

    }
    private void Update()
    {
        // Updating both HP and Mana variables
        HPInfo.currValue = player.fighterData.currHP;
        HPInfo.maxValue = player.fighterData.maxHP;

        ManaInfo.currValue = player.fighterData.currMana;
        ManaInfo.maxValue = player.fighterData.maxMana;

        // Updating HP's interface
        if (HPInfo.currValue <= 0)
        {
            HPInfo.innerText.text = "0 / " + HPInfo.maxValue.ToString();
            HPInfo.bar.localScale = Vector3.zero;
        }
        else
        {
            HPInfo.innerText.text = HPInfo.currValue.ToString() + " / " + HPInfo.maxValue.ToString();
            HPInfo.bar.localScale = new Vector3((float)HPInfo.currValue / (float)HPInfo.maxValue, 1, 1);
        }

        // Updating Mana's interface
        if (ManaInfo.currValue <= 0)
        {
            ManaInfo.innerText.text = "0 / " + ManaInfo.maxValue.ToString();
            ManaInfo.bar.localScale = Vector3.zero;
        }
        else
        {
            ManaInfo.innerText.text = ManaInfo.currValue.ToString() + " / " + ManaInfo.maxValue.ToString();
            ManaInfo.bar.localScale = new Vector3((float)ManaInfo.currValue / (float)ManaInfo.maxValue, 1, 1);
        }

        levelText.text = player.playerData.level.ToString() + " lvl";
        XPText.text = player.playerData.experience.ToString();
        goldAmountText.text = "G: " + player.playerData.goldAmount.ToString();

    }
    
}

