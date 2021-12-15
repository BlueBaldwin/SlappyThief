using System;
using System.Linq;
using System.Collections.Generic;
public static class ShopItemTypes
{
    public enum SHOPITEMTYPE
    {
        UNDEFINED = -1,
        SHIRTBLACK = 0,
        SHIRTRED = 1,
        SHIRTGREEN = 2,
        SHIRTWHITE = 3,
        SHIRTPURPLE = 4,
        SHIRTBLUE = 5,
    }

    public static SHOPITEMTYPE GetMax()
    {
        return  new List<SHOPITEMTYPE>((SHOPITEMTYPE[])Enum.GetValues(typeof(SHOPITEMTYPE))).Max();
    }

}


