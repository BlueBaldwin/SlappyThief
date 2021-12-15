using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity;

public class ShopInfo : MonoBehaviour
{
    // Start is called before the first frame update
    private List<ShopItem> AvailableItems;
    public  List<ShopperBehaviour> ActiveShoppers;
    private Queue<ShopperBehaviour> QueuedShoppers;
    public  List<List<ShopItem>> AvailableItemsByType;
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
            ShopperBehaviour s = QueuedShoppers.Dequeue();
            s.isInQueue = false;
        }
        return null;
    }

    public void DequeueShopper(ShopperBehaviour s)
    {
        //Queue does not have a remove method, so we have to replace the list with a version of the list that doesnt include the item we want to remove. Which sounds dumb and bad but whatever.
        QueuedShoppers = new Queue<ShopperBehaviour>(QueuedShoppers.Where(x  => x!= s));
        Debug.Log("Removed " + s.name + "from queue");
        s.isInQueue = false;
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
       AvailableItems.Remove(s);
       return AvailableItemsByType[(int)s.ShopItemType].Remove(s);
    }

    public void AddShopItem(ShopItem s)
    {
        AvailableItems.Add(s);
        AvailableItemsByType[(int)s.ShopItemType].Add(s);
    }

    public void PopulateLists()
    {
        AvailableItems = new List<ShopItem>(FindObjectsOfType<ShopItem>());
        if (AvailableItems.Count == 0) Debug.LogError("Can't find any shopitems!");
        ActiveShoppers = new List<ShopperBehaviour>(FindObjectsOfType<ShopperBehaviour>());
        QueuedShoppers = new Queue<ShopperBehaviour>();
        AvailableItemsByType = new List<List<ShopItem>>(); //list for each type of shopitem

        for(int i = 0; i <= (int)ShopItemTypes.GetMax(); ++i)
        {
            AvailableItemsByType.Add(new List<ShopItem>(AvailableItems.Where(x => x.ShopItemType == (ShopItemTypes.SHOPITEMTYPE)i))); //creates a list of shop items for each shop item type and adds them to availableitemsbytype
            //this means that we can check a type of item with shopitemtype enum e.g. AvailableItemsByType[(int)ShopItemType.SHIRTB].Count;
            Debug.Log(((ShopItemTypes.SHOPITEMTYPE)i).ToString() + " Count:" + AvailableItemsByType[i].Count); 
        }
        
    }

    public ShopItem GetRandomItem()
    {
        return AvailableItems[Random.Range(0, AvailableItems.Count)];
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

    public int ItemsAvailableCount(ShopItemTypes.SHOPITEMTYPE s)
    {
        return AvailableItemsByType[(int)s].Count;
    }

}
