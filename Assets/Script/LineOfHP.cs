using UnityEngine;
using UnityEngine.UI;

public class LineOfHP : MonoBehaviour {
    
    [SerializeField] private GameObject fighter;
    private Fighter fighterScript;
    private TargetDetect targetDetect;
    public RectTransform HPBar;
    private CanvasGroup canvasGroup;
    [SerializeField] private Vector3 offsetHPLine;
    private void Start() {
        if (gameObject.tag == "Player") 
            return;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        fighterScript = fighter.GetComponent<Fighter>();
        targetDetect = fighter.GetComponent<TargetDetect>();
    }

    private void Update(){
        transform.position = Camera.main.WorldToScreenPoint(fighter.transform.position) + offsetHPLine;
        HPBar.localScale = new Vector3((float)fighterScript.fighterData.currHP / (float)fighterScript.fighterData.maxHP, 1, 1);
        if(gameObject.tag == "Player")
            return;
        if (targetDetect.IsTargetInRange(fighterScript.fighterData.searchTargetRadius))
        {
            DisplayHP();
        }
        else
        {
            HideHP();
        }
    }

    public void DisplayHP()
    {
        canvasGroup.alpha = 1;
    }

    public void HideHP()
    {
        canvasGroup.alpha = 0;
    }
}