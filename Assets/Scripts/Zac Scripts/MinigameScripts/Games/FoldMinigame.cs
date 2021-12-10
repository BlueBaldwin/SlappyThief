using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldMinigame : Minigame
{
    //local variables needed for this minigame
    [SerializeField]
    GameObject Shirt;

    [SerializeField]
    GameObject FoldPoint;

    [SerializeField]
    Vector3 Offsets; //used to spawn foldpoints at the correct locations on the shirt, currently only configured for PLACEHOLDERSHIRT prefab.

    List<GameObject> RemainingPoints = new List<GameObject>();


    //if the shirts we end up using are consistent in size this is fine, if not I can make offsets dynamically change based on the given shirt
    public void CreateFoldPointPair(Vector3 A, Vector3 B)
    {
        Color c = Random.ColorHSV();
        GameObject temp;
        GameObject g = Instantiate(FoldPoint, gameObject.transform);
        g.GetComponent<MeshRenderer>().material.color = c;
        g.transform.localPosition = A;
        RemainingPoints.Add(g);
        temp = g;

        g = Instantiate(FoldPoint, gameObject.transform);
        g.GetComponent<MeshRenderer>().material.color = c;
        g.transform.localPosition = B;
        temp.GetComponent<FoldPoint>().LinkedPoint = g.transform;

        SpawnedObjects.Add(g);
        SpawnedObjects.Add(temp);
    }

    public override void Load()
    {
        base.Load();
        //Spawns 3 sets of 2 color coded linked foldpoints, see Foldpoint.cs 
        SpawnedObjects.Add(Instantiate(Shirt, gameObject.transform));
        CreateFoldPointPair(new Vector3(-Offsets.x, Offsets.y, 0), new Vector3(-Offsets.x / 2, Offsets.y, Offsets.z));
        CreateFoldPointPair(new Vector3(Offsets.x, Offsets.y, 0), new Vector3(Offsets.x / 2, Offsets.y, Offsets.z));
        CreateFoldPointPair(new Vector3(0, Offsets.y, -Offsets.z), new Vector3(0, Offsets.y, Offsets.z));
    }

    public override void Tick()
    {
        IsFinished = true;
        foreach (GameObject g in RemainingPoints)
        {
            if (g.activeSelf)
            {
                IsFinished = false;
                break;
            }
        }
        base.Tick();
    }
}

