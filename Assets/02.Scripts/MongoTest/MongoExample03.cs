using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongoExample03 : MonoBehaviour
{
    // 도큐먼트 삽입 (Create)
    private void Start()
    {
        string connectionString = "mongodb+srv://Subin:SoobSoob@cluster0.xjolm4a.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        var collection = sampleDB.GetCollection<Article>("articles");

        // 1. 도큐먼트 하나 삽입
        // InserteOne(도큐먼트)
        Article article = new Article
        {
            Name = "정수빈",
            Content = "공지사항입니다.",
            ArticleType = ArticleType.Notice,
            Like = 100,
            WriteTime = DateTime.Now
        };
        Debug.Log(article.MyID);  // 0000000000000000000
        collection.InsertOne(article);
        Debug.Log(article.MyID);


        // 2. 도큐먼트 여러개 삽입
        // InsertMany(List<도큐먼트>)
        List<Article> articles = new List<Article>()
        {
           new Article()
            {
                Name = "티모",
                Content =  "민성씨 빨리와요.",
                ArticleType =  ArticleType.Normal,
                Like = 11,
                WriteTime = new DateTime(2023, 12, 24)
            },
            new Article()
            {
                Name = "말파이트",
                Content =  "성민씨 휴가 왜 쓰셨나요.",
                ArticleType =  ArticleType.Normal,
                Like = 130,
                WriteTime =  DateTime.Now
            }
            ,new Article()
            {
                Name = "애쉬",
                Content =  "경희씨 아프지마세요.",
                ArticleType =  ArticleType.Normal,
                Like = 19700,
                WriteTime =  DateTime.Now.AddHours(-3)
            }
        };
        collection.InsertMany(articles);

    }
}
