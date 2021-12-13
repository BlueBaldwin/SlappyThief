using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    //abstract class that polymophs into the various minigames
    //spawnedobjects tracks the list of prefabs created by the minigame
    public List<GameObject> SpawnedObjects; //add to this list when spawning objects for minigame
    [SerializeField]
    Camera MinigameCamera;
    [SerializeField]
    HandProcessor HandProcessor;
    [SerializeField]
    float HandSizeScalar;
    Vector3 MinigameMovementScalar;
    [SerializeField]
    Vector3 Offset;
    Leap.LeapTransform MinigameTransform;
    public bool IsFinished;


    Camera MainCamera;

    public virtual void Start()
    {
        MinigameCamera.enabled = false;
        IsFinished = false;
        MainCamera = Camera.main;
        MinigameTransform = LeapUnityUtils.UnityTransformToLeapTransform(MinigameCamera.transform);
        MinigameTransform.translation += LeapUnityUtils.UnityV3ToLeapV3(Offset);
        MinigameMovementScalar = Vector3.one*HandSizeScalar;
    }

    

    public virtual void Load()
    {
        MinigameCamera.enabled = true;
        MainCamera.enabled = false;
        IsFinished = false;
        HandProcessor.SetHandPositionScalar(MinigameMovementScalar);
        HandProcessor.SetTransform(MinigameTransform);
        HandProcessor.SetScale(HandSizeScalar);

        //call base.Load() when overriding this in custom minigame
    }
    public virtual void Tick()
    {
        MinigameTransform = LeapUnityUtils.UnityTransformToLeapTransform(MinigameCamera.transform);
        MinigameTransform.translation += LeapUnityUtils.UnityV3ToLeapV3(Offset);
        if (IsFinished) Unload();
    }

    public void Unload() //same definition for each minigame object so it can be defined once here
    {
        foreach (GameObject g in SpawnedObjects)
        {
            Destroy(g); //destroy all objects that were spawned for the minigame
        }
        SpawnedObjects.Clear();
        MinigameCamera.enabled = false;
        MainCamera.enabled = true;
        HandProcessor.ResetHandPositionScalar();
        HandProcessor.ResetTransform();
        HandProcessor.ResetScale();
    }
}


