using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 1. 하나만을 보장 (싱글톤 사용)
// 2. 어디서든 쉽게 접근 가능
public class ArticleManager : MonoBehaviour
{
    private List<Article> _articles = new List<Article>();

    public List<Article> Articles  => _articles;

    public static ArticleManager Instance { get; private set; }

    private void Awake()
    {
        
            Instance = this;
        
       

        

    }

    public void OnNotification()
    {
        
    }

    private void Refresh()
    {
        // 몽고 DB로부터 article 조회
        // 1. 몽고DB 연결
        string connectionString = "mongodb+srv://Subin:SoobSoob@cluster0.xjolm4a.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        // 2. 특정 데이터베이스 연결
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        // 3. 특정 콜렉션 연결
        var articlesCollection = sampleDB.GetCollection<BsonDocument>("articles");
        // 4. 모든 문서 읽어오기
        int count = (int)articlesCollection.CountDocuments(new BsonDocument());
        var articlesAll = articlesCollection.Find(new BsonDocument()).Limit(count).ToList();

        

        // 5. 읽어온 문서 만큼 New Articles()해서 데이터 채우고
        //    _articles에 넣기
        foreach (var eachArticle in articlesAll)
        {
            Article article = new Article();
            article.Name = eachArticle["Name"].ToString();
            article.Content = eachArticle["Content"].ToString();
            article.Like = (int)eachArticle["Like"];

            string writeTime = eachArticle["WriteTime"].ToString();
            article.WriteTime = DateTime.Parse(writeTime);


            _articles.Add(article);
        }
    }
}
