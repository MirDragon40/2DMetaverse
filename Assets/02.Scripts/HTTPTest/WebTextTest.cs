using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebTextTest : MonoBehaviour
{
    public Text TextUI;

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://time.jsontest.com/");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
            string text = www.downloadHandler.text;
            TextUI.text = text;
            WebTime webTime = JsonUtility.FromJson<WebTime>(text);
            Debug.Log(webTime.date);
            Debug.Log(webTime.milliSeconds_since_epoch);
            Debug.Log(webTime.time);
        }
    }

}

[Serializable]
public class WebTime
{
    public string date;
    public long milliSeconds_since_epoch;
    public string time;


}