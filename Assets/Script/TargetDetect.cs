using System.Linq;
using UnityEngine;

public class TargetDetect : MonoBehaviour {
    public LayerMask fighterLayer;
    public GameObject fighterObject;

    public Collider[] SearchTargets(float radius){
        Collider[] objects = Physics.OverlapSphere(transform.position, radius, fighterLayer);
        // remove self from list
        objects = objects.Where(x => x.gameObject != fighterObject).ToArray();
        if(gameObject.tag == "Mob"){
            objects = objects.Where(x => x.gameObject.tag != "Mob").ToArray();
        }
        return objects;
    }
    public Collider SelectTarget(float radius){
        
        Collider[] objects = SearchTargets(radius);
        if (objects.Length == 0)
        {
            return null;
        }
        Collider nearestTarget = objects[0];

        foreach(Collider obj in objects){
            if(Vector3.Distance(transform.position, obj.transform.position) 
                < Vector3.Distance(transform.position, nearestTarget.transform.position)){
                nearestTarget = obj;
            }
        }
        
        return nearestTarget;
    }

    public bool IsTargetInRange(float radius){
        Collider[] objects = SearchTargets(radius);
        if (objects.Length == 0)
        {
            return false;
        }
        return true;
    }
    

}