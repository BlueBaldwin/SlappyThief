using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTrigger : MonoBehaviour
{
    BoxCollider Trigger;
    ShopInfo ShopInfo;
    private void Start()
    {
        ShopInfo = FindObjectOfType<GameplayManager>().ShopInfo;
    }

    private void OnTriggerEnter(Collider other)
    {
        ShopperBehaviour s; 
        if((s = gameObject.GetComponent<ShopperBehaviour>()) != null)
        {
            ShopInfo.EnqueueShopper(s);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
        //Queue has no direct remove method. Do we want shoppers to be able to walk out of the queue or not?
   // }


}
