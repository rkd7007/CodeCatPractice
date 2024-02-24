using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; /* for Serializable */

[Serializable]

class ShopBase
{
    public string CategoryName;
    public List<ProductData> Products;
}

[Serializable]
class ProductData
{
    public string Name;
    public int Price;
    public string Detail;
}

[Serializable]
class ShopList
{
    public List<ShopBase> Shop;
}
