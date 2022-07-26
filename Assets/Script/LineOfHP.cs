using UnityEngine;
using UnityEngine.UI;

public class LineOfHP : MonoBehaviour {
    
    [SerializeField] private GameObject fighter;
    private Fighter fighterScript;
    public RectTransform HPBar;
    private CanvasGroup canvasGroup;
    [SerializeField] private Vector3 offsetHPLine;
    private void Start() {
        if (gameObject.tag == "Player") 
            return;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        fighterScript = fighter.GetComponent<Fighter>();

    }

    private void Update(){
        transform.position = Camera.main.WorldToScreenPoint(fighter.transform.position) + offsetHPLine;
        HPBar.localScale = new Vector3((float)fighterScript.fighterData.currHP / (float)fighterScript.fighterData.maxHP, 1, 1);
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