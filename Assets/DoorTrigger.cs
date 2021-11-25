using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject LeftDoor;
    [SerializeField] GameObject RightDoor;

    // private float speed = 0.02f;

    // All set in world positions
    public Transform leftDoorOpenPos;
    public Transform leftDoorClosePos;
    public Transform rightDoorOpenPos;
    public Transform rightDoorClosePos;

    private bool bOpenDoors;
    private bool bCloseDoors;


// Adjust the speed for the application.
    public float speed = 1.0f;

    // The target (cylinder) position.
    private Transform target;

    void Awake()
    {
       

    }

    void Update()
    {
        // Move our position a step closer to the target.
        float step =  speed * Time.deltaTime; // calculate distance to move
        if (bOpenDoors)
        {
            LeftDoor.transform.position = Vector3.MoveTowards(LeftDoor.transform.position, leftDoorOpenPos.position, step);
        }
        if (bCloseDoors)
        {
            LeftDoor.transform.position = Vector3.MoveTowards(LeftDoor.transform.position, leftDoorClosePos.position, step);
        }
        
    }



    private void OnTriggerEnter(Collider other) {
        
        bOpenDoors = true;
         bCloseDoors = false;
        Debug.Log("DoorsOpen");
    }

    private void OnTriggerExit(Collider other) {
        bCloseDoors = true;
        bOpenDoors = false;
        Debug.Log("DoorsClose");
    }
}
