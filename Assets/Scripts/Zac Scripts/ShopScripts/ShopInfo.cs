using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    // Start is called before the first frame update
    private List<ShopItem> AvailableItems;
    public  List<ShopperBehaviour> ActiveShoppers;
    private Queue<ShopperBehaviour> QueuedShoppers;
    bool init = false;

    public void EnqueueShopper(ShopperBehaviour s)
    {
        if (s.ShopperCart.Count != 0)
        {
            QueuedShoppers.Enqueue(s);
            s.isInQueue = true;
            Debug.Log(s.name + "queued");
        } 
        else
        {
            Debug.Log(s.name + "is trying to queue but has an empty cart");
        }
    }

    public ShopperBehaviour DequeueShopper()
    {
        if(QueuedShoppers.Count > 0)
        {
            return QueuedShoppers.Dequeue();
        }
        return null;
    }

    public void Init()
    {
        if (!init)
        {

            PopulateLists();
            init = true;
        }
        else
        {
            Debug.Log("Already Initialized!");
        }
    }

    public bool RemoveShopItem(ShopItem s)
    {
       return AvailableItems.Remove(s);
    }

    public void AddShopItem(ShopItem s)
    {
        AvailableItems.Add(s);
    }

    public void PopulateLists()
    {
        AvailableItems = new List<ShopItem>(FindObjectsOfType<ShopItem>());
        ActiveShoppers = new List<ShopperBehaviour>(FindObjectsOfType<ShopperBehaviour>());
        QueuedShoppers = new Queue<ShopperBehaviour>();
    }

    public ShopItem RemoveRandomItem()
    {
        ShopItem s = AvailableItems[Random.Range(0,AvailableItems.Count)];
        RemoveShopItem(s);
        return s;
    }

    public int ItemsAvailableCount()
    {
        return AvailableItems.Count;
    }


}
