using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Vector3 startPosition;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active) return;
        
        if(Time.time - lastShown > duration) 
            Hide();
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1 - ((Time.time - lastShown) / duration));
        go.transform.position += motion * Time.deltaTime;
        
    }
}
