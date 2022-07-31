using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text HPText, ManaText, levelText, goldAmountText;
    public RectTransform HPBar, ManaBar, XPBar, StaminaBar;
    public CanvasGroup staminaPanel;
    public Image markPrefab;
    public Image enemyHp;
    [SerializeField] private Vector3 offsetEnemyHpBar;

    public Camera cam;

    public struct StatusInfo
    {
        public Text innerText;
        public int maxValue, currValue;
        public RectTransform bar;
    }

    private StatusInfo HPInfo;
    private StatusInfo ManaInfo;
    private StatusInfo StaminaInfo;
    private StatusInfo XPInfo;

    private Player player;

    public void Start()
    {
        player = CharacterManager.instance.player;

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
        StaminaInfo = new StatusInfo
        {
            bar = StaminaBar
        };
        XPInfo = new StatusInfo
        {
            bar = XPBar
        };

    }
    private void Update()
    {
        // Updating both HP and Mana variables
        HPInfo.currValue = player.fighterData.currHP;
        HPInfo.maxValue = player.fighterData.maxHP;

        ManaInfo.currValue = player.fighterData.currMana;
        ManaInfo.maxValue = player.fighterData.maxMana;

        StaminaInfo.currValue = player.fighterData.currStamina;
        StaminaInfo.maxValue = player.fighterData.maxStamina;

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
        // Updating Stamina's interface
        if (StaminaInfo.currValue <= 0)
        {
            StaminaInfo.bar.localScale = Vector3.zero;
            staminaPanel.alpha = 1;
        }
        else
        {
            float scale = (float)StaminaInfo.currValue / (float)StaminaInfo.maxValue;
            StaminaInfo.bar.localScale = new Vector3(1, scale, 1);
            staminaPanel.alpha = 1 - scale;
        }

        int currLevel = player.playerData.level;
        levelText.text = currLevel.ToString() + " lvl";
        if(currLevel == player.playerData.levelTable.Length)
        {
            XPInfo.bar.localScale = Vector3.one;
        }
        else{
            int prevLevelXp = player.GetXpToLevel(currLevel - 1);
            int currLevelXp = player.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXp = player.playerData.experience - prevLevelXp;
            float scale = (float)currXp / (float)diff;
            XPInfo.bar.localScale = new Vector3(scale, 1, 1);

        }
        
        goldAmountText.text = "G: " + player.playerData.goldAmount.ToString();

    }
    
    public void SetFocusMark(Vector3 target)
    {
        Vector3 screenPoint = cam.WorldToScreenPoint(target);
        markPrefab.transform.position = screenPoint;
        
    }
    
    public void ResetFocusMark()
    {
        markPrefab.rectTransform.position = new Vector3(-3000, 0, 0);
    }


}

