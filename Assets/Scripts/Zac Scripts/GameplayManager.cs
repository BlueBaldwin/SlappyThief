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
    InventoryManager InventoryManager;
    [SerializeField]
    MinigameManager MinigameManager;

    int inventoryLocation;
    bool gameToBeign;
    bool gameToEnd;
    int minigameID;

    Vector3 OffsetVector;
    [SerializeField]
    float ProviderMinigameOffset; //amount to move leapserviceprovider object when games start or end, this results in the players hands being translated up and inward, making it easier to interact with the minigame and not the objects below

    private void Start()
    {
        InventoryManager.Init(); //setup inventory 
        inventoryLocation = 0; //0 = left hand, 1 = right hand
        OffsetVector = new Vector3(0,ProviderMinigameOffset,-ProviderMinigameOffset); //vector used on leapmotionprovider's transform.position
        gameToBeign = false;
    }

    SelectedBehaviour target; 
    [SerializeField]
    float selectionTime; //base selection timer length
    float timer; //variable used to track timer

    private void FixedUpdate()
    {
        HandManager.Tick();
        Leap.Hand invHand = HandManager.GetHands()[inventoryLocation];
        if (invHand != null)
        {
            InventoryManager.AttachToHand(invHand); //assign inventory to correct hand (must be done every frame as hands that leave and re-enter scene are not guaranteed to be the same in LeapSDK)
        }

        if (!gameToBeign)
        {
            //select minigame
            GameObject g = HandManager.GetHandTarget(inventoryLocation == 1); //get the object we are pointing at (true = lefthand, false = righthand)
            SelectedBehaviour temp = null;
            if (g != null)
            {
                temp = g.GetComponent<SelectedBehaviour>(); //check if the object can be selected
            }

            if (temp != null)
            {
                if (target != null && target.Equals(temp)) //if the object we are pointing at hasnt changed
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        //target has been pointed at long enugh to register a selection
                        gameToBeign = true;
                        timer = selectionTime;
                        minigameID = target.GetGameID(); //get the associated minigame
                    }
                }
                else //target has switched since last frame
                {
                    Debug.Log("target changed to " + temp + "from" + target);
                    timer = selectionTime; //reset timer
                    target = temp;
                }
            }
            else timer = selectionTime;
        }

        if (gameToBeign)
        {
            //begin minigame
            MinigameManager.LoadMinigame(minigameID);
            HandManager.AddOffset(OffsetVector);
            gameToBeign = false;
            gameToEnd = false;
        }

        if (gameToEnd)
        {
            //end minigame
            MinigameManager.UnloadMinigame();
            HandManager.AddOffset(-OffsetVector);
            gameToBeign = false;
            gameToEnd = false;
        }

    }
}
