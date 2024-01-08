using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using PanTiltControl_v2;

public class PinchZoomInOut : MonoBehaviour
{
    private GameManager gamemanager;
    public GameObject butzoom;
    private Image Zoombut_img;
    private Button Zoombut_btn;
    GameObject zoomimg;
    public Sprite ZoomIn;
    public Sprite ZoomOut;
    public Text ZoomState;

    public float prevzoom;

    public static bool ZoomMove = false;
    public static bool ZoomIN = false;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Zoombut_img = butzoom.GetComponent<Image>();
        Zoombut_btn = butzoom.GetComponent<Button>();

        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            zoomimg = gamemanager.xrmode.CameraWindow.transform.GetChild(0).transform.GetChild(0).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (Mathf.Abs((float)gamemanager.xrmode.cctvcontrol.zoomFactor - 1) <= 0.01f)
            {
                Zoombut_img.sprite = ZoomIn;
            }
            else if (Mathf.Abs((float)gamemanager.xrmode.cctvcontrol.zoomFactor - 1) > 0.01f)
            {
                Zoombut_img.sprite = ZoomOut;
            }

            //float bar_y = ((float)gamemanager.xrmode.cctvcontrol.zoomFactor - 1) / 19 * 49 - 18.5f;
            float bar_y = ((float)gamemanager.xrmode.cctvcontrol.zoomFactor - 1) / (39) * 49 - 18.5f;

            gamemanager.ZoomBar.transform.localPosition = new Vector3(0.5f, bar_y, gamemanager.ZoomBar.transform.localPosition.z);

            ZoomState.text = ((int)(20 + (gamemanager.xrmode.cctvcontrol.zoomFactor - 1) / 19 * 80)).ToString();
            ZoomState.text = ((int)(gamemanager.xrmode.cctvcontrol.zoomFactor * 5)).ToString();
        } else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            //Zoom 정도표시
            if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - ClearMode.MaxZoomOut) <= 0.5f)
            {
                Zoombut_img.sprite = ZoomIn;
            }
            else if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.localPosition.z - ClearMode.MaxZoomOut) > 0.5f)
            {
                Zoombut_img.sprite = ZoomOut;
            }

            //float bar_y = gamemanager.clearMode.CameraWindow.transform.parent.gameObject.transform.position.z / 1852 * 60 - 30;
            float bar_y = (gamemanager.clearmode.CameraWindow.transform.parent.gameObject.transform.position.z - ClearMode.MaxZoomOut) / (ClearMode.MaxZoomIn - ClearMode.MaxZoomOut) * 49 - 18.5f;
            //Debug.Log(gamemanager.ZoomBar.transform.localPosition);
            gamemanager.ZoomBar.transform.localPosition = new Vector3(0.5f, bar_y, gamemanager.ZoomBar.transform.localPosition.z);
            //ZoomState.text = (gamemanager.clearmode.CameraWindow.transform.parent.gameObject.transform.position.z * 5).ToString();
        }

        if (ZoomMove == true)
        {
            MoveCameraCanvas();
        }
    }

    public void BtnZoom()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            //prevzoom = (float)gamemanager.xrmode.camerazoom.zoomFactor;
            gamemanager.xrmode.zoommove_t = 0;
            if (gamemanager.xrmode.cctvcontrol.zoomFactor == 1)    // 1->10
            {
                gamemanager.MiniMap_CameraGuide.SetActive(false);
                //PanTiltControl.Stop();
                gamemanager.xrmode.cctvcontrol.StopControl();
                ZoomMove = true;
                ZoomIN = true;
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Slow);
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Slow);
                gamemanager.speed_enum = GameManager.Speed_enum.slow;
                SunAPITest.CCTVControl.purposezoom = 2;
                prevzoom = 1;
                gamemanager.xrmode.cctvcontrol.DigitalZoom("ZoomIn");
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Zoom, "BtnZoomIn", GetType().ToString());
            }
            else if (gamemanager.xrmode.cctvcontrol.zoomFactor > 1 && gamemanager.xrmode.cctvcontrol.zoomFactor <= 2)    //2->1,4
            {

                gamemanager.MiniMap_CameraGuide.SetActive(false);
                //PanTiltControl.Stop();
                gamemanager.xrmode.cctvcontrol.StopControl();
                ZoomMove = true;
                ZoomIN = false;
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Middle);
                gamemanager.speed_enum = GameManager.Speed_enum.middle;

                if (prevzoom <= 1)
                {
                    SunAPITest.CCTVControl.purposezoom = 4;
                    gamemanager.xrmode.cctvcontrol.DigitalZoom("ZoomIn");
                    prevzoom = 2;
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Zoom, "BtnZoomIn", GetType().ToString());
                }
                else if (prevzoom > 1)
                {
                    SunAPITest.CCTVControl.purposezoom = 1;
                    gamemanager.xrmode.cctvcontrol.DigitalZoom("ZoomOut");
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Zoom, "BtnZoomOut", GetType().ToString());
                }
            }
            else if (gamemanager.xrmode.cctvcontrol.zoomFactor > 2 && gamemanager.xrmode.cctvcontrol.zoomFactor <= 4)   //4->2,16
            {

                gamemanager.MiniMap_CameraGuide.SetActive(false);
                //PanTiltControl.Stop();
                gamemanager.xrmode.cctvcontrol.StopControl();
                ZoomMove = true;
                ZoomIN = false;
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Middle);
                gamemanager.speed_enum = GameManager.Speed_enum.middle;

                if (prevzoom <= 2)
                {
                    SunAPITest.CCTVControl.purposezoom = 16;
                    gamemanager.xrmode.cctvcontrol.DigitalZoom("ZoomIn");
                    prevzoom = 4;
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Zoom, "BtnZoomIn", GetType().ToString());
                }
                else if (prevzoom > 2)
                {
                    SunAPITest.CCTVControl.purposezoom = 2;
                    gamemanager.xrmode.cctvcontrol.DigitalZoom("ZoomOut");
                    //prevzoom = 10.1f;
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Zoom, "BtnZoomOut", GetType().ToString());
                }
            }
            else if (gamemanager.xrmode.cctvcontrol.zoomFactor > 4 && gamemanager.xrmode.cctvcontrol.zoomFactor <= 16)   //16->4,40
            {

                gamemanager.MiniMap_CameraGuide.SetActive(false);
                //PanTiltControl.Stop();
                gamemanager.xrmode.cctvcontrol.StopControl();
                ZoomMove = true;
                ZoomIN = false;
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Middle);
                gamemanager.speed_enum = GameManager.Speed_enum.middle;
                if (prevzoom <=4)
                {
                    SunAPITest.CCTVControl.purposezoom = 40;
                    gamemanager.xrmode.cctvcontrol.DigitalZoom("ZoomIn");
                    prevzoom = 16;
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Zoom, "BtnZoomIn", GetType().ToString());
                }
                else if (prevzoom > 4)
                {
                    SunAPITest.CCTVControl.purposezoom = 4;
                    gamemanager.xrmode.cctvcontrol.DigitalZoom("ZoomOut");
                    //prevzoom = 20.1f;
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Zoom, "BtnZoomOut", GetType().ToString());
                }
            }
            else if (gamemanager.xrmode.cctvcontrol.zoomFactor >= 40)  //40->16
            {

                gamemanager.MiniMap_CameraGuide.SetActive(false);
                //PanTiltControl.Stop();
                gamemanager.xrmode.cctvcontrol.StopControl();
                ZoomMove = true;
                ZoomIN = false;
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
                //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Middle);
                gamemanager.speed_enum = GameManager.Speed_enum.middle;
                SunAPITest.CCTVControl.purposezoom = 16;
                gamemanager.xrmode.cctvcontrol.DigitalZoom("ZoomOut");
                prevzoom = 40f;
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Zoom, "BtnZoomOut", GetType().ToString());
            }
            //prevzoom = (float)gamemanager.cctvcontrol.zoomFactor;
            Zoombut_btn.enabled = false;
        }
        else if(SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            ClearMode.zoommove_t = 0;
            ClearMode.SeeSelectLabel_move = false;
            ClearMode.SeeSelectLabel_zoom = false;
            gamemanager.clearmode.TouchStop = false;

            for (int index = 0; index < gamemanager.clearmode.AllMapLabels.transform.childCount; index++)
            {
                gamemanager.clearmode.AllMapLabels.transform.GetChild(index).GetComponent<Button>().enabled = true;
            }

            if (gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z >= ClearMode.MaxZoomOut && gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z < (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2)
            {
                ZoomMove = true;
                ZoomIN = true;
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Zoom, "ZoomIn", GetType().ToString());
            }
            else if (gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z == ClearMode.MaxZoomIn)
            {
                ZoomMove = true;
                ZoomIN = false;
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Zoom, "ZoomOut", GetType().ToString());
            }
            else if(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z < ClearMode.MaxZoomIn && gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z >= (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2)
            {
                if(prevzoom == ClearMode.MaxZoomOut)
                {
                    ZoomMove = true;
                    ZoomIN = true;
                } else if(prevzoom == ClearMode.MaxZoomIn)
                {
                    ZoomMove = true;
                    ZoomIN = false;
                }
                else
                {
                    ZoomMove = true;
                    ZoomIN = true;
                }
            }
            Zoombut_btn.enabled = false;
        }
    }


    public void MoveCameraCanvas()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (ZoomIN == true)
            {
                //Debug.Log(Mathf.Abs((float)gamemanager.xrmode.camerazoom.zoomFactor - 1));
                if (Mathf.Abs((float)gamemanager.xrmode.cctvcontrol.zoomFactor - SunAPITest.CCTVControl.purposezoom) > 0.4f)
                {
                    //zoomimg.transform.localPosition = Vector3.Lerp(zoomimg.transform.localPosition, new Vector3(zoomimg.transform.localPosition.x, zoomimg.transform.localPosition.y, -XRMode.MaxZoom), gamemanager.xrmode.zoommove_t * 0.2f);
                    StopBtnZoom();
                }
                else if (Mathf.Abs((float)gamemanager.xrmode.cctvcontrol.zoomFactor - SunAPITest.CCTVControl.purposezoom) <= 0.4f)
                {
                    //zoomimg.transform.localPosition = new Vector3(zoomimg.transform.localPosition.x, zoomimg.transform.localPosition.y, -XRMode.MaxZoom);
                    gamemanager.xrmode.cctvcontrol.zoomFactor = SunAPITest.CCTVControl.purposezoom;
                    gamemanager.xrmode.cctvcontrol.DigitalZoom(gamemanager.xrmode.cctvcontrol.zoomFactor.ToString());
                    //prevzoom = gamemanager.cctvcontrol.zoomFactor;
                    ZoomIN = false;
                    ZoomMove = false;
                    gamemanager.xrmode.zoommove_t = 0;
                    Zoombut_btn.enabled = true;
                }
            }
            else if (ZoomIN == false)
            {
                //Debug.Log(Mathf.Abs((float)gamemanager.xrmode.camerazoom.zoomFactor - 1));
                if (Mathf.Abs((float)gamemanager.xrmode.cctvcontrol.zoomFactor - SunAPITest.CCTVControl.purposezoom) > 0.4f)
                {
                    //zoomimg.transform.localPosition = Vector3.Lerp(zoomimg.transform.localPosition, new Vector3(zoomimg.transform.localPosition.x, zoomimg.transform.localPosition.y, 0), gamemanager.xrmode.zoommove_t * 0.2f);
                    StopBtnZoom();
                }
                else if (Mathf.Abs((float)gamemanager.xrmode.cctvcontrol.zoomFactor - SunAPITest.CCTVControl.purposezoom) <= 0.4f)
                {
                    //zoomimg.transform.localPosition = new Vector3(zoomimg.transform.localPosition.x, zoomimg.transform.localPosition.y, 0);
                    gamemanager.xrmode.cctvcontrol.zoomFactor = SunAPITest.CCTVControl.purposezoom;
                    gamemanager.xrmode.cctvcontrol.DigitalZoom(gamemanager.xrmode.cctvcontrol.zoomFactor.ToString());
                    //prevzoom = gamemanager.cctvcontrol.zoomFactor;
                    ZoomIN = true;
                    ZoomMove = false;
                    gamemanager.xrmode.zoommove_t = 0;
                    Zoombut_btn.enabled = true;
                }
            }
        } else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            if (ZoomIN == true)
            {
                if (gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z >= (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2)
                {
                    if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - ClearMode.MaxZoomIn) > 10f)
                    {
                        gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = Vector3.Lerp(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition, new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, ClearMode.MaxZoomIn), ClearMode.zoommove_t * 0.03f);
                        StopBtnZoom();
                    }
                    else if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - ClearMode.MaxZoomIn) <= 10f)
                    {
                        gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, ClearMode.MaxZoomIn);
                        ZoomIN = false;
                        ZoomMove = false;
                        ClearMode.zoommove_t = 0;
                        Zoombut_btn.enabled = true;
                        prevzoom = gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z;
                    }
                }
                else if (gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z != (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2)
                {
                    if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2) > 10f)
                    {
                        gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = Vector3.Lerp(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition, new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2), ClearMode.zoommove_t * 0.03f);
                        StopBtnZoom();
                    }
                    else if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2) <= 10f)
                    {
                        gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2);
                        ZoomIN = false;
                        ZoomMove = false;
                        ClearMode.zoommove_t = 0;
                        Zoombut_btn.enabled = true;
                    }
                }
            }
            else if (ZoomIN == false)
            {
                if (gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z <= (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2)
                {
                    if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - ClearMode.MaxZoomOut) > 10f)
                    {
                        gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = Vector3.Lerp(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition, new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, ClearMode.MaxZoomOut), ClearMode.zoommove_t * 0.03f);
                        StopBtnZoom();
                    }
                    else if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - ClearMode.MaxZoomOut) <= 10f)
                    {
                        gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, ClearMode.MaxZoomOut);
                        ZoomIN = true;
                        ZoomMove = false;
                        ClearMode.zoommove_t = 0;
                        Zoombut_btn.enabled = true;
                        prevzoom = gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z;
                    }
                }
                else if (gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z != (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2)
                {
                    if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2) > 10f)
                    {
                        gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = Vector3.Lerp(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition, new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2), ClearMode.zoommove_t * 0.03f);
                        StopBtnZoom();
                    }
                    else if (Mathf.Abs(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z - (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2) <= 10f)
                    {
                        gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, (ClearMode.MaxZoomIn + ClearMode.MaxZoomOut) / 2);
                        ZoomIN = true;
                        ZoomMove = false;
                        ClearMode.zoommove_t = 0;
                        Zoombut_btn.enabled = true;
                    }
                }
            }
        }
    }

    public void StopBtnZoom()
    {
        if (Input.touchCount >= 1)
        {
            if (SceneManager.GetActiveScene().name.Contains("XRMode")&& gamemanager.xrmode.zoommove_t > 0.5f)
            {
                //zoomimg.transform.localPosition = new Vector3(zoomimg.transform.localPosition.x, zoomimg.transform.localPosition.y, zoomimg.transform.localPosition.z);
                //gamemanager.xrmode.camerazoom.zoomFactor = gamemanager.xrmode.camerazoom.zoomFactor;
                gamemanager.xrmode.zoommove_t = 0;
                Zoombut_btn.enabled = true;
                ZoomMove = false;
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode") && ClearMode.zoommove_t > 0.5f)
            {
                gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition = new Vector3(gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.x, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.y, gamemanager.clearmode.CameraWindow.transform.parent.transform.localPosition.z);
                ClearMode.zoommove_t = 0;
                Zoombut_btn.enabled = true;
                ZoomMove = false;
            }
        }
    }
}
