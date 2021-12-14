using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ShopItem : MonoBehaviour
{
    public enum ShopItemType 
    { 
        RED_SHIRT = 0,
        BLUE_SHIRT = 1,
        TROUSERS = 2,
    }

    [SerializeField] private ShopItemType Type;
        
    [SerializeField]
    public Sprite PreviewSprite; //need a sprite for speech/thought bubble?

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
