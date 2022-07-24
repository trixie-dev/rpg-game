using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private bool cameraPosition = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            if (!cameraPosition)
            {
                transform.localRotation = Quaternion.Euler(50, 90, 0);
                offset = new Vector3 (-20, 25, 0);
                cameraPosition = true;
            }
            else if (cameraPosition)
            {
                transform.localRotation = Quaternion.Euler(50, 0, 0);
                offset = new Vector3(0, 25, -20);
                cameraPosition = false;
            }
            



        }
        transform.position = player.position + offset;
    }
}