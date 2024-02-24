using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductList : MonoBehaviour
{
    [SerializeField]
    GameObject[] ProductPrefab;

    [SerializeField]
    ShopList shopList = new ShopList();

    void Awake()
    {
        //Resources 파일 속 Json 파일을 TextAsset 객체로 가져온다
        TextAsset loadedJson = Resources.Load<TextAsset>("Json/ShopData");
        //불러온 객체를 파싱
        shopList = JsonUtility.FromJson<ShopList>(loadedJson.text);
        //데이터 넣기
        Init();
    }

    private void Init()
    {
        int Shopcount = 0;
        int Prefabcount = 0;
        foreach (ShopBase it in shopList.Shop)
        {
            for (int j = 0; j < it.Products.Count; j++)
            {
                ProductPrefab[Prefabcount].transform.GetChild(0).SendMessage("SetText", it.Products[j].Name);
                ProductPrefab[Prefabcount].transform.GetChild(1).SendMessage("SetText", it.Products[j].Detail);
                ProductPrefab[Prefabcount].transform.GetChild(2).transform.GetChild(0).SendMessage("SetText", it.Products[j].Price.ToString());

                Prefabcount++;
            }
            Shopcount++;
        }
    }
}
