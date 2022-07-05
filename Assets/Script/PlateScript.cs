using UnityEditor;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public float delta = 0.5f;
#if UNITY_EDITOR
    [ContextMenu("Position Plate")]
    
    public void PlatePosition()
    {
        Undo.RecordObject(transform, "plate position");
        transform.position = new Vector3(transform.position.x, Random.Range(transform.position.y-delta, transform.position.y+delta), transform.position.z);
        
        
    }
#endif
}

