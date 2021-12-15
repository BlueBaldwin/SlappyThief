using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ShopItem : MonoBehaviour
{

    [SerializeField]
    public ShopItemTypes.SHOPITEMTYPE ShopItemType;
    bool Hanging;

    Vector3 ColliderSize = new Vector3(5, 10, 3);
    Quaternion BaseRotation;

    public Quaternion GetBaseRotation()
    {
        return BaseRotation;
    }

    private void Start()
    {
        Hanging = true;
        BaseRotation = transform.rotation;
        //ensure we can interact with shop items 
        if(gameObject.GetComponent<InteractionBehaviour>() == null){
             InteractionBehaviour ib = gameObject.AddComponent<InteractionBehaviour>(); //should automatically add a rigidbody too
            ib.manager = FindObjectOfType<InteractionManager>();
            ib.OnContactBegin += Contact;
            GetComponent<Rigidbody>().useGravity = false;

        }
        if(gameObject.GetComponent<Collider>() == null)
        {
            BoxCollider bc = gameObject.AddComponent<BoxCollider>();
            bc.size = ColliderSize;
            bc.center = new Vector3(0, 0, -1);
        }
        if(gameObject.GetComponent<AnchorableBehaviour>() == null)
        {
           AnchorableBehaviour ab = gameObject.AddComponent<AnchorableBehaviour>();
            ab.enabled = true;
            ab.detachWhenGrasped = true;
            ab.maxAnchorRange = 10;
         
            
        }
        if(gameObject.GetComponent<InventoryItem>() == null)
        {
            InventoryItem it = gameObject.AddComponent<InventoryItem>();
            it.scaleFactor = 0.75f;
        }

    }

    private void OnCollisionEnter(Collision other) {
        Contact();
    }
    void Contact()
    {
        if (Hanging)
        {
            Hanging = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
