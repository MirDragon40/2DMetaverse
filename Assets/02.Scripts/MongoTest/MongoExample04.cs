using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongoExample04 : MonoBehaviour
{
    private void Start()
    {
        string connectionString = "mongodb+srv://Subin:SoobSoob@cluster0.xjolm4a.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase sampleDB = mongoClient.GetDatabase("metaverse");
        var collection = sampleDB.GetCollection<Article>("articles");

        // 도큐먼트 수정
        // 1. Update : 특정 도큐먼트의 Value를 수정
        // 2. Replace: 특정 도큐먼트의 Id를 제외하고 전부 대체

        // 1-1. UpdateOne(하나 수정), UpdateMany(두개 수정)
        // UpdateOne(filter, updateDefinition)
        // filter: 업데이트 할 도큐먼트의 기준
        // UpdateDefinitio: 도큐먼트에 적용할 변동사항
        var updateDefinition = Builders<Article>.Update.Set("Name", "정수빈");
        UpdateResult result =  collection.UpdateMany(data => data.Name == "티모", updateDefinition);
        Debug.Log($"수정된 문서 개수: {result.ModifiedCount}");


        // 2-1. ReplaceOne, ReplaceMany
        // 도큐먼트 교체
        // Replace는 전에있는 글을 불러와서 교체할때 쓰인다. 
        Article article = collection.Find(d => d.Name == "정수빈").First();
        article.Content = "나는 뭐지";
        var result2 = collection.ReplaceOne(d => d.MyID == article.MyID, article);
        Debug.Log(result2.ModifiedCount);

        // 데이터의 기준은 항상 Id로 세워야 한다. 
        // Id를 가져와서 넣어주어야 한다. 
        //ObjectId Id = new ObjectId("663d7b330e4a155f6fcdefe0");
        //var result2 = collection.ReplaceOne(data => data.Name == "정수빈", article);

        // 수정된 문서의 개수
        //Debug.Log(result2.ModifiedCount); 



        // 도큐먼트 삭제
        // DeleteOne, DeleteMany
        // 필터 - 삭제할 문서의 기준
        var deleteResult = collection.DeleteOne(d => d.Name == "공지수");
        Debug.Log($"{deleteResult.DeletedCount}개의 문서가 삭제됐습니다.");

        collection.DeleteOne(d => d.MyID == article.MyID);

    }

    
}
