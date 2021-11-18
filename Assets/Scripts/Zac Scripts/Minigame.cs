using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    //abstract class that polymophs into the various minigames
    //spawnedobjects tracks the list of prefabs created by the minigame
    public List<GameObject> SpawnedObjects; //add to this list when spawning objects for minigame
    public abstract void Load(GameObject MinigameParent);
    //abstract class, implemented individually for each minigame
    public void Unload() //same defenition for each minigame object so it can be defined once here
    {
        foreach (GameObject g in SpawnedObjects)
        {
            Destroy(g); //destroy all objects that were spawned for the minigame
        }
        SpawnedObjects.Clear();
    }
}


