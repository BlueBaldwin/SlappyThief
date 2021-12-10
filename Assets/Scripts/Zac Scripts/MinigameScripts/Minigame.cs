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

    private void Start()
    { 
        MinigameCamera = gameObject.GetComponent<Camera>();
        if(MinigameCamera == null)
        {
            MinigameCamera = gameObject.AddComponent<Camera>();
            MinigameCamera.transform.localPosition = Camera.main.transform.localPosition;
            MinigameCamera.transform.localRotation = Camera.main.transform.localRotation;
        }
        MinigameCamera.enabled = false;
        IsFinished = false;
    }

    public virtual void Load()
    {
        MinigameCamera.enabled = true;
        Camera.main.enabled = false;
        IsFinished = false;
        //call base.Load() when overriding this in custom minigame
    }
    public virtual void Tick()
    {
        if (IsFinished) Unload();
    }

    public void Unload() //same defenition for each minigame object so it can be defined once here
    {
        foreach (GameObject g in SpawnedObjects)
        {
            Destroy(g); //destroy all objects that were spawned for the minigame
        }
        SpawnedObjects.Clear();
        MinigameCamera.enabled = false;
        Camera.main.enabled = true;
    }
}


