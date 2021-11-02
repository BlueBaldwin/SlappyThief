using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;



public class HandManager : MonoBehaviour
{

    [SerializeField]
    LeapServiceProvider Provider;

    [SerializeField]
    InventoryManager Inventory;

    [SerializeField]
    bool InventoryOnLeft;

    [SerializeField]
    Hand Left;
    [SerializeField]
    Hand Right;


    // Start is called before the first frame update
    void Start()
    {
        Inventory.Init();
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

