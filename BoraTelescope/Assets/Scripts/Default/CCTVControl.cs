using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Net;

namespace SunAPITest
{
    public class CCTVControl : MonoBehaviour
    {
        public GameManager gamemanager;
        //public static string url = "http://172.30.1.8/";  //테스트 사이트
        public static string firsturl;  //테스트 사이트
        public static string secondurl;  //테스트 사이트
        public static string url;
        public static string UseUrl;
        public static string uid = "admin";
        public static string pwd = "Bora7178";
        public Communication.HttpRequest httpRequest = new Communication.HttpRequest();

        string[] ptzstring;
        public static float currentZoom;
        public float zoomFactor;
        public static float purposezoom;
        public static bool SwitchiingCCTV = false;

        public CCTVViewer monitor;

        void Start()
        {
            gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            //monitor.ReadytoStart();
            CheckPTZ();
            GOPanTilt((uint)GameManager.startlabel_x, (uint)GameManager.startlabel_y);
            //url += "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&MoveSpeed=10";
            //httpRequest.Request("GET", url, uid, pwd);
            //url += "/stw-cgi/ptzcontrol.cgi?msubmenu=absolute&action=control&pan=180";
            //httpRequest.Request("GET", url, uid, pwd);
            //url += "/stw-cgi/ptzcontrol.cgi?msubmenu=absolute&action=control&Zoom=32";
            //httpRequest.Request("GET", url, uid, pwd);

            //CheckPTZ();
        }

        public void CheckPTZ()
        {
            UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=query&action=view&Query=Pan,Tilt,Zoom";
            string a = httpRequest.QueryRequest("GET", UseUrl, uid, pwd);
            
            ptzstring = a.Split('\n');
            for (int index = 0; index < 3; index++)
            {
                ptzstring[index].Replace('\r',' ');
            }

            if (SceneManager.GetActiveScene().name.Contains("XRMode") && gamemanager != null)
            {
                gamemanager.xrmode.currentMotor_x = float.Parse(ptzstring[0].Substring(ptzstring[0].IndexOf('=') + 1));

                if(gamemanager.xrmode.currentMotor_x >= 200)
                {
                    gamemanager.xrmode.currentMotor_x = gamemanager.xrmode.currentMotor_x - 360;
                }

                gamemanager.xrmode.currentMotor_y = float.Parse(ptzstring[1].Substring(ptzstring[1].IndexOf('=') + 1)) * -1;
                currentZoom = float.Parse(ptzstring[2].Substring(ptzstring[2].IndexOf('=') + 1));
                //Debug.Log("Pan : " + gamemanager.xrmode.currentMotor_x + ", Tilt : " + gamemanager.xrmode.currentMotor_y + ", Zoom : " + currentZoom);
            }
            Invoke("CheckPTZ", 0.5f);
        }

        public void MoveCamera_Arrow()
        {
            if (gamemanager.MiniMap_CameraGuide.activeSelf)
            {
                gamemanager.MiniMap_CameraGuide.SetActive(false);
            }
            XRMode.StartMove = false;

            gamemanager.xrmode.Resetothers();

            //UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=" + gamemanager.MoveDir.ToString();
            //httpRequest.Request("GET", UseUrl, uid, pwd);

            switch (gamemanager.MoveDir)
            {
                case "Left":
                    //Debug.Log("Pan : " + gamemanager.xrmode.currentMotor_x + ", MinPan : " + XRMode_Manager.MinPan);
                    if (gamemanager.xrmode.currentMotor_x >= XRMode_Manager.MinPan)
                    {
                        UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Left";
                        httpRequest.Request("GET", UseUrl, uid, pwd);
                        Debug.Log(1);
                    }
                    else
                    {
                        StopControl();
                        Debug.Log(2);
                    }
                    break;
                case "Right":
                    if (gamemanager.xrmode.currentMotor_x <= XRMode_Manager.MaxPan)
                    {
                        UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Right";
                        httpRequest.Request("GET", UseUrl, uid, pwd);
                        Debug.Log(1);
                    }
                    else
                    {
                        StopControl();
                        Debug.Log(2);
                    }
                    break;
                case "Up":
                    if (gamemanager.xrmode.currentMotor_y < XRMode_Manager.MaxTilt)
                    {
                        UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Up";
                        httpRequest.Request("GET", UseUrl, uid, pwd);
                        Debug.Log(1);
                    }
                    else
                    {
                        StopControl();
                        Debug.Log(2);
                    }
                    break;
                case "Down":
                    if (gamemanager.xrmode.currentMotor_y > XRMode_Manager.MinTilt)
                    {
                        UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Down";
                        httpRequest.Request("GET", UseUrl, uid, pwd);
                        Debug.Log(1);
                    }
                    else
                    {
                        StopControl();
                        Debug.Log(2);
                    }
                    break;
            }
        }

        public void StopControl()
        {
            UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Stop";
            httpRequest.Request("GET", UseUrl, uid, pwd);
        }

        public void GOPanTilt(float pan, float tilt)
        {
            UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=absolute&action=control&Pan=" + pan.ToString();
            httpRequest.Request("GET", UseUrl, uid, pwd);
            UseUrl += "/stw-cgi/ptzcontrol.cgi?msubmenu=absolute&action=control&Tilt=" + tilt.ToString();
            httpRequest.Request("GET", UseUrl, uid, pwd);
        }

        public void MoveSpeed(int speed)
        {
            UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&MoveSpeed=" + speed.ToString();
            httpRequest.Request("GET", UseUrl, uid, pwd);
        }

        public void DigitalZoom(string zoom)
        {
            switch (zoom)
            {
                case "ZoomIn":
                    zoomFactor = purposezoom;
                    break;
                case "ZoomOut":
                    zoomFactor = purposezoom;
                    break;
                case "Origin":
                    zoomFactor = 1;
                    break;
                default:
                    zoomFactor = float.Parse(zoom);
                    break;
            }

            ZoomControl(zoomFactor);
        }

        public void ZoomControl(float zoom)
        {
            UseUrl = "http://" + url + "/stw-cgi/ptzcontrol.cgi?msubmenu=absolute&action=control&Zoom=" + zoom.ToString();
            httpRequest.Request("GET", UseUrl, uid, pwd);
        }
    }
}
