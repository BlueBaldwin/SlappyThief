using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

[RequireComponent(typeof(AnchorableBehaviour))]
public class InventoryItem : MonoBehaviour
{

    AnchorableBehaviour ab;
    [SerializeField]
    float scaleFactor; //scales object to/from inventory size
    // Start is called before the first frame update
    void Start()
    {
        //setup deletgates
        ab = GetComponent<AnchorableBehaviour>();
        ab.OnAttachedToAnchor = OnAttach;
        ab.OnDetachedFromAnchor = OnDetach;
        
    }

    void OnAttach()
    {
        //shrink object into inventory and hide the empty slot mesh
        gameObject.transform.localScale*= scaleFactor;
        ab.anchor.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void OnDetach()
    {
        //scale object back up to world size, and show that the slot is now empty
        gameObject.transform.localScale *= 1 / scaleFactor;
        ab.anchor.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
