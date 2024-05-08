using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class MongoExample01 : MonoBehaviour
{

    private void Start()
    {
        // 몽고 데이터베이스에 연결
        // 연결 문자열: 데이터베이스 연결을 위한 정보가 담겨있는 문자열
        string connectionString = "mongodb + srv://Subin:SoobSoob@cluster0.xjolm4a.mongodb.net/";
        // - 프로토콜: mongodb + srv
        // - 아이디: Subin
        // - 비밀번호: SoobSoob
        // - 주소: cluster0.ixxxv9w.mongodb.net

        // 1. 접속
        MongoClient mongoClient = new MongoClient(connectionString);

        // 2. 데이터베이스가 무엇무엇이 있는지?
        List<BsonDocument> dbList = mongoClient.ListDatabases().ToList();
        foreach (BsonDocument db in dbList)
        {
            Debug.Log(db["Name"]);
        }
    }
}
