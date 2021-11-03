using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using Leap;
using Leap.Unity;
using UnityEngine;


public class FoldPoint : MonoBehaviour
{
    [SerializeField]
    public Transform LinkedPoint;
    LeapServiceProvider Provider;


    private void Start()
    {
        GetComponent<InteractionBehaviour>().OnContactBegin = BeginPath;
        Provider = FindObjectOfType<LeapServiceProvider>();
    }

    bool pathStarted = false;

    bool? leftHand = null;

    Hand h;

    void BeginPath()
    {
        if (LinkedPoint != null)
        {
            pathStarted = true;
            if ((h = GetClosestHand()) != null) leftHand = h.IsLeft;
        }
    }

    void EndPath()
    {
        if (LinkedPoint != null)
        {
            if ((h = GetClosestHand()) != null && h.IsLeft == leftHand)
            {
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
            Vector3 v = gameObject.transform.position - LeapUnityUtils.LeapV3ToUnityV3(h.PalmPosition);
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
            if (pathStarted)
            {
                LinkedPoint.GetComponent<InteractionBehaviour>().OnContactBegin = EndPath;
                pathStarted = false;
            }
        }
    }
}
