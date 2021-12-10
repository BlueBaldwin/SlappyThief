using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using Leap;
using Leap.Unity;
using UnityEngine;


public class FoldPoint : MonoBehaviour
{
    [SerializeField]
    public Transform LinkedPoint; //the point that this foldpont is connected to, must move finger from one to the other to clear both points in fold minigame
    //the iinked point will have a linkedpoint of null, logic for both this object and its linkedpoint is handled here, only firing if linkedpoint is not null (meaning this is the original point)
    LeapServiceProvider Provider; //used for setting built in Leap delegate methods and checking that the hand that touches the linked point is the same that touched this point

    

    private void Start()
    {
        GetComponent<InteractionBehaviour>().OnContactBegin = BeginPath; //set the delegate method for this foldpoint (will do nothing if not called on an original point)
        Provider = FindObjectOfType<LeapServiceProvider>(); //get the provider from our scene
    }

    bool pathStarted = false;
    bool? leftHand = null; //nullable bools are terrifying but is probably fine here, true = lefthand touched the original point, false = righthand did it, null = point hasnt had contact yet
    public bool isFinished = false;
    Hand h; //tracks the hand that touched origin point, checked in linkedpoints endpath method to ensure we are mimicking the folding motion and not just pressing the beginning and end point at the same time with different hands

    void BeginPath()
    {
        if (LinkedPoint != null) //if this is an original/first point
        {
            pathStarted = true; //used in update method
            if ((h = GetClosestHand()) != null) leftHand = h.IsLeft; //sets lefthand bool to the hand that contacted this point
        }
    }

    void EndPath() //called when linkedpoint is touched after touching this point
    {
        if (LinkedPoint != null)
        {
            if ((h = GetClosestHand()) != null && h.IsLeft == leftHand) //check the same hand was used
            {
                //disable both this object and its linked point
                LinkedPoint.gameObject.SetActive(false);
                gameObject.SetActive(false);             
            }
        }
    }

    Hand GetClosestHand()
    {
        //returns the hand closest to this object 
        Hand result = null; 
        Frame frame = Provider.CurrentFrame;
        float distance = float.PositiveInfinity;
        foreach(Hand h in frame.Hands)
        {
            Vector3 v = gameObject.transform.position - LeapUnityUtils.LeapV3ToUnityV3(h.PalmPosition); //see LeapUnityUtils.cs for implementation
            float newDist = Mathf.Abs(v.magnitude);
            if (newDist < distance)
            {
                distance = newDist;
                result = h;
            }
        }
        return result;
    }

    private void Update()
    {
        if (LinkedPoint != null)
        {
            if (pathStarted)//when the first object has been contacted, pathstarted will toggle to true
            {
                LinkedPoint.GetComponent<InteractionBehaviour>().OnContactBegin = EndPath; //setup delegate for linkedobject (the linkedobject actually calls the endpath method inside this object which is probably weird and bad but it works)
                pathStarted = false; //stops delegate being reassigned every frame after contact
            }
        }
    }
}
