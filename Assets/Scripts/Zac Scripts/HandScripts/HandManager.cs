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
    Hand Left;
    [SerializeField]
    Hand Right;


    Frame f;

    // Start is called before the first frame update

    public void Update()
    {
        Left = null;
        Right = null;
        f = Provider.CurrentFixedFrame; //update current frame
        AssignHands(); //assign left/right hands, this must be done every frame as hands that leave/reenter the scene are not considered the same hand object in Leap
    }

    void AssignHands()
    {
        //check if hand is left or right and assign appropriately (this will work as long as there are not more than 2 hands in the scene).
        foreach (Hand h in f.Hands)
        {
            if (h.IsLeft) Left = h; 
            else if (h.IsRight) Right = h;
            else Debug.LogError("Could not determine if hand was left or right!");
        }
    }

    public List<Hand> GetHands()
    {
        return new List<Hand>() { Left, Right };
    }
  
    public void AddOffset(Vector3 v)
    {
        Provider.gameObject.transform.position += v;
    }

    public GameObject GetHandTarget(bool b)
    {
        Hand current = null;
        //assign current hand depending on input bool
        foreach(Hand h in f.Hands)
        {
            if(h.IsLeft == b)
            {
                current = h;
                break;
            }
        }

        if (current != null)
        {
            Finger index = current.GetIndex();
            if (index != null)
            {
                //fire out ray to find out what we are pointing at with index finger
                Ray ray = new Ray(LeapUnityUtils.LeapV3ToUnityV3(index.TipPosition), LeapUnityUtils.LeapV3ToUnityV3(index.Direction));
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    return hit.collider.gameObject; //return object we are pointing at 
                }
            }
        }
        return null;
    }

}

