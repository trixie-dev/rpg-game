using UnityEngine;

public class TargetDetect : MonoBehaviour {
    [SerializeField] private float reachRadius = 5f;
    public LayerMask fighter;

    public Collider SelectTarget(){
        
        Collider[] objects = Physics.OverlapSphere(transform.position, reachRadius, fighter);
        if (objects.Length == 0)
        {
            return null;
        }
        Collider nearestTarget = objects[0];

        foreach(Collider obj in objects){
            print(obj.name);
            if(Vector3.Distance(transform.position, obj.transform.position) > 0
                && Vector3.Distance(transform.position, obj.transform.position) 
                < Vector3.Distance(transform.position, nearestTarget.transform.position)){
                nearestTarget = obj;
            }
        }
        
        return nearestTarget;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reachRadius);
    }

}