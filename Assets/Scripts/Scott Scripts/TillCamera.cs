using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TillCamera : MonoBehaviour
{
    [SerializeField] Camera CameraOne;
    [SerializeField] Camera CameraTwo;
    [SerializeField] Collider trigger;

    public static bool bChangeToTillCam;
    public static bool bCustomerWaiting;

    // Update is called once per frame
    private void Awake() {
        //camera.Main
        CameraOne.enabled = true;
        CameraTwo.enabled = false;
    }
    
    void Update()
    {
        if (bChangeToTillCam)
        {
            StartTillGame();
        }

        if (!bChangeToTillCam)
        {
            EndTillCam();
        }
    }

    // If Change to till cam is true the cameras will change 
    void StartTillGame()
    {
        CameraOne.enabled = false;
        CameraTwo.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        bCustomerWaiting = true;
        bChangeToTillCam = true;
        //Debug.Log(bChangeToTillCam);
    }

    void EndTillCam()
    {
        CameraOne.enabled = true;
        CameraTwo.enabled = false;
    }
}
