using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

// 1. 하나만을 보장 (싱글톤 사용)
// 2. 어디서든 쉽게 접근 가능
public class ArticleManager : MonoBehaviour
{
    private List<Article> _articles = new List<Article>();

    public List<Article> Articles => _articles;

    public static ArticleManager Instance { get; private set; }

    // 콜렉션
    private IMongoCollection<BsonDocument> _articleCollection;



    private void Awake()
    {

        Instance = this;
        Init();
        FindAll();

    }

    // 몽고 
    private void Init()
    {
        // 몽고 DB로부터 article 조회
        // 1. 몽고DB 연결
        string connectionString = "mongodb+srv://Subin:SoobSoob@cluster0.xjolm4a.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        // 2. 특정 데이터베이스 연결
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        // 3. 특정 콜렉션 연결
        _articleCollection = sampleDB.GetCollection<BsonDocument>("articles");

    }

    public void FindAll()
    {
        // 4. 모든 문서 읽어오기
        var dataList = _articleCollection.Find(new BsonDocument()).ToList();
        // 5. 읽어온 문서 만큼 New Article()해서 데이터 채우고 
        _articles.Clear();
        foreach (var data in dataList)
        {
            Article article = new Article();
            article.Name = data["Name"].ToString();
            article.Content = data["Content"].ToString();
            article.Like = (int)data["Like"];
            article.ArticleType = (ArticleType)(int)data["ArticleType"];


            article.WriteTime = DateTime.Parse(data["WriteTime"].ToString());
            //    _articles에 넣기
            _articles.Add(article);
        }
    }

    public void FindNotice()
    {
        // 4. 공지 문서 읽어오기
        var dataList = _articleCollection.Find(data => data["ArticleType"] == (int)ArticleType.Notice).ToList();
        // 5. 읽어온 문서 만큼 New Article()해서 데이터 채우고 
        _articles.Clear();
        foreach (var data in dataList)
        {
            Article article = new Article();
            article.Name = data["Name"].ToString();
            article.Content = data["Content"].ToString();
            article.Like = (int)data["Like"];
            article.ArticleType = (ArticleType)(int)data["ArticleType"];
            article.WriteTime = DateTime.Parse(data["WriteTime"].ToString());
            //    _articles에 넣기
            _articles.Add(article);
        }
    }

   
}
