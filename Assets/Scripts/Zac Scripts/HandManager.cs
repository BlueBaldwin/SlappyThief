using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Interaction;



public class HandManager : MonoBehaviour
{

    [SerializeField]
    LeapServiceProvider Provider;

    [SerializeField]
    InventoryManager Inventory;

    [SerializeField]
    MinigameManager Minigames;

    [SerializeField]
    bool InventoryOnLeft;

    [SerializeField]
    Hand Left;
    [SerializeField]
    Hand Right;

    [SerializeField]
    float MinigameOffset;

    [SerializeField]
    InteractionButton TestMinigameButton;

    Vector3 OffsetVector;


    // Start is called before the first frame update
    void Start()
    {
        OffsetVector = new Vector3(0, MinigameOffset, -MinigameOffset);
        TestMinigameButton.OnContactBegin = ToggleMinigame;
        Inventory.Init();
    }

    void ToggleMinigame()
    {
        if (Minigames.GetActiveMinigame() == 0)
        {
            MinigameBegin(1);
        }
        else
        {
            MinigameEnd();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Left = null;
        Right = null;
        Frame f = Provider.CurrentFrame;
        AssignHands(f);
        if (InventoryOnLeft && Left != null)
        {
            Inventory.AttachToHand(Left);
        }
        else if (!InventoryOnLeft && Right != null)
        {
            Inventory.AttachToHand(Right);
        }
        
    }

    void MinigameBegin(int i)
    {
        Minigames.LoadMinigame(i);
        Provider.gameObject.transform.position += OffsetVector;
    }

    void MinigameEnd()
    {
        Minigames.UnloadMinigame();
        Provider.gameObject.transform.position -= OffsetVector;
    }

    void AssignHands(Frame f)
    {
        foreach (Hand h in f.Hands)
        {
            if (h.IsLeft) Left = h;
            else if (h.IsRight) Right = h;
            else Debug.LogError("Could not determine if hand was left or right!");
        }
    }

  

}

