using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    public Sprite PreviewSprite; //need a sprite for speech/thought bubble?

    [SerializeField]
    public ShopItemTypes.SHOPITEMTYPE ShopItemType;

    private void Start()
    {
        //ensure we can interact with shop items 
        if(gameObject.GetComponent<InteractionBehaviour>() == null){
            gameObject.AddComponent<InteractionBehaviour>(); //should automatically add a rigidbody too
        }
        if(gameObject.GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }
}
