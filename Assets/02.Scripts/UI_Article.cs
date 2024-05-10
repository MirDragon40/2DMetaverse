using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Article 데이터를 보여주는 게임 오브젝트
public class UI_Article : MonoBehaviour
{
    public Image ProfilePictureUI;            // 프로필 이미지
    public TextMeshProUGUI NameTextUI;        // 글쓴이
    public TextMeshProUGUI ContentTextUI;     // 글 내용
    public TextMeshProUGUI LikeTextUI;        // 좋아요 개수
    public TextMeshProUGUI WriteTimeUI;       // 글 쓴 날짜/시간



    public void Init(Article article)
    {
        NameTextUI.text = article.Name;
        ContentTextUI.text = article.Content;
        LikeTextUI.text = $"좋아요 {article.Like}";
        WriteTimeUI.text = GetTimeString(article.WriteTime.ToLocalTime());
    }

    private string GetTimeString (DateTime dateTime)
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
        else if (timeSpan.TotalDays  < 1)
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
}
