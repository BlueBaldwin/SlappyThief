using System;
using System.Linq;
using System.Collections.Generic;
public static class ShopItemTypes
{
    public enum SHOPITEMTYPE
    {
        UNDEFINED = -1,
        SHIRTBLACK = 0,
        SHIRTRED,
        SHIRTGREEN,
        SHIRTWHITE,

        SHIRTPURPLE,
        SHIRTBLUE,

        TROUSERBLUE,
        TROUSERGREEN,
        TROUSERBROWN,
        TROUSERBLACK,
    }

    public static SHOPITEMTYPE GetMax()
    {
        return  new List<SHOPITEMTYPE>((SHOPITEMTYPE[])Enum.GetValues(typeof(SHOPITEMTYPE))).Max();
    }

}


