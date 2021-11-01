using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Interaction;


public class HandManager : MonoBehaviour
{

    [SerializeField]
    LeapServiceProvider Provider;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Frame f = Provider.CurrentFrame;
        foreach (Hand h in f.Hands)
        {
              
        }
    }


}

