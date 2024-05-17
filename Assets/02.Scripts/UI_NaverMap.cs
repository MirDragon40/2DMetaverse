using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class MapManager : MonoBehaviour
{
    public RawImage MapRawImage;
    public TMP_InputField PlaceInputField;
    public Button SearchButton;

    [Header("맵 정보 입력")]
    public string strBaseURL = "";
    public string latitude = "";
    public string longitude = "";
    public int level = 17;
    public int mapWidth;
    public int mapHeight;
    private string strAPIKey = "l7djsvzd7v"; // 네이버 클라이언트 ID
    private string secretKey = "ybEkqHZgQprBfGdFOehlPoE9fV6PL4ILWFSc6wbN"; //
    private string baseUrl = "https://naveropenapi.apigw.ntruss.com/map-geocode/v2/geocode";

    // 네이버 클라이언트 시크릿


    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void OnClickSearchButton()
    {
        GetCoordinatesFromAddress(PlaceInputField.text);
    }

    public void GetCoordinatesFromAddress(string address)
    {
        StartCoroutine(SendGeocodeRequest(address));
    }
    IEnumerator SendGeocodeRequest(string address)
    {
        string url = $"{baseUrl}?query={UnityWebRequest.EscapeURL(address)}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            ProcessGeocodeResponse(request.downloadHandler.text);
        }
    }
    void ProcessGeocodeResponse(string jsonResponse)
    {
        var geocodeResponse = JsonUtility.FromJson<GeocodeResponse>(jsonResponse);
        if (geocodeResponse.addresses != null && geocodeResponse.addresses.Length > 0)
        {
            string latitude = geocodeResponse.addresses[0].y;
            string longitude = geocodeResponse.addresses[0].x;
            Debug.Log($"Latitude: {latitude}, Longitude: {longitude}");

            StartCoroutine(MapLoader(latitude, longitude));
        }
        else
        {
            Debug.LogError("No addresses found in geocode response");
        }
    }

    IEnumerator MapLoader(string latitude, string longitude)
    {
        string str = strBaseURL + "?w=" + mapWidth.ToString() + "&h=" + mapHeight.ToString() + "&center=" + longitude + "," + latitude + "&level=" + level.ToString();


        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            MapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }

    [System.Serializable]
    public class GeocodeResponse
    {
        public Address[] addresses;
    }

    [System.Serializable]
    public class Address
    {
        public string roadAddress;
        public string jibunAddress;
        public string englishAddress;
        public AddressElement[] addressElements;
        public string x;
        public string y;
        public float distance;
    }

    [System.Serializable]
    public class AddressElement
    {
        public string[] types;
        public string longName;
        public string shortName;
    }

}



