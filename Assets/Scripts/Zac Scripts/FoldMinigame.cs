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
    //if the shirts we end up using are consistent in size this is fine, if not I can make offsets dynamically change based on the given shirt
    public override void Load(GameObject MinigameParent)
    {
        //awful bad terrible test code i will *probably* delete this.
        //Spawns 3 sets of 2 color coded linked foldpoints, see Foldpoint.cs 
        SpawnedObjects.Add(Instantiate(Shirt, MinigameParent.transform));

        Color c = Random.ColorHSV();
        GameObject temp;
        GameObject g = Instantiate(FoldPoint, MinigameParent.transform);
        g.GetComponent<MeshRenderer>().material.color = c;
        g.transform.localPosition = new Vector3(-Offsets.x, Offsets.y, 0);
        temp = g;

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.GetComponent<MeshRenderer>().material.color = c;
        g.transform.localPosition = new Vector3(-Offsets.x / 2, Offsets.y, Offsets.z);
        temp.GetComponent<FoldPoint>().LinkedPoint = g.transform;

        SpawnedObjects.Add(g);
        SpawnedObjects.Add(temp);

        c = Random.ColorHSV();

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.transform.localPosition = new Vector3(Offsets.x, Offsets.y, 0);
        g.GetComponent<MeshRenderer>().material.color = c;
        temp = g;


        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.transform.localPosition = new Vector3(Offsets.x / 2, Offsets.y, Offsets.z);
        g.GetComponent<MeshRenderer>().material.color = c;
        temp.GetComponent<FoldPoint>().LinkedPoint = g.transform;

        SpawnedObjects.Add(g);
        SpawnedObjects.Add(temp);

        c = Random.ColorHSV();

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.transform.localPosition = new Vector3(0, Offsets.y, -Offsets.z);
        g.GetComponent<MeshRenderer>().material.color = c;
        temp = g;

        g = Instantiate(FoldPoint, MinigameParent.transform);
        g.transform.localPosition = new Vector3(0, Offsets.y, Offsets.z);
        g.GetComponent<MeshRenderer>().material.color = c;
        temp.GetComponent<FoldPoint>().LinkedPoint = g.transform;

        SpawnedObjects.Add(g);
        SpawnedObjects.Add(temp);
    }
}

