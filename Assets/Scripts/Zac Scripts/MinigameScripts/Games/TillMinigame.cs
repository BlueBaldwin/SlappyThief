using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TillMinigame : Minigame
{
    [SerializeField]
    Transform ClothesSpawnPoint;
    ShopInfo ShopInfo;
    ShopperBehaviour CurrentShopper;
    ShopItem CurrentItem;
    [SerializeField]
    Transform Scanner;
    LineRenderer lr;
    // [SerializeField]
    // List<GameObject> Coins;
    // [SerializeField]
    // GameObject TillTray;

    // Vector3 ClosedTill;
    // Vector3 OpenTill;

    // bool TakenMoney;
    // bool CoinsSpawned;

    //these were bugged so got rid of them for now 

    float ItemScale = 0.75f;

    public override void Start()
    {
        base.Start();
        Debug.Log("TilStart");
        lr = gameObject.AddComponent<LineRenderer>();
        lr.startWidth = 0.05f;
        lr.startColor = Color.red;
        ShopInfo = FindObjectOfType<GameplayManager>().ShopInfo;
    }

    public override bool CheckStartConditions()
    {
        return ShopInfo.QueueLength() > 0;
    }

    public override void Load()
    {
        lr.enabled = true;
        base.Load();
    }

    public override void Tick()
    {
        ScanItems();
        HandleSpawns();
    }


    private void ScanItems()
    {
        Vector3 RayStart = Scanner.position;
        Vector3 dir = Vector3.up;
        Ray ray = new Ray(RayStart, dir);
        RaycastHit Hit;
        float dist = 1000;
        Vector3 RayEnd = RayStart + (dir * dist);
        if (Physics.Raycast(ray, out Hit, dist))
        {
            if (Hit.collider != null)
            {
                RayEnd = Hit.point;
                ShopItem s;
                Debug.Log("SCANNED" + Hit.collider.name);
                if ((s = Hit.collider.gameObject.GetComponentInParent<ShopItem>()) != null && s == CurrentItem.gameObject.GetComponent<ShopItem>())
                {
                    SpawnedObjects.Add(CurrentItem.gameObject);
                    CurrentItem = null;
                }
            }        
        }
        lr.SetPosition(0, RayStart);
        lr.SetPosition(1, RayEnd);
    }

    private void HandleSpawns()
    {
    
        if(CurrentShopper == null)
        {
            CurrentShopper = ShopInfo.DequeueShopper();
            if(CurrentShopper == null)
            {
                IsFinished = true;
                IsLoaded = false;
                lr.enabled = false;
                Unload();
            }
        }

        if(CurrentItem == null && CurrentShopper.ShopperCart.Count > 0)
        {
            CurrentItem = CurrentShopper.ShopperCart[0];
            CurrentShopper.ShopperCart.Remove(CurrentItem);
            CurrentItem.transform.localScale *= ItemScale;
            CurrentItem.GetComponent<BoxCollider>().size.Scale(Vector3.one * 1.25f);           
            SpawnItem(CurrentItem);
        }

        if(CurrentItem == null)
        {
            CurrentShopper = null;
        }

    }

    private void SpawnItem(ShopItem s)
    {
        CurrentItem.transform.position = ClothesSpawnPoint.position;
        Rigidbody rb = CurrentItem.gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.mass = 1000000;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject != null && CurrentItem != null)
        {
            ShopItem s;
            if ((s = other.gameObject.GetComponentInParent<ShopItem>()) != null && s == CurrentItem.gameObject.GetComponent<ShopItem>())
            {
                SpawnItem(CurrentItem); //if currentitem leaves minigame zone bring it back so we dont get softlocked
            }
        }
    }

}

