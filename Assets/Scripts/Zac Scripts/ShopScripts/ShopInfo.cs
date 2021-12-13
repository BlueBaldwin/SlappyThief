using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopInfo : MonoBehaviour
{
    // Start is called before the first frame update
    private List<ShopItem> AvailableItems;
    public  List<ShopperBehaviour> ActiveShoppers;
    private Queue<ShopperBehaviour> QueuedShoppers;
    bool init = false;


    public int QueueLength()
    {
        return QueuedShoppers.Count;
    }

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

    public void DequeueShopper(ShopperBehaviour s)
    {
        //Queue does not have a remove method, so we have to replace the list with a version of the list that doesnt include the item we want to remove. Which sounds dumb and bad but whatever.
        QueuedShoppers = (Queue<ShopperBehaviour>)QueuedShoppers.Where(x  => x!!= s);
        Debug.Log("Removed " + s.name + "from queue");
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
        if (AvailableItems.Count == 0) Debug.LogError("Can't find any shopitems!");
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
