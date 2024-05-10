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



    }
}
