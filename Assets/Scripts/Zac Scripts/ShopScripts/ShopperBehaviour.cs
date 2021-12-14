using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ShopperBehaviour : MonoBehaviour
{
    public List<ShopItem> ShopperCart;
    [SerializeField]
    float BaseMood;
    ShopItemTypes.SHOPITEMTYPE RequestedItemType;
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
    BoxCollider PickupBox;
    ShopInfo ShopInfo;

    Transform ShopperMovement;

    [SerializeField]
    float SlapVelocity;

    [SerializeField]
    Vector3 CartItemOffset;

    [SerializeField]
    Vector3 PickupRange;

    InteractionBehaviour ib;

    [SerializeField]
    float BaseShakeTimer;
    float ShakeTimer;
    private void Start()
    {
        ShopperCart = new List<ShopItem>();
        BaseRequestTime *= Random.Range(1, 5);
        Timer = BaseRequestTime;
        BaseMood *= Random.Range(1, 5);
        Mood = BaseMood;
        TargetItems = Random.Range(1, MaxCartSize);
        isInQueue = false;
        ib = GetComponent<InteractionBehaviour>();
        if(ib == null)
        {
            ib = gameObject.AddComponent<InteractionBehaviour>();
        }
        ib.OnContactBegin += OnSlap;
        ib.OnGraspStay += OnShake;
        ShakeTimer = BaseShakeTimer;
        ShopperMovement = GetComponentInChildren<SkinnedMeshRenderer>().rootBone;
        PickupBox = gameObject.AddComponent<BoxCollider>();
        PickupBox.isTrigger = true;
        PickupBox.center = ShopperMovement.localPosition;
        PickupBox.size = PickupRange;
        
        
    }
    void OnSlap()
    {
        if(ib.closestHoveringController.velocity.magnitude > SlapVelocity)
        {
            DropRandomItem();
        }
    }

    void OnShake()
    {
        if(ib.graspingController.velocity.magnitude > SlapVelocity && ShakeTimer < 0)
        {
            DropRandomItem();
            ShakeTimer = BaseShakeTimer;
        }
        else
        {
            ShakeTimer -= Time.deltaTime;
        }
    }


    void DropRandomItem()
    {
        if (ShopperCart.Count > 0)
        {
            ShopperCart.RemoveAt(Random.Range(0, ShopperCart.Count));
            Debug.Log(name + " Dropped an item!");
        }
    }

    private void Update()
    {
        HandleItemRequests();
        RenderCart();
    }

    void RenderCart()
    {
        for (int i = 0; i < ShopperCart.Count; ++i)
        {
            ShopperCart[i].gameObject.transform.position = transform.position + ((i + 1) * CartItemOffset);
        }
    }


    void HandleItemRequests()
    {
        if (!isInQueue && Timer <= 0 && ShopperCart.Count < TargetItems && RequestedItemType == ShopItemTypes.SHOPITEMTYPE.UNDEFINED)
        {
            isPendingItemRequest = true; //request an item 
            Timer = BaseRequestTime;
        }
        else
        {
            Timer -= Time.deltaTime;
            if (RequestedItemType != ShopItemTypes.SHOPITEMTYPE.UNDEFINED || (isInQueue && ShopperCart.Count != 0))
            {
                Mood -= Time.deltaTime;
            }
        }
    }

    public void RequestItem(ShopItem s)
    {
        RequestedItemType = s.ShopItemType;
        Debug.Log(name + " requesting " + s.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        ShopItem s;
        if ((s = other.gameObject.GetComponentInParent<ShopItem>()) != null && s.ShopItemType == RequestedItemType)
        {
            if (ShopInfo.AvailableItemsByType[(int)RequestedItemType].Contains(s))
            {
                ShopperCart.Add(s);
                ShopInfo.RemoveShopItem(s);
                RequestedItemType = ShopItemTypes.SHOPITEMTYPE.UNDEFINED;
            }
        }
    }
}
