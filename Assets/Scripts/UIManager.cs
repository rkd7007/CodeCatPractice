using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("# Shop UI")]
    public GameObject RubyShop_UI;
    public GameObject PakageShop_UI;

    [Header("# Product UI")]
    public GameObject RubyShopProduct_UI;
    public GameObject PakageShopProduct_UI;
    public GameObject[] PakageShopProducts;

    [Header("# Shop Button")]
    public GameObject[] ShopButton;

    void Awake()
    {
        if (instance != null) //�̹� �����ϸ�
        {
            Destroy(gameObject); //�ΰ� �̻��̴� ����
            return;
        }
        instance = this; //�ڽ��� �ν��Ͻ���
        DontDestroyOnLoad(gameObject); //�� �̵��ص� ��������ʰ�

        //Ruby
        RubyShop_UI.SetActive(true);
        RubyShopProduct_UI.SetActive(true);
        //Pakage
        PakageShop_UI.SetActive(false);
        PakageShopProduct_UI.SetActive(false);
    }

    public void ClickPackageShop()
    {
        //Ruby
        RubyShop_UI.SetActive(false);
        RubyShopProduct_UI.SetActive(false);
        //Pakage
        PakageShop_UI.SetActive(true);
        PakageShopProduct_UI.SetActive(true);
        //Pakage ù ��° â�� Ȱ��ȭ, ������ ��Ȱ��ȭ
        PakageShopProducts[0].SetActive(true);
        for (int i = 1; i < PakageShopProducts.Length; i++)
        {
            PakageShopProducts[i].SetActive(false);
        }
    }

    public void ClickBack()
    {
        //Ruby
        RubyShop_UI.SetActive(true);
        RubyShopProduct_UI.SetActive(true);
        //Pakage
        PakageShop_UI.SetActive(false);
        PakageShopProduct_UI.SetActive(false);
    }

    public void ClickShopButton(GameObject obj)
    {
        foreach (GameObject it in ShopButton)
        {
            if (it == obj)
            {
                obj.GetComponent<ShopUI>().ProductUIname.SetActive(true);
            }
            else
            {
                it.GetComponent<ShopUI>().ProductUIname.SetActive(false);
            }
        }
    }

    public void ClickProduct()
    {
        
    }
}
