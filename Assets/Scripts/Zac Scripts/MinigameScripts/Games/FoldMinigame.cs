using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldMinigame : Minigame
{
    //local variables needed for this minigame
    [SerializeField]
    GameObject ClothingItem;

    [SerializeField]
    GameObject FoldPoint;

    [SerializeField]
    Vector3 FoldPointOffsets; //used to spawn foldpoints at the correct locations on the shirt, currently only configured for PLACEHOLDERSHIRT prefab.

    [SerializeField]
    Vector3 ClothingOffset;

    List<GameObject> RemainingPoints = new List<GameObject>();

    MessClothes mc;

    public override void Start()
    {
        base.Start();
        mc = GetComponentInChildren<MessClothes>();
    }


    public override bool CheckStartConditions()
    {
        return mc.isMessy;
    }

    //if the shirts we end up using are consistent in size this is fine, if not I can make offsets dynamically change based on the given shirt
    public void CreateFoldPointPair(Vector3 A, Vector3 B)
    {
        Color c = Random.ColorHSV();
        GameObject temp;
        GameObject g = Instantiate(FoldPoint, gameObject.transform);
        g.GetComponent<MeshRenderer>().material.color = c;
        g.transform.localPosition = A + ClothingOffset/4;
        g.SetActive(true);
        RemainingPoints.Add(g);
        temp = g;

        g = Instantiate(FoldPoint, gameObject.transform);
        g.GetComponent<MeshRenderer>().material.color = c;
        g.transform.localPosition = B + ClothingOffset/4;
        temp.GetComponent<FoldPoint>().LinkedPoint = g.transform;

        SpawnedObjects.Add(g);
        SpawnedObjects.Add(temp);
    }

    public override void Load()
    {
        base.Load();
        GameObject g = Instantiate(ClothingItem, gameObject.transform);
        ShopItem s;
        if ((s = g.GetComponent<ShopItem>()) != null)
        {
            Destroy(s); //dont want our representation of the different shop items to actually have shopitem behaviour.             
        }
        SpawnedObjects.Add(g);
        g.transform.position += ClothingOffset;

        //Spawns 3 sets of 2 color coded linked foldpoints, see Foldpoint.cs 
        CreateFoldPointPair(new Vector3(-FoldPointOffsets.x, FoldPointOffsets.y, 0), new Vector3(-FoldPointOffsets.x * 0.5f, FoldPointOffsets.y, FoldPointOffsets.z));
        CreateFoldPointPair(new Vector3(FoldPointOffsets.x, FoldPointOffsets.y, 0), new Vector3(FoldPointOffsets.x * 0.5f, FoldPointOffsets.y, FoldPointOffsets.z));
        CreateFoldPointPair(new Vector3(0, FoldPointOffsets.y, -FoldPointOffsets.z), new Vector3(0, FoldPointOffsets.y, FoldPointOffsets.z * 0.75f));
    }

    public override void Tick()
    {
        if (IsLoaded)
        {
            IsFinished = true;
            foreach (GameObject g in RemainingPoints)
            {
                if (g != null)
                {
                    if (g.activeSelf)
                    {
                        IsFinished = false;
                        break;
                    }
                }
            }
            if (IsFinished)
            {
                mc.bTidyUpMess = true;
            }
        }
    }
}

