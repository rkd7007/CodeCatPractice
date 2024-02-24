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
    private IStoreController storeController; //���� ������ �����ϴ� �Լ� ����
    private IExtensionProvider storeExtensionProvider; //���� �÷����� ���� Ȯ�� ó�� ����

    /* IAP �ʱ�ȭ�� �� ���� �ϵ��� �� */
    public bool IsInitialized => storeController != null && storeExtensionProvider != null;
    /* �Ʒ� �Լ��İ� ����� ����
    {
        get
        {
            if (storeController != null && storeExtensionProvider != null)
                return true;
            return false;
        }
    }
    */


    //�̱���
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
        //�̹� �����ϰų� �ڱ� �ڽ��� ����Ű�°� �ƴ϶��
        if (instance != null && instance != this)
        {
            Destroy(gameObject); //����
            return;
        }
        DontDestroyOnLoad(gameObject); //�� �̵��ص� ��������ʰ�

        InitUnityIAP(); //IAP �ʱ�ȭ
    }

    /* Unity IAP�� �ʱ�ȭ�ϴ� �Լ� */
    private void InitUnityIAP()
    {
        //�̹� �ʱ�ȭ ������ ���ư���
        if (IsInitialized) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        /* ���� �÷��� ��ǰ�� �߰� */
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

    /* �ʱ�ȭ ���� �� ����Ǵ� �Լ� */
    public void OnInitialized(IStoreController controller, IExtensionProvider extension)
    {
        Debug.Log("����Ƽ IAP �ʱ�ȭ ����");
        storeController = controller;
        storeExtensionProvider = extension;
    }

    /* �ʱ�ȭ ���� �� ����Ǵ� �Լ� */
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"����Ƽ IAP �ʱ�ȭ ���� {error}");
    }
    public void OnInitializeFailed(InitializationFailureReason error, string reason)
    {
        Debug.LogError($"����Ƽ IAP �ʱ�ȭ ���� {error} �ʱ�ȭ ���� ���� - {reason}");
    }

    /* ��ǰ ���� */
    public void Purchase(string productId)
    {
        //�ʱ�ȭ �ȵ����� ���ư���
        if (!IsInitialized) return;

        var product = storeController.products.WithID(productId);

        //��ǰ�� �����ϸ鼭 ���� �����ϸ�
        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"���� �õ� - {product.definition.id}");
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.Log($"���� �õ� �Ұ� - {productId}");
        }
    }

    /* ���� ���� */
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"���� ���� - ID : {args.purchasedProduct.definition.id}");

        if (args.purchasedProduct.definition.id == ruby540Id)
        {
            Debug.Log("��� 540�� ���� �Ϸ�");
        }
        else if (args.purchasedProduct.definition.id == ruby920Id)
        {
            Debug.Log("��� 920�� ���� �Ϸ�");
        }
        else if (args.purchasedProduct.definition.id == ruby1900Id)
        {
            Debug.Log("��� 1900�� ���� �Ϸ�");
        }
        else if (args.purchasedProduct.definition.id == limited4400Id)
        {
            Debug.Log("���� ��Ű�� ��Ÿ�� �� ���� �Ϸ�");
        }
        else if (args.purchasedProduct.definition.id == limited5500Id)
        {
            Debug.Log("���� ��Ű�� ���̸� �� ���� �Ϸ�");
        }
        else if (args.purchasedProduct.definition.id == limited39000Id)
        {
            Debug.Log("���� ��Ű�� �̾� �� ���� �Ϸ�");
        }
        else if (args.purchasedProduct.definition.id == limited110000Id)
        {
            Debug.Log("���� ��Ű�� ������ �� ���� �Ϸ�");
        }
        else if (args.purchasedProduct.definition.id == Grow39000Id)
        {
            Debug.Log("���� ��Ű�� ���� ���� �� ���� �Ϸ�");
        }

        return PurchaseProcessingResult.Complete;
    }

    /* ���� ���� */
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning($"���� ���� - {product.definition.id}, {reason}");
    }

    /* ������ ��ǰ���� �ƴ��� */
    public bool HadPurchased(string productId)
    {
        if (!IsInitialized) return false;

        var product = storeController.products.WithID(productId);
        if (product != null)
        {
            //�������� �����ϴ���
            return product.hasReceipt;
        }
        return false;
    }
}
