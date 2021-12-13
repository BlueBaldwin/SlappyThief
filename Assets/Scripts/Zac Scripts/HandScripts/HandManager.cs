using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Interaction;
using System;

public class HandManager : MonoBehaviour
{

    [SerializeField]
    LeapServiceProvider Provider;

    [SerializeField]
    Hand LeftHand;
    [SerializeField]
    Hand RightHand;
    Frame frame;

    GameObject LTarget;
    GameObject RTarget;

    [SerializeField]
     public HandProcessor HandProcessor;


    public void Update()
    {
        frame = Provider.CurrentFrame;
        HandProcessor.ProcessFrame(ref frame); //process frame through custom provider that is used by the InteractionManager and HandModelManager
        AssignHands(); //assign left/right hands, this must be done every frame as hands that leave/reenter the scene are not considered the same hand object in Leap
        UpdateTargets();
       
    }
    void AssignHands()
    {
        //check if hand is left or right and assign appropriately (this will work as long as there are not more than 2 hands in the scene).
        foreach (Hand h in frame.Hands)
        {
            if (h.IsLeft) LeftHand = h; 
            else if (h.IsRight) RightHand = h;
            else Debug.LogError("Could not determine if hand was left or right!");
        }
    }

    public List<Hand> GetHands()
    {
        return new List<Hand>() { LeftHand, RightHand };
    }
  
    public void AddOffset(Vector3 v)
    {
        Provider.gameObject.transform.position += v;
    }


    public GameObject GetHandTarget(bool left)
    {   
        if (left) return LTarget;
        else return RTarget;
    }

    void UpdateTargets()
    {
        foreach (Hand current in frame.Hands)
        {        
            Finger index = current.GetIndex();
            if (index != null)
            {
                //fire out ray to find out what we are pointing at with index finger
                Ray ray = new Ray(LeapUnityUtils.LeapV3ToUnityV3(index.TipPosition), LeapUnityUtils.LeapV3ToUnityV3(index.Direction));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                     GameObject result = hit.collider.gameObject; //return object we are pointing at 
                    if (current.IsLeft) LTarget = result;
                    else RTarget = result;
                }
               
            }
            
        }
    }

}

