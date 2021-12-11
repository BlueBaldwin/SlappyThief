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
    public bool IsFinished;
    [SerializeField]
    GameObject LeapServiceProvider;
    [SerializeField]
    float MinigameHandScale;
    Transform LeapParent;
    Vector3 LeapPos;

    [SerializeField]
    Vector3 MinigameHandsOffset;
    Camera MainCamera;

    public virtual void Start()
    {
        MinigameCamera.enabled = false;
        IsFinished = false;
        LeapParent = LeapServiceProvider.transform.parent;
        MainCamera = Camera.main;
        LeapPos = LeapServiceProvider.transform.position;
    }

    

    public virtual void Load()
    {
        LeapServiceProvider.transform.SetParent(MinigameCamera.transform, false);
        LeapServiceProvider.transform.localPosition = MinigameHandsOffset;
        LeapServiceProvider.transform.localScale *= MinigameHandScale;
        MinigameCamera.enabled = true;
        MainCamera.enabled = false;
        IsFinished = false;
        //call base.Load() when overriding this in custom minigame
    }
    public virtual void Tick()
    {
        if (IsFinished) Unload();
    }

    public void Unload() //same defenition for each minigame object so it can be defined once here
    {
        LeapServiceProvider.transform.SetParent(LeapParent, false);
        LeapServiceProvider.transform.localPosition = LeapPos;
        LeapServiceProvider.transform.localScale /= MinigameHandScale;
        foreach (GameObject g in SpawnedObjects)
        {
            Destroy(g); //destroy all objects that were spawned for the minigame
        }
        SpawnedObjects.Clear();
        MinigameCamera.enabled = false;
        MainCamera.enabled = true;
    }
}


