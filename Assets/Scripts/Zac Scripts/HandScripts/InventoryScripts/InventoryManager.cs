using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using Leap.Unity.Attachments;
using Leap.Unity;
using Leap;

using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    //variables
    [SerializeField]
    GameObject OpenSlot;

    [SerializeField]
    int SlotCount;

    [SerializeField]
    float BaseOffsetX;

    [SerializeField]
    float BaseOffsetZ;

    [SerializeField]
    float Spacing;

    [SerializeField]
    float ItemScale;
    Hand Hand;

    [SerializeField]
    HandProcessor HandProcessor;

    List<GameObject> InventoryObjects;

    float OffsetX;

    public void Init()
    {
        if (InventoryObjects == null)
        {
            InventoryObjects = new List<GameObject>();
        }

        OffsetX = BaseOffsetX;

        for (int i = 0; i < SlotCount; ++i) //create inventory with given parameters
        {
            GameObject g = Instantiate(OpenSlot);
            g.transform.parent = gameObject.transform;
            g.transform.localPosition = (new Vector3(OffsetX, 0, BaseOffsetZ + (Spacing * i)));
            g.transform.localScale *= ItemScale;
            InventoryObjects.Add(g);
        }
    }

    public void AttachToHand(Hand h)
    {
        //attach inventory to hand, inventory X offset is flipped on right hands to ensure the inventory is always on the inside of the hand
        Hand = h;
        if (h.IsRight) OffsetX = -BaseOffsetX;
        else OffsetX = BaseOffsetX;
    }

    void AddObjectSlot() //not currently used
    {
        //add additional slots to inventory
        SlotCount++;    
        GameObject g = Instantiate(OpenSlot);
        g.transform.parent = gameObject.transform;
        g.transform.localPosition = (new Vector3(OffsetX, 0, BaseOffsetZ + (Spacing * SlotCount)));
        InventoryObjects.Add(g);
    }

    void RemoveObjectSlot() //not currently used
    {
        //remove most recent slot added to inventory
        GameObject g = InventoryObjects[SlotCount];
        InventoryObjects.Remove(g);
        Destroy(g);
        SlotCount--;
    }

    private void Update()
    {
        if (Hand != null)
        {
            gameObject.transform.position = LeapUnityUtils.LeapV3ToUnityV3(Hand.PalmPosition);
            gameObject.transform.rotation = LeapUnityUtils.LeapQuatToUnityQuat(Hand.Rotation);
        }
        
    }

}
