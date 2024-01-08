using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DirectionManager : MonoBehaviour
{
    public class Goal
    {
        public List<double> location { get; set; }
        public int dir { get; set; }
    }

    public class Guide
    {
        public int pointIndex { get; set; }
        public int type { get; set; }
        public string instructions { get; set; }
        public int distance { get; set; }
        public int duration { get; set; }
    }

    public class Root
    {
        public int code { get; set; }
        public string message { get; set; }
        public DateTime currentDateTime { get; set; }
        public Route route { get; set; }
    }

    public class Route
    {
        public List<Trafast> trafast { get; set; }
    }

    public class Section
    {
        public int pointIndex { get; set; }
        public int pointCount { get; set; }
        public int distance { get; set; }
        public string name { get; set; }
        public int congestion { get; set; }
        public int speed { get; set; }
    }

    public class Start1
    {
        public List<double> location { get; set; }
    }

    public class Summary
    {
        public Start1 start { get; set; }
        public Goal goal { get; set; }
        public int distance { get; set; }
        public int duration { get; set; }
        public List<List<double>> bbox { get; set; }
        public int tollFare { get; set; }
        public int taxiFare { get; set; }
        public int fuelPrice { get; set; }
    }

    public class Trafast
    {
        public Summary summary { get; set; }
        public List<List<double>> path { get; set; }
        public List<Section> section { get; set; }
        public List<Guide> guide { get; set; }
    }


    [Header("정보 입력")]
    public string strBasURL = "";
    public List<string> laitudelist = new List<string>();
    public List<string> longitudelist = new List<string>(); 
    public string laitude = "";
    public string longitude = "";
    public string goallaitude = "";
    public string goallongitude = "";
    private string strAPIKey = "lxs6g7f2lp";
    private string secretKey = "HbLlHWVtGrDQErhwu6rS2NH23o1SPZ3hhfC5isvX";

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator Loader()
    {
        string waypoint = "";
        if(laitudelist.Count>=2)
        {
            waypoint = "&waypoints=";
            for(int i=1; i<laitudelist.Count; i++)
            {
                if(i!=laitudelist.Count-1)
                {
                    waypoint += (longitudelist[i] + "," + laitudelist[i]+ ":");
                }
                else
                {
                    waypoint += (longitudelist[i] + "," + laitudelist[i]);
                }
            }
        }

        string str = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving?start=" + "124.744" + "," + "37.9595" + "&goal=" + longitudelist[0] + "," + laitudelist[0] +waypoint+"&option=trafast";

        print(str);
        UnityWebRequest request = UnityWebRequest.Get(str);

        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", strAPIKey);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", secretKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            print("Error");
        }
        else
        {
            print(request.downloadHandler.text);
            Root Distance = JsonConvert.DeserializeObject<Root>(request.downloadHandler.text);
            print(Distance.route.trafast[0].summary.distance);
        }
    }

    public void OnClickDirectionBtn()
    {
        StartCoroutine(Loader());
    }
}
