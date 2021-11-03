using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using Leap.Unity.Attachments;
using Leap.Unity;
using Leap;

using UnityEngine;
public class InventoryManager : MonoBehaviour
{
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
    List<GameObject> InventoryObjects;

    float OffsetX;

    private void Start()
    {
        if (InventoryObjects == null)
        {
            InventoryObjects = new List<GameObject>();
        }
        OffsetX = BaseOffsetX;
    }

    public void Init()
    {
        for (int i = 0; i < SlotCount; ++i)
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
        Hand = h;
        if (h.IsRight) OffsetX = -BaseOffsetX;
        else OffsetX = BaseOffsetX;
    }

    void AddObjectSlot()
    {
        SlotCount++;    
        GameObject g = Instantiate(OpenSlot);
        g.transform.parent = gameObject.transform;
        g.transform.localPosition = (new Vector3(OffsetX, 0, BaseOffsetZ + (Spacing * SlotCount)));
        InventoryObjects.Add(g);
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
