using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BubbleSpriteManager 
{
    public static List<Sprite> ShopItemSprites;

    public static Vector3 BubbleOffset;


//these methods are called and vars are populated in GameplayManager.cs
    public static void SetShopItemSprites(List<Sprite> s){
        ShopItemSprites = s;
    }

    public static void SetBubbleOffset(Vector3 v){
        BubbleOffset = v;
    }


    
    
}
