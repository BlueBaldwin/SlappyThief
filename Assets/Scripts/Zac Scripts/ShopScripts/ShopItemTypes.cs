using System;
using System.Linq;
using System.Collections.Generic;
public static class ShopItemTypes
{
    public enum SHOPITEMTYPE
    {
        UNDEFINED = -1,
        SHIRTBLUE = 0,
        SHIRTRED = 1,
        SHIRTGREEN = 2,

        TROUSERBLUE = 3,
        TROUSERGREEN = 4, 

        TROUSERBROWN = 5
    }

    public static SHOPITEMTYPE GetMax()
    {
        return  new List<SHOPITEMTYPE>((SHOPITEMTYPE[])Enum.GetValues(typeof(SHOPITEMTYPE))).Max();
    }

}


