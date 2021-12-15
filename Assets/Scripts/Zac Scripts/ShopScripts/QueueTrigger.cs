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
        Trigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ShopperBehaviour s;

        if((s = other.gameObject.GetComponentInParent<ShopperBehaviour>()) != null)
        {
            if (!s.isInQueue)
            {
                ShopInfo.EnqueueShopper(s);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ShopperBehaviour s;
        if ((s = other.gameObject.GetComponentInParent<ShopperBehaviour>()) != null)
        {
            if (s.isInQueue)
            {
               // ShopInfo.DequeueShopper(s); for some reason this is triggering even when shopper isnt moving. Might just leave it doing nothing and see if its fine. 
            }
        }
    }

}
