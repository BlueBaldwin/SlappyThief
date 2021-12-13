using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

public class HandProcessor : PostProcessProvider
{
    //this may not look like a lot but getting this to work and let me control the hands independently of the leap tracking data took me > 10 hours.
    //its 9:30pm and I started this at 11am
    //i thought it would take 5 mins
    //it wouldve if the documentation was complete / not on dead links. the documentation for PostProcessProvider is 1 line
    //class Leap.Unity.PostProcessProvider : public Leap.Unity.LeapProvider
    //thats it
    //thats all the documentation
    //https://docs.ultraleap.com/unity-api/class/class_leap_1_1_unity_1_1_post_process_provider.html?highlight=postprocess#class-Leap.Unity.PostProcessProvider
    //i hate everything
    //it works now, but making it work also broke some other stuff
    //time to fix all that

    [SerializeField]
    Vector3 BaseHandPositionScalar;
    Vector3 HandPositionScalar;
    LeapTransform LeapTransform; 

    public void SetScale(float f)
    {
        gameObject.transform.localScale = Vector3.one * f;
    }

    public void ResetScale()
    {
        gameObject.transform.localScale = Vector3.one;
    }

    public void SetTransform(LeapTransform t)
    {
        LeapTransform = t;
    }

    public void ResetTransform()
    {
        LeapTransform = LeapTransform.Identity;
    }

    public void SetHandPositionScalar(Vector3 v)
    {
        HandPositionScalar = v;
    }

    public void ResetHandPositionScalar()
    {
        HandPositionScalar = BaseHandPositionScalar;
    }

    private void Start()
    {
        HandPositionScalar = BaseHandPositionScalar;
        LeapTransform = LeapTransform.Identity;
    }

    public override void ProcessFrame(ref Frame inputFrame)
    {
        foreach (Hand h in inputFrame.Hands)
        {
            ApplyMovementScaling(h);
            h.Transform(LeapTransform);
        }
    }

    void ApplyMovementScaling(Hand h)
    {
        Pose p = h.GetPalmPose();
        Vector3 v = p.position;
        v.Scale(HandPositionScalar);
        h.SetPalmPose(p.WithPosition(v));
    }
}
