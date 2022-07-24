using UnityEngine;

public class LineOfHP : MonoBehaviour {
    
    [SerializeField] private GameObject fighter;
    private CanvasGroup canvasGroup;
    [SerializeField] private Vector3 offsetHPLine;
    private void Start() {
        if (gameObject.tag == "Player") 
            return;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void Update(){
        transform.position = Camera.main.WorldToScreenPoint(fighter.transform.position) + offsetHPLine;
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