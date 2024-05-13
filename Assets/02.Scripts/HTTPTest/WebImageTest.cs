using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebImageTest : MonoBehaviour
{
    public RawImage ImageUI;

    // HTTP: 웹에서 요청(Request)과 응답(Response)을 하기 위한 약속된 형태의 텍스트
    // 웹(WEB): 거미줄이라는 뜻으로 현재는 '인터넷'을 의미

    private void Start()
    { 
        // 네트워크를 통해 데이터를 받아오는 것은 실시간이 아니기 때문에
        // 코루틴을 이용해서 비동기로 데이터를 받아온다. 
        // 작업을 처리하는 데에 시간이 걸릴때 비동기를 이용한다.
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        // Http 주문을 위해 주문서(Request)를 만든다.
        // -> 주문서 내용: URL로부터 텍스처(이미지)를 다운로드하기 위한 GET Request 요청
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://flexible.img.hani.co.kr/flexible/normal/970/777/imgdb/resize/2019/0926/00501881_20190926.JPG");
        yield return www.SendWebRequest();  // 비동기가 일어나는 구간

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            ImageUI.texture = myTexture;
        }
    }
}
