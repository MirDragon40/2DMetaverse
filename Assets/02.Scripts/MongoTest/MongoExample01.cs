using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using System.Linq;

public class MongoExample01 : MonoBehaviour
{

    private void Start()
    {
        // var: 암시적 타입으로써 데이터의 자료형을 자동으로 설정하는 키워드
        // r-value로부터 자동으로 타입을 유추
        // 장점: 간단하다.
        // 단점: 자료형이 명확하지 않아서 휴먼에러가 날 수 있다. 되도록이면 사용하지 말기
        // 언제쓰면 좋은가?: 1. 자료형이 너무 길 경우, 지역변수에만 사용
        //                   2. foreach 반복문에서 명확할 경우
        // e.g
        List<Article> articles = new List<Article>();
        foreach (var article in articles)
        {

        }

        int age;
        age = 3;

        // var number;   => var를 사용할 때엔 변수 선언만 해주면 안됨. 선언과 동시에 초기값을 넣어 주어야 함.
        var number = 3;
        var number2 = 2.3f;
        var myName = "티모";

        // 몽고 데이터베이스에 연결
        // 연결 문자열: 데이터베이스 연결을 위한 정보가 담겨있는 문자열
        string connectionString = "mongodb+srv://Subin:SoobSoob@cluster0.xjolm4a.mongodb.net/";
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
            Debug.Log(db["name"]);
        }

        // 3. 특정 데이터베이스에 연결
        IMongoDatabase sampleDB =  mongoClient.GetDatabase("sample_mflix");

        // 4. 콜렉션들 이름 확인 
        List<string> collectionNames = sampleDB.ListCollectionNames().ToList();
        foreach(string name in collectionNames)
        {
            Debug.Log(name);
        }

        // 5. 콜렉션 연결 및 도큐먼트 개수 출력
        var movieCollection = sampleDB.GetCollection<BsonDocument>("movies");
        long count = movieCollection.CountDocuments(new BsonDocument());
        Debug.Log($"영화 개수: {count}");

        // 6. 도큐먼트 하나만 읽기
        BsonDocument firstDoc = movieCollection.Find(new BsonDocument()).FirstOrDefault();
        Debug.Log(firstDoc["plot"]);
    }
}
