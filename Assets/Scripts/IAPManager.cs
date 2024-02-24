using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    [Header("Product Type")]
    public const string ProductRuby = "ruby"; //for Consumable
    public const string Productlimited = "package_limited"; //for NonConsumable
    public const string ProductSubscription = "package_Grow"; //for ubscription

    [Header("Product ID")]
    private const string ruby540Id = "com.areum.shop.ruby540";
    private const string ruby920Id = "com.areum.shop.ruby920";
    private const string ruby1900Id = "com.areum.shop.ruby1900";
    private const string limited4400Id = "com.areum.shop.limited4400";
    private const string limited5500Id = "com.areum.shop.limited5500";
    private const string limited39000Id = "com.areum.shop.limited39000";
    private const string limited110000Id = "com.areum.shop.limited110000";
    private const string Grow39000Id = "com.areum.shop. Grow39000";

    [Header("Cache")]
    private IStoreController storeController; //구매 과정을 제어하는 함수 제공
    private IExtensionProvider storeExtensionProvider; //여러 플랫폼을 위한 확장 처리 제공

    /* IAP 초기화를 한 번만 하도록 함 */
    public bool IsInitialized => storeController != null && storeExtensionProvider != null;
    /* 아래 함수식과 결과는 같다
    {
        get
        {
            if (storeController != null && storeExtensionProvider != null)
                return true;
            return false;
        }
    }
    */


    //싱글톤
    private static IAPManager instance;
    public static IAPManager Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = FindObjectOfType<IAPManager>();
            if (instance == null)
                instance = new GameObject("IAPManager").AddComponent<IAPManager>();
            return instance;
        }
    }

    void Awake()
    {
        //이미 존재하거나 자기 자신을 가르키는게 아니라면
        if (instance != null && instance != this)
        {
            Destroy(gameObject); //삭제
            return;
        }
        DontDestroyOnLoad(gameObject); //씬 이동해도 사라지지않게

        InitUnityIAP(); //IAP 초기화
    }

    /* Unity IAP를 초기화하는 함수 */
    private void InitUnityIAP()
    {
        //이미 초기화 했으면 돌아간다
        if (IsInitialized) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        /* 구글 플레이 상품들 추가 */
        builder.AddProduct(ruby540Id, ProductType.Consumable, new IDs() {
            { ruby540Id, GooglePlay.Name } });
        builder.AddProduct(ruby920Id, ProductType.Consumable, new IDs() {
            { ruby920Id, GooglePlay.Name } });
        builder.AddProduct(ruby1900Id, ProductType.Consumable, new IDs() {
            { ruby1900Id, GooglePlay.Name } });
        builder.AddProduct(limited4400Id, ProductType.NonConsumable, new IDs() {
            { limited4400Id, GooglePlay.Name } });
        builder.AddProduct(limited5500Id, ProductType.NonConsumable, new IDs() {
            { limited5500Id, GooglePlay.Name } });
        builder.AddProduct(limited39000Id, ProductType.NonConsumable, new IDs() {
            { limited39000Id, GooglePlay.Name } });
        builder.AddProduct(limited110000Id, ProductType.NonConsumable, new IDs() {
            { limited110000Id, GooglePlay.Name } });
        builder.AddProduct(Grow39000Id, ProductType.Subscription, new IDs() {
            { Grow39000Id, GooglePlay.Name } });

        UnityPurchasing.Initialize(this, builder);
    }

    /* 초기화 성공 시 실행되는 함수 */
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        Debug.Log("유니티 IAP 초기화 성공");
        storeController = controller;
        storeExtensionProvider = extension;
    }

    /* 초기화 실패 시 실행되는 함수 */
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"유니티 IAP 초기화 실패 {error}");
    }
    public void OnInitializeFailed(InitializationFailureReason error, string reason)
    {
        Debug.LogError($"유니티 IAP 초기화 실패 {error} 초기화 실패 이유 - {reason}");
    }

    /* 제품 구매 */
    public void Purchase(string productId)
    {
        //초기화 안됐으면 돌아간다
        if (!IsInitialized) return;

        var product = storeController.products.WithID(productId);

        //상품이 존재하면서 구매 가능하면
        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"구매 시도 - {product.definition.id}");
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log($"구매 시도 불가 - {productId}");
        }
    }

    /* 구매 성공 */
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"구매 성공 - ID : {args.purchasedProduct.definition.id}");

        if (args.purchasedProduct.definition.id == ruby540Id)
        {
            Debug.Log("루비 540개 구매 완료");
        }
        else if (args.purchasedProduct.definition.id == ruby920Id)
        {
            Debug.Log("루비 920개 구매 완료");
        }
        else if (args.purchasedProduct.definition.id == ruby1900Id)
        {
            Debug.Log("루비 1900개 구매 완료");
        }
        else if (args.purchasedProduct.definition.id == limited4400Id)
        {
            Debug.Log("한정 패키지 스타터 팩 구매 완료");
        }
        else if (args.purchasedProduct.definition.id == limited5500Id)
        {
            Debug.Log("한정 패키지 아이리 팩 구매 완료");
        }
        else if (args.purchasedProduct.definition.id == limited39000Id)
        {
            Debug.Log("한정 패키지 미아 팩 구매 완료");
        }
        else if (args.purchasedProduct.definition.id == limited110000Id)
        {
            Debug.Log("한정 패키지 릴리스 팩 구매 완료");
        }
        else if (args.purchasedProduct.definition.id == Grow39000Id)
        {
            Debug.Log("성장 패키지 성장 지원 팩 구매 완료");
        }

        return PurchaseProcessingResult.Complete;
    }

    /* 구매 실패 */
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning($"구매 실패 - {product.definition.id}, {reason}");
    }

    /* 구매한 제품인지 아닌지 */
    public bool HadPurchased(string productId)
    {
        if (!IsInitialized) return false;

        var product = storeController.products.WithID(productId);
        if (product != null)
        {
            //영수증이 존재하느냐
            return product.hasReceipt;
        }
        return false;
    }
}
