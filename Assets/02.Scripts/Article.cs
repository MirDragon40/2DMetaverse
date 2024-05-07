using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArticleType
{
    Normal,
    Notice
}

// 게시글을 나타내는 데이터 클래스
public class Article    // Quest, Item, Achievement, Attendance
{
    public ArticleType ArticleType;   // 일반글? 공지사항?
    public string Name;        // 글쓴이
    public string Content;     // 글 내용
    public int Like;           // 좋아요 개수
    public DateTime WriteTime; // 글 쓴 날짜/시간

}
