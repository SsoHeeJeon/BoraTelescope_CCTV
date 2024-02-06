using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MapPos
{
    public string PosName;
    public string Lati;
    public string Longi;

    public MapPos(string name, string x, string y)
    {
        PosName = name;
        Lati = x;
        Longi = y;
    }
}

public class ConnectAPI : MonoBehaviour
{
    public static Dictionary<string, MapPos> mp = new Dictionary<string, MapPos>();
    public RawImage mapImage;

    //private string strBaseURL = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster?";
    private string strBaseURL = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster?crs=EPSG:4326&";
    public string Latitude = "";    // 위도
    public string Longitude = "";   // 경도
    public int level = 14;          // 지도의 확대축소
    private int mapWidth = 300;
    private int mapHeight = 300;
    public static string strAPIKey = "kz29vqi08d";
    public static string secretKey = "wwMBHagLylKub6JDULjGx7Vg169a1eaKwFTOnVCC";

    // Start is called before the first frame update
    void Start()
    {
        //SelectPos("street");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectPos(string pos)
    {
        foreach(KeyValuePair<string,MapPos> item in mp)
        {
            if(item.Value.PosName == pos)
            {
                Longitude = item.Value.Longi;
                Latitude = item.Value.Lati;
            }

            Debug.Log(pos + " / " + Longitude + " / " + Latitude);
        }

        StartCoroutine(SeeMap());
    }

    IEnumerator SeeMap()
    {
        //string str = strBaseURL + "w=" + mapWidth.ToString() + "&h=" + mapHeight.ToString() +
        //             "&center=" + Longitude + "," + Latitude + "&level=" + level.ToString();
        string str = strBaseURL + "w=" + mapWidth.ToString() + "&h=" + mapHeight.ToString() + "&markers=type:d|size:mid|pos:"
                     + Longitude + "%20" + Latitude + "|viewSizeRatio:0.5";

        switch (GameManager.currentLang)
        {
            case GameManager.Language_enum.Korea:
                str += "&lang=ko";
                break;
            case GameManager.Language_enum.English:
                str += "&lang=en";
                break;
            case GameManager.Language_enum.Chinese:
                str += "&lang=zh";
                break;
            case GameManager.Language_enum.Japanese:
                str += "&lang=ja";
                break;
        }
        Debug.Log(str.ToString());
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        } else
        {
            print("Success!!!!!!");
            mapImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }
}
