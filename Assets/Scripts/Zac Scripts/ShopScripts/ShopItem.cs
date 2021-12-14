using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ShopItem : MonoBehaviour
{

    [SerializeField]
    public ShopItemTypes.SHOPITEMTYPE ShopItemType;

    float colliderSize = 2.5f;

    private void Start()
    {

        //ensure we can interact with shop items 
        if(gameObject.GetComponent<InteractionBehaviour>() == null){
             gameObject.AddComponent<InteractionBehaviour>(); //should automatically add a rigidbody too
        }
        if(gameObject.GetComponent<Collider>() == null)
        {
            BoxCollider bc = gameObject.AddComponent<BoxCollider>();
            bc.size = Vector3.one * colliderSize;
            bc.center = new Vector3(0, 0, 1);
        }
        if(gameObject.GetComponent<AnchorableBehaviour>() == null)
        {
             gameObject.AddComponent<AnchorableBehaviour>();
        }
        if(gameObject.GetComponent<InventoryItem>() == null)
        {
            InventoryItem it = gameObject.AddComponent<InventoryItem>();
            it.scaleFactor = 0.25f;
        }
    }
}
