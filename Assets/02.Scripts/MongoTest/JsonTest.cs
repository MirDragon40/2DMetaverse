using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.IO;

public class JsonTest : MonoBehaviour
{
    private void Start()
    {
        // 1. 유추해서 데이터를 담을 수 있는 클래스를 Person 만들어라

        // 2. 리소스 폴더로 부터 person.json 내용을 읽어와라.
        //TextAsset textAsset = Resources.Load<TextAsset>("person");

        /*var textAsset = Resources.Load("person") as TextAsset;
        string jsonText = textAsset.text;
        Debug.Log(jsonText);
        */

        string jsonText = File.ReadAllText($"{Application.dataPath}/Resources/person.json");


        // 3. 클래스 A의 객체를 만들고 읽어온 내용을 역직렬화 해라.
        Person loadedPerson = JsonUtility.FromJson<Person>(jsonText);

        // 4. 클래스 A의 이름, 나이, 취미들을 Debug.Log로 출력
        Debug.Log(loadedPerson.Name);
        Debug.Log(loadedPerson.Age);
        foreach (var hobby in loadedPerson.Hobby)
        {
            Debug.Log(hobby);
        }

    }
}
