using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    //class that handles the current gamestate and delegates information to the classes that need it.


    //submanagers
    [SerializeField]
    HandManager HandManager;
    [SerializeField]
    InventoryManager InvManager;
    [SerializeField]
    public ShopInfo ShopInfo;


    private void Start()
    {
        InvManager.Init(); //setup inventory 
        ShopInfo.PopulateLists();
    }

    [SerializeField]
    bool LeftHandInventory = true;

    private void Update()
    {
        HandManager.Tick();
        HandleMinigames();
        HandleInventoryAttachment();
        HandleShoppers();
    }


    void HandleInventoryAttachment()
    {
        if (ActiveMinigame == null)
        {
            InvManager.gameObject.SetActive(true);
            Leap.Hand invHand = HandManager.GetHands()[LeftHandInventory ? 0 : 1];
            if (invHand != null)
            {
                InvManager.AttachToHand(invHand); //assign inventory to correct hand (must be done every frame as hands that leave and re-enter scene are not guaranteed to be the same in LeapSDK)
            }
        }
        else
        {
            //turn off inventory when in a game as it isnt needed
            InvManager.gameObject.SetActive(false);
        }
    }


    void HandleShoppers()
    {
        foreach(ShopperBehaviour s in ShopInfo.ActiveShoppers)
        {
            if (s.isPendingItemRequest && ShopInfo.ItemsAvailableCount() > 0)
            {
                s.RequestItem(ShopInfo.RemoveRandomItem()); //shopper requests a random item
                s.isPendingItemRequest = false;
            }
        }
    }

    Minigame ActiveMinigame = null;

    void HandleMinigames()
    {
        if (ActiveMinigame == null)
        {
            //check if a minigame is selected
            GameObject g = HandManager.GetHandTarget(!LeftHandInventory);
            if (g != null)
            {
                Minigame m;
                if ((m = g.GetComponentInParent<Minigame>()) != null)
                {
                    if (m.CheckStartConditions() == true)
                    {
                        //load selected minigame
                        m.Load();
                        ActiveMinigame = m;
                    }
                }
            }
        }
        else
        {
            if (ActiveMinigame.IsFinished)
            {
                //unload minigame
                ActiveMinigame.Unload();
                ActiveMinigame = null;
            }
            else
            {
                //update minigame
                ActiveMinigame.Tick();
            }
        }
    }


    void SwapInventoryHand()
    {
        LeftHandInventory = !LeftHandInventory;
    }
}

