using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MVC 아키텍처 패턴
// - Model      (데이터와 그 데이터를 다루는 로직)    -> Article
// - View       (UI, 사용자 인터페이스)               -> UI_Article, UI_ArticleList
// - Controller (관리자, Model <-> View 중재자)       -> ArticleManager
// 위 요소들(데이터, 시각적, 관리)의 간섭없이 독립적으로 개발할 수 있다. 


public enum ArticleType
{
    Normal,
    Notice
}

// 게시글을 나타내는 데이터 클래스


[Serializable]
[BsonIgnoreExtraElements]
public class Article // Quest, Item, Achievement, Attendance 
{
    [BsonId]
    public ObjectId MyID;           // 유일한 주민번호: Id, _id, Id (시간 + 기기ID + 프로세스ID + count)
    public ArticleType ArticleType; // 일반글? 공지사항글이냐?
    //[BsonElement("Name")]
    public string Name;             // 글쓴이
    public string Content;          // 글 내용
    public int Like;                // 좋아요 개수
    public DateTime WriteTime;      // 글 쓴 날짜/시간
    [BsonDefaultValue("http://192.168.200.104:3059/Profile.png")]   // null검사 말고 다른 방식
    public string Profile;          // 프로필 이미지 주소
}

[Serializable]
public class ArticleData
{
    public List<Article> Data;

    public ArticleData(List<Article> data)
    {
        Data = data;
    }
}