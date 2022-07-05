using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public float reachRadius = 5f;
    public LayerMask player;

    public bool isPlayer;
    

    // Update is called once per frame
    void Update()
    {
        isPlayer = Physics.CheckSphere(transform.position, reachRadius, player);
        print(isPlayer);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, reachRadius);
    }
}
