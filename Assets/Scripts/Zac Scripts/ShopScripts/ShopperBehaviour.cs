using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperBehaviour : MonoBehaviour
{
    public List<ShopItem> ShopperCart;
    [SerializeField]
    float BaseMood;
    ShopItem RequestedItem;
    float Mood;
    [SerializeField]
    float BaseRequestTime;
    float Timer;
    [SerializeField]
    int MaxCartSize;
    int TargetItems;
    public bool isInQueue;
    public bool isPendingItemRequest;
    [SerializeField]
    float MoodDelta;
    [SerializeField]
    Bounds PickupRange;
  

    

    private void Start()
    {
        ShopperCart = new List<ShopItem>();
        BaseRequestTime *= Random.Range(1, 5);
        Timer = BaseRequestTime;
        BaseMood *= Random.Range(1, 5);
        Mood = BaseMood;
        TargetItems = Random.Range(1, MaxCartSize);
        isInQueue = false;
    }

    private void Update()
    {
        HandleItemRequests();
        HandlePickup();
    }

    void HandlePickup()
    {
        if (PickupRange.Contains(RequestedItem.gameObject.transform.position))
        {
            ShopperCart.Add(RequestedItem);
        }
    }


    void HandleItemRequests()
    {
        if (!isInQueue && Timer <= 0 && ShopperCart.Count < TargetItems && RequestedItem == null)
        {
            isPendingItemRequest = true; //request an item 
            Timer = BaseRequestTime;
        }
        else
        {
            Timer -= Time.deltaTime;
            if (RequestedItem != null || (isInQueue && ShopperCart.Count != 0))
            {
                Mood -= Time.deltaTime;
            }
        }
    }

    public void RequestItem(ShopItem s)
    {
        RequestedItem = s;
    }

    public void RecieveItemFromPlayer(ShopItem s)
    {
        if(RequestedItem == s)
        {
            ShopperCart.Add(s);
            Mood += MoodDelta;
        }
        else
        {
            Mood -= MoodDelta;
        }
    }

    public void PickupRequestedItem()
    {
       ShopperCart.Add(RequestedItem);
    }


}
