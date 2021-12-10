using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TillMinigame : Minigame
{

    [SerializeField]
    Vector3 ItemOffset;
    ShopInfo ShopInfo;
    ShopperBehaviour CurrentShopper;
    [SerializeField]
    Transform Scanner;
    LineRenderer lr;
    [SerializeField]
    GameObject Coin;
    List<GameObject> Coins;
    [SerializeField]
    GameObject TillTray;

    Vector3 ClosedTill;
    Vector3 OpenTill;
    bool CompletedQueue;
    bool TakenMoney;
    bool CoinsSpawned;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 5;
        lr.startColor = Color.red;
        ShopInfo = FindObjectOfType<GameplayManager>().ShopInfo;
        ClosedTill = TillTray.transform.position;
        OpenTill = TillTray.transform.position;
        OpenTill.y = 0;
    }
    public override void Load()
    {
        CompletedQueue = false;     
        lr.enabled = true;        
        base.Load();
    }

    public override void Tick()
    {
        ScanItems();
        base.Tick();
    }

    public void ScanItems()
    {
        if (CurrentShopper == null)
        {
            CurrentShopper = ShopInfo.DequeueShopper();
            if (CurrentShopper != null)
            {
                TakenMoney = false;
                for (int i = 0; i < CurrentShopper.ShopperCart.Count; ++i)
                {
                    ShopItem s = CurrentShopper.ShopperCart[i];
                    s.gameObject.transform.parent = gameObject.transform;
                    s.gameObject.transform.localPosition = ItemOffset * i;
                    SpawnedObjects.Add(s.gameObject);
                }
            }
            else
            {
                //we have finished the queue
                CompletedQueue = true;
                lr.enabled = false;
            }
        }
        else
        {
            if (CurrentShopper.ShopperCart.Count == 0)
            {
                //take money
                if (!TakenMoney)
                {
                    //open till
                    TillTray.transform.localPosition = OpenTill;
                    Bounds bounds = TillTray.GetComponent<BoxCollider>().bounds;
                    Coins = new List<GameObject>();
                    if (!CoinsSpawned)
                    {                    
                        for (int i = 0; i < Random.Range(0, 5); ++i)
                        {
                            GameObject g = Instantiate(Coin, Scanner.transform);
                            g.transform.localPosition += Vector3.up * i;
                            Coins.Add(g);
                        }
                        CoinsSpawned = true;
                    }
                    else
                    {
                       
                        foreach(GameObject Coin in Coins)
                        {
                            if (bounds.Contains(Coin.transform.position))
                            {
                                Coins.Remove(Coin);
                            }
                        }
                        if (Coins.Count == 0) TakenMoney = true;
                    }
                }
                else
                {
                    //go to next shopper in queue TODO: give shopper waypoint to exit?
                    CurrentShopper = null;
                }
            }
            else
            {
                Vector3 RayStart = Scanner.position;
                Vector3 dir = Vector3.up;
                Ray ray = new Ray(RayStart, dir);
                RaycastHit Hit = new RaycastHit();
                float dist = 1000;
                Vector3 RayEnd;
                if (Physics.Raycast(ray, out Hit, dist))
                {
                    ShopItem s;
                    if ((s = Hit.collider.gameObject.GetComponent<ShopItem>()) != null)
                    {
                        CurrentShopper.ShopperCart.Remove(s);
                        RayEnd = Hit.point;
                    }
                    else
                    {
                        RayEnd = RayStart + (dist * dir);
                    }

                    lr.SetPosition(0, RayStart);
                    lr.SetPosition(1, RayEnd);
                }
            }
        }
    }

}
