using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text HPText, StaminaText, levelText, goldAmountText, MurderCountText;
    public RectTransform HPBarMask, StaminaBarMask, XPBar, StaminaBar;
    public CanvasGroup staminaPanel;
    public Image markPrefab;
    public Image enemyHp;
    public RawImage HPBarRaw, ManaBarRaw;
    [SerializeField] private Vector3 offsetEnemyHpBar;

    public Camera cam;

    public struct StatusInfo
    {
        
        public Text innerText;
        public int maxValue, currValue;
        public RawImage rawImage;
        public RectTransform bar;
        public Rect uvRect;
        public Vector2 barSize;
        
    }

    private StatusInfo HPInfo;
    private StatusInfo ManaInfo;
    private StatusInfo StaminaInfo;
    private StatusInfo XPInfo;

    private Player player;

    public void Start()
    {
        
        player = GameManager.instance.player;

        HPInfo = new StatusInfo
        {
            innerText = HPText,
            rawImage = HPBarRaw,
            bar = HPBarMask,
            barSize = HPBarMask.sizeDelta
            
        };
        HPInfo.uvRect = HPInfo.rawImage.uvRect;
        
        StaminaInfo = new StatusInfo
        {
            innerText = StaminaText,
            rawImage = ManaBarRaw,
            bar = StaminaBarMask,
            barSize = StaminaBarMask.sizeDelta
            
        };
        StaminaInfo.uvRect = StaminaInfo.rawImage.uvRect;
        
        ManaInfo.uvRect = HPInfo.rawImage.uvRect;
        
        XPInfo = new StatusInfo
        {
            bar = XPBar
        };
        
    }
    private void Update()
    {
        // -- Bar effects --
        // HP
        Rect uvRectHP = HPInfo.rawImage.uvRect;
        uvRectHP.x -= 0.06f * Time.deltaTime;
        HPInfo.rawImage.uvRect = uvRectHP;
        // Stamina
        Rect uvRectStamina = StaminaInfo.rawImage.uvRect;
        uvRectStamina.x -= 0.06f * Time.deltaTime;
        StaminaInfo.rawImage.uvRect = uvRectStamina;
        
        // Updating both HP and Mana variables
        HPInfo.currValue = player.fighterData.currHP;
        HPInfo.maxValue = player.fighterData.maxHP;

        StaminaInfo.currValue = player.fighterData.currStamina;
        StaminaInfo.maxValue = player.fighterData.maxStamina;

        // Updating HP's interface
        if (HPInfo.currValue <= 0)
        {
            HPInfo.innerText.text = "0 / " + HPInfo.maxValue.ToString();
            HPInfo.bar.sizeDelta = Vector2.zero;
        }
        else
        {
            HPInfo.innerText.text = HPInfo.currValue.ToString() + " / " + HPInfo.maxValue.ToString();
            HPInfo.bar.sizeDelta = new Vector2(HPInfo.barSize.x * (float)(HPInfo.currValue / (float)HPInfo.maxValue), HPInfo.barSize.y);
        }

        // Updating Stamina's interface
        if (StaminaInfo.currValue <= 0)
        {
            StaminaInfo.innerText.text = "0 / " + StaminaInfo.maxValue.ToString();
            StaminaInfo.bar.sizeDelta = Vector2.zero;
        }
        else
        {
            StaminaInfo.innerText.text = StaminaInfo.currValue.ToString() + " / " + StaminaInfo.maxValue.ToString();
            StaminaInfo.bar.sizeDelta = new Vector2(StaminaInfo.barSize.x * (float)StaminaInfo.currValue / (float)StaminaInfo.maxValue, StaminaInfo.barSize.y);
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

