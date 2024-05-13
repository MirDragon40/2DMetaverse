using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

// Article 데이터를 보여주는 게임 오브젝트
public class UI_Article : MonoBehaviour
{
    public RawImage ProfilePictureUI;            // 프로필 이미지
    public TextMeshProUGUI NameTextUI;        // 글쓴이
    public TextMeshProUGUI ContentTextUI;     // 글 내용
    public TextMeshProUGUI LikeTextUI;        // 좋아요 개수
    public TextMeshProUGUI WriteTimeUI;       // 글 쓴 날짜/시간

    public UI_ArticleMenu MenuUI;

    private Article _article;


    public Dictionary<string, Texture> cache = new Dictionary<string, Texture>();

    public void Init(Article article)
    {
        _article = article;
        NameTextUI.text = article.Name;
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"좋아요 {article.Like}";
        WriteTimeUI.text = GetTimeString(article.WriteTime.ToLocalTime());



        StartCoroutine(GetProfileTexture(article.Profile));

        if (article.Profile == null)
        {
            StartCoroutine(GetProfileTexture("http://192.168.200.104:3059/Profile.png"));
        }

    }

    private string GetTimeString(DateTime dateTime)
    {
        TimeSpan timeSpan = DateTime.Now - dateTime;

        if (timeSpan.TotalMinutes < 1)
        {
            return "방금 전";
        }
        else if (timeSpan.TotalHours < 1)
        {
            return $"{timeSpan.TotalMinutes:N0}분 전";
        }
        else if (timeSpan.TotalDays < 1)
        {
            return $"{timeSpan.TotalHours:N0}시간 전";
        }
        else if (timeSpan.TotalDays < 7)
        {
            return $"{timeSpan.TotalDays:N0}일 전";
        }
        else if (timeSpan.TotalDays < 7 * 4)
        {
            return $"{timeSpan.TotalDays / 7:N0}주 전";
        }

        return dateTime.ToString("yyyy년M월d일");
    }

    public void OnClickMenuButton()
    {
        MenuUI.Show(_article);

    }


    public void OnClickLikeButton()
    {
        // 1. 데이터 조작은 항상 매니저에게 시킨다.
        ArticleManager.Instance.AddLike(_article);

        ArticleManager.Instance.FindAll();
        UI_ArticleList.Instance.Refresh();
    }


    IEnumerator GetProfileTexture(string profileLink)
    {
        if (string.IsNullOrEmpty(profileLink))
        {
            Debug.LogError("URL이 null이거나 비어있습니다.");
            yield break; // 코루틴 종료
        }


        if (cache.ContainsKey(profileLink))
        {
            // 캐시에서 이미지를 찾았다면, RawImage에 적용
            ProfilePictureUI.texture = cache[profileLink];
        }
        else
        {
            // Http 주문을 위해 주문서(Request)를 만든다.
            // -> 주문서 내용: URL로부터 텍스처(이미지)를 다운로드하기 위한 GET Request 요청
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(profileLink);
            yield return www.SendWebRequest();  // 비동기가 일어나는 구간

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(profileLink);
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                ProfilePictureUI.texture = myTexture;

                cache[profileLink] = myTexture;
            }
        }
      
    }


    /*
    IEnumerator GetProfileTexture(string profileLink)
    {
   

        // Http 주문을 위해 주문서(Request)를 만든다.
        // -> 주문서 내용: URL로부터 텍스처(이미지)를 다운로드하기 위한 GET Request 요청
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(profileLink);
        yield return www.SendWebRequest();  // 비동기가 일어나는 구간

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(profileLink);
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            ProfilePictureUI.texture = myTexture;
        }
    }*/

}
