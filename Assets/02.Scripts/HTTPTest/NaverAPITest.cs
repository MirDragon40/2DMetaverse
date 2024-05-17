using MongoDB.Bson.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NaverAPITest : MonoBehaviour
{
    private readonly string clientId = "MSg10yC_CUK5UyeB0jED"; // 네이버에서 받은 클라이언트 ID
    private readonly string clientSecret = "Na9GG2G7v0"; // 네이버에서 받은 클라이언트 시크릿
    private readonly string searchQuery = "기계식 키보드"; // 검색어

    void Start()
    {
        StartCoroutine(GetSearchResult());
    }

    IEnumerator GetSearchResult()
    {
        string url = $"https://openapi.naver.com/v1/search/shop.json?query={UnityWebRequest.EscapeURL(searchQuery)}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("X-Naver-Client-Id", clientId);
        www.SetRequestHeader("X-Naver-Client-Secret", clientSecret);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string jsonText = www.downloadHandler.text;
            ShopingItems shopingItems = JsonUtility.FromJson<ShopingItems>(jsonText);
            foreach (var item in shopingItems.item)
            {
                Debug.Log(item.title);
            }

        }

    }
}



[Serializable]
public class ShopingItems
{
    public DateTime lastBuildDate; //"Tue, 14 May 2024 10:49:25 +0900",
    public int total; // 1088776,
    public int start; // 1,
    public int display; // 10,

    public List<Item> item;
}

[Serializable]
public class Item
{
    public string title;  //  "웨이코스 씽크웨이 X VGN TV99 유무선 99배열 <b>기계식 키보드</b>",
    public string link;   //  "https://search.shopping.naver.com/gate.nhn?id=44775642618",
    public string image;  //  "https://shopping-phinf.pstatic.net/main_4477564/44775642618.20240122155827.jpg",
    public int lprice;  // 139000",
    public int hprice;
    public string mailName; //  "네이버",
    public long productId;  // "44775642618",
    public int productType; // "1",
    public string brand; // "씽크웨이",
    public string maker; // "웨이코스",
    public string category1; //  "디지털/가전",
    public string category2; // "주변기기",
    public string category3; //  "키보드",
    public string category4; //  "무선키보드"
}