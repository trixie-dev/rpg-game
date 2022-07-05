using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public InteractiveObject interactScript;
    public enum door_axis_ENUM {x, y, z};
    public door_axis_ENUM door_axis;

    public float open_angel;
    public float speed_open;

    public float start_angel;
    private bool isOpen;
    void Start()
    {
        if (door_axis == door_axis_ENUM.x) start_angel = transform.localEulerAngles.x;
        else if (door_axis == door_axis_ENUM.y) start_angel = transform.localEulerAngles.y;
        else if (door_axis == door_axis_ENUM.z) start_angel = transform.localEulerAngles.z;
    }
    void Open()
    {
        isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interactScript.isPlayer)
        {

            isOpen=true;
        }
        
        if (isOpen)
        {
            if (door_axis == door_axis_ENUM.x)
            {
                float angelX = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, start_angel + open_angel, speed_open * Time.deltaTime);
                transform.localEulerAngles = new Vector3(angelX, 0, 0);
                if (angelX >= start_angel + open_angel) isOpen = false;
            }
            else if (door_axis == door_axis_ENUM.y)
            {
                float angelY = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, start_angel + open_angel, speed_open * Time.deltaTime);
                transform.localEulerAngles = new Vector3(0, angelY, 0);
                if (angelY >= start_angel + open_angel) isOpen = false;
            }
            else if (door_axis == door_axis_ENUM.z)
            {
                float angelZ = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, start_angel + open_angel, speed_open * Time.deltaTime);
                transform.localEulerAngles = new Vector3(0, 0, angelZ);
                if (angelZ >= start_angel + open_angel) isOpen = false;
            }
            
        }
        
    }
    
}
