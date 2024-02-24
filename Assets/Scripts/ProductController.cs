using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ProductController : MonoBehaviour
{
    public string targetProductType;
    public string targetProductId;

    public void HandleClick()
    {
        if (targetProductType == IAPManager.Productlimited ||
            targetProductType == IAPManager.ProductSubscription)
        {
            if (IAPManager.Instance.HadPurchased(targetProductId))
            {
                Debug.Log("�̹� ������ ��ǰ");
                return;
            }
        }
        IAPManager.Instance.Purchase(targetProductId);
    }
}
