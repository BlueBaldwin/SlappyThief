using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

[RequireComponent(typeof(AnchorableBehaviour))]
public class InventoryItem : MonoBehaviour
{

    AnchorableBehaviour ab;
    [SerializeField]
    float scaleFactor;
    // Start is called before the first frame update
    void Start()
    {
        ab = GetComponent<AnchorableBehaviour>();
        ab.OnAttachedToAnchor = OnAttach;
        ab.OnDetachedFromAnchor = OnDetach;
        
    }

    void OnAttach()
    {
        gameObject.transform.localScale*= scaleFactor;
        ab.anchor.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void OnDetach()
    {
        gameObject.transform.localScale *= 1 / scaleFactor;
        ab.anchor.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
