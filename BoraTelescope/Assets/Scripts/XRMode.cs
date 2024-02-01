//using PanTiltControl_v2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XRMode : MonoBehaviour
{
    public GameManager gamemanager;
    public LabelDetail labeldetail;
    public SunAPITest.CCTVControl cctvcontrol;

    public GameObject CameraWindow;
    public GameObject AllMapLabels;
    public Camera UICam;
    public GameObject AllIUi;
    public GameObject SelectLabel;

    public GameObject XRToggle;

    public float xpulse;
    public float ypulse;
    public float currentMotor_x;
    public float currentMotor_y;
    public static float ValueX;
    public static float ValueY;
    public static float labelzoom;

    string dir;
    int touchcount_int;
    float m_fOldToucDis = 0f;       // 터치 이전 거리를 저장합니다.
    float m_fFieldOfView = 0;
    public float zoommove_t;
    public float playtime;

    public static int panFreq_ARR = 0;
    public static int panFreq_Near = 0;
    public static int panFreq_Far = 0;
    public static int PanFreq = 0;

    //5메가픽셀
    public static float MaxZoom = 385;
    float MaxUICam = 540;
    float MinUICam = 100;

    //12메가픽셀
    //public static float MaxZoom = 680;
    //float MaxUICam = 1152;
    //float MinUICam = 475;

    public static float minpan;
    public static float maxpan;
    private float mintilt;
    private float maxtilt;
    float labelscale;

    public static bool StartMove = false;
    bool seeDetail = false;
    public static bool FirstXRClick = false;
    public static bool otherposition = false;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gamemanager.UISetting();

        if(ContentsInfo.ContentsName == "Apsan")
        {
            AllMapLabels.transform.parent.gameObject.GetComponent<BehindLabel>().ReadytoStart();
            BehindLabel.ReadLabelPosition();
        } else if(ContentsInfo.ContentsName == "Aegibong" || ContentsInfo.ContentsName == "Typhoon")
        {
            AllMapLabels.transform.parent.gameObject.GetComponent<DisableLabel>().ReadytoStart();
            DisableLabel.ReadLabelPosition();
            //AllMapLabels.transform.parent.gameObject.GetComponent<DisableLabel>().MapLabel();
        }

        gamemanager.pinchzoominout.ZoomState.gameObject.SetActive(true);
        gamemanager.broadcastcam.ReadytoStart();
        //camerazoom = CameraWindow.transform.GetChild(0).GetChild(0).gameObject.GetComponent<CameraClient>();
        //camerazoom.DigitalZoom("Origin");
        cctvcontrol.DigitalZoom("Origin");
        gamemanager.pinchzoominout.prevzoom = 0;
        if (FunctionCustom.OnceLiveXR == true)
        {
            FunctionCustom.functionorigin.OnceLiveXR_notice();
        }

        minpan = XRMode_Manager.MinPan;
        maxpan = XRMode_Manager.MaxPan;
        mintilt = XRMode_Manager.MinTilt;
        maxtilt = XRMode_Manager.MaxTilt;

        if (GameManager.PrevMode == "ClearMode")
        {
            Invoke("AutoClose", 3f);
        }

        if (gamemanager.WantNoLabel == false)
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.ChangeMode, "ChangeMode : Finish(" + GameManager.PrevMode + " - " + "XRMode)", GetType().ToString());
            GameManager.PrevMode = "XRMode";
        }
        else if (gamemanager.WantNoLabel == true)
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.ChangeMode, "ChangeMode : Finish(" + GameManager.PrevMode + " - " + "LIveMode)", GetType().ToString());
            GameManager.PrevMode = "LiveMode";
        }

        PanFreq = panFreq_ARR;
        ValueX = XRMode_Manager.TotalPan;
        ValueY = XRMode_Manager.TotalTilt;

        if (GameManager.SettingLabelPosition == true)
        {
            gamemanager.xrmode_manager.enabled = true;
            gamemanager.xrmode.enabled = false;
        }
        else if (GameManager.SettingLabelPosition == false)
        {
            gamemanager.xrmode.enabled = true;
            gamemanager.xrmode_manager.enabled = false;
        }
        //gamemanager.labelmake.ReadytoStart();
        gamemanager.label.SelectCategortButton(gamemanager.label.CategoryContent.transform.GetChild(0).gameObject);
        playtime = 0;

        if (CheckingView.CheckingHour == false)
        {
            Invoke("WaitNotice", 30f);
        }
    }

    public void WaitNotice()
    {
        CheckingView.StartChecking = true;
    }

    public void AutoClose()
    {
        gamemanager.TipClose();
    }

    // Update is called once per frame
    void Update()
    {
        playtime += Time.deltaTime;
        zoommove_t += Time.deltaTime * 3;

        //currentMotor_x = PanTiltControl.NowPanPulse;
        //currentMotor_y = PanTiltControl.NowTiltPulse;
        //Debug.Log("today " + currentMotor_x + " / " + currentMotor_y);

        CameraWindow.transform.localPosition = new Vector3(currentMotor_x * ValueX, currentMotor_y * ValueY, 0);

        //if(playtime >= 3600)
        //{
        //    NoticeWindow.NoticeWindowOpen("PantiltOrigin");
        //    playtime = 0;
        //}
        /*
        if ((float)camerazoom.zoomFactor <= 4)
        {
            if(gamemanager.WantNoLabel == false)
            {
                AllMapLabels.SetActive(true);
                if (AllMapLabels.transform.parent.childCount != 0)
                {
                    AllMapLabels.transform.parent.GetChild(1).gameObject.SetActive(true);
                }

                if (ContentsInfo.ContentsName == "Aegibong")
                {
                    this.GetComponent<Aegibong_Eco>().ResetXR();
                }
            }
            //labelzoom = 540 - ((float)camerazoom.zoomFactor - 1) / 3 * 440;
            labelzoom = ((float)camerazoom.zoomFactor - 1) * MaxZoom / 19;
            //AllMapLabels.transform.localPosition = new Vector3(AllMapLabels.transform.localPosition.x, AllMapLabels.transform.localPosition.y, labelzoom);
            //UICam.orthographicSize = (MaxZoom - (labelzoom)) / MaxZoom * (MaxUICam - MinUICam) + MinUICam;
            UICam.orthographicSize = 540 - ((float)camerazoom.zoomFactor - 1) / 3 * 440;

            labelscale = 1 + (-labelzoom / 680 * 0.66f);

            if (labelscale != AllMapLabels.transform.GetChild(1).transform.localScale.x)
            {
                for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                {
                    AllMapLabels.transform.GetChild(index).transform.localScale = new Vector3(labelscale, labelscale, labelscale);
                    //AllMapLabels.transform.GetChild(index).transform.position = new Vector3(AllMapLabels.transform.GetChild(index).transform.localPosition.x, AllMapLabels.transform.GetChild(index).transform.localPosition.y);
                }
            }
        }
        else if((float)camerazoom.zoomFactor > 4)
        {
            AllMapLabels.SetActive(false);
            AllMapLabels.transform.parent.GetChild(1).gameObject.SetActive(false);
        }*/

        if (AllMapLabels.transform.GetChild(0).transform.GetChild(2).gameObject.transform.localScale.x != cctvcontrol.zoomFactor*7)
        {
            for (int index = 0; index < AllMapLabels.transform.childCount; index++)
            {
                AllMapLabels.transform.GetChild(index).transform.GetChild(2).gameObject.transform.localScale = Vector3.Lerp(AllMapLabels.transform.GetChild(index).transform.GetChild(2).gameObject.transform.localScale, new Vector3(cctvcontrol.zoomFactor*7, cctvcontrol.zoomFactor * 7, cctvcontrol.zoomFactor * 7), Time.deltaTime);
            }
        }
        touchcount_int = Input.touchCount;

        if (touchcount_int >= 2)
        {
            gamemanager.arrowmove.OtherDragRange();
            PinchZoom();
            //PinchZoomInOut.prevzoom = CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition.z;
        }
        else if (touchcount_int < 2)
        {
            m_fOldToucDis = 0;
            if (pinchzoomLog == true)
            {
                //gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_PinchZoom, "XR_PinchZoom : Finish (" + ((int)(gamemanager.xrmode.camerazoom.zoomFactor * 5)).ToString() + ")", GetType().ToString());
                pinchzoomLog = false;
            }
            //PinchZoomInOut.prevzoom = CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition.z;
        }

        if (SelectLabel != null && seeDetail == true)
        {
            if (Mathf.Abs(xpulse - currentMotor_x) < 1 && Mathf.Abs(ypulse - currentMotor_y) < 1)
            //if (Mathf.Abs((SelectLabel.transform.localPosition.x + 202.0f) / ValueX - currentMotor_x) < 1 && Mathf.Abs(SelectLabel.transform.localPosition.y / ValueY - currentMotor_y) < 1)
            {
                if (ContentsInfo.ContentsName != "Typhoon")
                {
                    gamemanager.label.SelectLabel(SelectLabel.name);
                    gamemanager.labeldetail.DetailOpen();
                } else if(ContentsInfo.ContentsName == "Typhoon")
                {
                    Resetothers();
                }
                seeDetail = false;
            }
            else
            {/*
                if (gamemanager.NaviOn == false)
                {
                    gamemanager.navi_t = 0;
                    gamemanager.moveNavi = true;
                    gamemanager.NaviOn = true;
                }*/
            }
        }

        Labelactive();
    }
    
    public void MoveCamera_Arrow()
    {/*
        if (gamemanager.MiniMap_CameraGuide.activeSelf)
        {
            gamemanager.MiniMap_CameraGuide.SetActive(false);
        }
        StartMove = false;

        Resetothers();
        
        switch (gamemanager.MoveDir)
        {
            case "Left":
                if (currentMotor_x >= XRMode_Manager.MinPan)
                {
                    PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.LEFT);
                }
                else
                {
                    PanTiltControl.Stop();
                }
                break;
            case "Right":
                if (currentMotor_x <= XRMode_Manager.MaxPan)
                {
                    PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.RIGHT);
                }
                else
                {
                    PanTiltControl.Stop();
                }
                break;
            case "Up":
                if (currentMotor_y < XRMode_Manager.MaxTilt)
                {
                    PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.UP);
                }
                else
                {
                    PanTiltControl.Stop();
                }
                break;
            case "Down":
                if (currentMotor_y > XRMode_Manager.MinTilt)
                {
                    PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.DOWN);
                }
                else
                {
                    PanTiltControl.Stop();
                }
                break;
        }*/
    }

    bool pinchzoomLog = false;

    public void PinchZoom()
    {
        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            StartPinchZoom();
            if (otherposition == true)
            {
                StartMove = false;
                Resetothers();
            }
        }
        else if (Input.touchCount != 2)
        {
            if (pinchzoomLog == true)
            {
                //gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_PinchZoom, "XR_PinchZoom : Finish (" + ((int)(gamemanager.xrmode.camerazoom.zoomFactor * 5)).ToString() + ")", GetType().ToString());
                pinchzoomLog = false;
            }
            m_fOldToucDis = 0;
        }
    }

    public void StartPinchZoom()
    {
        if (pinchzoomLog == false)
        {
            //gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_PinchZoom, "XR_PinchZoom : Start (" + ((int)(gamemanager.xrmode.camerazoom.zoomFactor * 5)).ToString() + ")", GetType().ToString());
            pinchzoomLog = true;
        }

        int nTouch = Input.touchCount;
        float m_fToucDis = 0f;
        float fDis = 0f;

        m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;
        if (m_fOldToucDis != 0)
        {
            fDis = (m_fToucDis - m_fOldToucDis) * 0.001f;

            // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
            if (fDis < 100f)
            {
                m_fFieldOfView += fDis;
            }

            // 최대는 100, 최소는 20으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 0.0f, 800);
            if (m_fFieldOfView == 0)
            {
                // 확대 / 축소가 갑자기 되지않도록 보간합니다.
                //CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition = Vector3.Lerp(CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition, new Vector3(CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition.x, CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition.y, -m_fFieldOfView), zoommove_t * 0.005f);
                cctvcontrol.DigitalZoom(1.ToString("F1"));
            }
            else
            {
                // 확대 / 축소가 갑자기 되지않도록 보간합니다.
                //CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition = Vector3.Lerp(CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition, new Vector3(CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition.x, CameraWindow.transform.GetChild(0).gameObject.transform.GetChild(0).transform.localPosition.y, -m_fFieldOfView), zoommove_t * 0.005f);
                cctvcontrol.DigitalZoom((m_fFieldOfView / 20).ToString("F1"));
                gamemanager.pinchzoominout.prevzoom = (float)cctvcontrol.zoomFactor;
            }
            m_fOldToucDis = m_fToucDis;
        }
        else if (m_fOldToucDis == 0)
        {
            m_fOldToucDis = m_fToucDis;
        }
    }

    // 라벨 선택하면 팬틸트 움직이기
    public void SelectMapLabel(GameObject Label)
    {
        if (gamemanager.MiniMap_CameraGuide.activeSelf)
        {
            gamemanager.MiniMap_CameraGuide.SetActive(false);
        }

        gamemanager.label.PlayNarr = false;
        WaitStopPlay();

        if (MinimapCustom.Hotspotclick == false)
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_SelectLabel, "XR_SelectLabel : " + Label.name, GetType().ToString());
        }

        AllMapLabels.transform.localPosition = new Vector3(AllMapLabels.transform.localPosition.x, 0);

        if (SelectLabel == null)
        {
            SelectLabel = Label;
            seeDetail = true;

            ClickLabelSetting();
            /*
            if (camerazoom.zoomFactor != 1)
            {
                camerazoom.DigitalZoom("Origin");
            }
            */
            xpulse = (Label.transform.localPosition.x + 202.0f) / ValueX;
            ypulse = Label.transform.localPosition.y / ValueY;

            MoveCamera_Navi();
        }
        else if (SelectLabel != null)
        {
            if (SelectLabel.name == Label.name)
            {
                for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                {
                    try
                    {
                        AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                    }
                    catch
                    { }
                }
                for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
                {
                    try
                    {
                        gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                    }
                    catch
                    {

                    }
                }

                labeldetail.CloseDetailWindow();
                SelectLabel = null;
            }
            else if (SelectLabel.name != Label.name)
            {
                labeldetail.CloseDetailWindow();
                seeDetail = false;
                SelectLabel = null;
                SelectMapLabel(Label);
            }
        }
    }

    // 네비게이션에서 라벨 선택하면 팬틸트 움직이기
    public void SelectNaviLabel(GameObject Label)
    {
        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_SelectNavi, "XR_SelectNavi : " + Label.name, GetType().ToString());
        gamemanager.label.PlayNarr = false;
        WaitStopPlay();
        if (gamemanager.MiniMap_CameraGuide.activeSelf)
        {
            gamemanager.MiniMap_CameraGuide.SetActive(false);
        }

        AllMapLabels.transform.localPosition = new Vector3(AllMapLabels.transform.localPosition.x, 0);

        if (SelectLabel == null)
        {
            //gamemanager.label.SelectLabel(Label.name);
            /*
            gamemanager.navi_t = 0;
            gamemanager.moveNavi = true;
            gamemanager.NaviOn = true;*/

            seeDetail = true;
            ClickLabelSetting();
            /*
            if(camerazoom.zoomFactor != 1)
            {
                camerazoom.DigitalZoom("Origin");
            }
            */
            for (int index = 0; index < AllMapLabels.transform.childCount; index++)
            {
                if (AllMapLabels.transform.GetChild(index).gameObject.name == Label.name)
                {
                    SelectLabel = AllMapLabels.transform.GetChild(index).gameObject;
                    xpulse = (AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x + 202.0f) / ValueX;
                    ypulse = AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.y / ValueY;
                    //Debug.Log("today " + AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.y + " / " + ValueY);
                    if (xpulse != currentMotor_x || ypulse != currentMotor_y)
                    {
                        MoveCamera_Navi();
                    }
                }
            }
        }
        else if (SelectLabel != null)
        {
            if (SelectLabel.name == Label.name)
            {
                for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                {
                    try
                    {
                        AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                    }
                    catch { }
                }

                for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
                {
                    try
                    {
                        gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                    }
                    catch { }
                }

                SelectLabel = null;
                labeldetail.CloseDetailWindow();
            }
            else if (SelectLabel.name != Label.name)
            {
                labeldetail.CloseDetailWindow();
                seeDetail = false;
                SelectLabel = null;

                SelectNaviLabel(Label);
            }
        }
    }

    // 네비게이션 라벨
    public void MoveCamera_Navi()
    {
        if (xpulse >= maxpan)
        {
            if (xpulse - 215 < maxpan)
            {
                xpulse = maxpan - 215;
            }
            else if (xpulse - 100 >= maxpan)
            {
                xpulse = maxpan;
            }
        }
        else if (xpulse <= minpan)
        {
            xpulse = minpan;
        }
        else
        {
            xpulse = xpulse;
        }

        if (ypulse >= maxtilt)
        {
            ypulse = maxtilt;
        }
        else if (ypulse <= mintilt)
        {
            ypulse = mintilt;
        }
        else
        {
            ypulse = ypulse;
        }

        if (Mathf.Abs(currentMotor_x - xpulse) <= 2000)
        {
            PanFreq = panFreq_Near;
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Slow);
            gamemanager.speed_enum = GameManager.Speed_enum.slow;
        }
        else if (Mathf.Abs(currentMotor_x - xpulse) > 2000)
        {
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Fast);
            gamemanager.speed_enum = GameManager.Speed_enum.fast;
        }
        Invoke("RealMovePantilt", 0.1f);
    }

    public void RealMovePantilt()
    {
        //PanTiltControl.SetPulse((uint)xpulse, (uint)ypulse);
        cctvcontrol.GOPanTilt(xpulse, ypulse);
    }

    // 라벨 선택하면 모든 라벨들 버튼 비활성화 및 네비게이션 버튼 비활성화, 네비게이션 닫기
    public void ClickLabelSetting()
    {
        for (int index = 1; index < AllMapLabels.transform.childCount; index++)
        {
            AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
        }
       
        for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
        {
            gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
        }
        gamemanager.allbar.navi_t = 0;
        gamemanager.allbar.moveNavi = true;
        gamemanager.allbar.NaviOn = true;
    }

    public void Resetothers()
    {/*
        if ((float)camerazoom.zoomFactor > 4)
        {
            m_fOldToucDis = 0;
            m_fFieldOfView = 0;
            camerazoom.DigitalZoom((4).ToString());
        }
        */

        //if (GameManager.MoveCamera == false)
        //{
        //    cctvcontrol.DigitalZoom("Origin");
        //}

        if (StartMove == false)
        {
            if (Mathf.Abs(labeldetail.Detail_Background.transform.localPosition.x - LabelDetail.Detail_Close_x) > 1f && SelectLabel != null)
            {
                seeDetail = false;
                for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                {
                    try
                    {
                        AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                    }
                    catch
                    {

                    }
                }
                
                for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
                {
                    try
                    {
                        gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                    }
                    catch { }
                }

                SelectLabel = null;
                labeldetail.CloseDetailWindow();
            }
            else if (Mathf.Abs(labeldetail.Detail_Background.transform.localPosition.x - LabelDetail.Detail_Close_x) < 1f && SelectLabel != null)
            {
                seeDetail = false;
                for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                {
                    try
                    {
                        AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                    }
                    catch { }
                }

                for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
                {
                    try
                    {
                        gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                    }
                    catch { }
                }

                SelectLabel = null;
            }

            if (gamemanager.joystick.enabled)
            {
                for (int index = 1; index < gamemanager.Arrow.transform.childCount; index++)
                {
                    gamemanager.Arrow.transform.GetChild(index).gameObject.SetActive(true);
                }
                gamemanager.joystick.enabled = false;
            }
            StartMove = true;
        }
    }

    public void Labelactive()
    {
        for (int index = 0; index < AllMapLabels.transform.childCount; index++)
        {
            if (CameraWindow.transform.localPosition.x + 1024 - 300 >= AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x
                && CameraWindow.transform.localPosition.x - 1024 + 300 <= AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x
                && CameraWindow.transform.localPosition.y + 540 - 100 >= AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.y
                && CameraWindow.transform.localPosition.y - 540 + 100 <= AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.y)
            {
                /*
                if ((CameraWindow.transform.localPosition.x - AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x) < 0)
                {
                    dir = "Right";
                }
                else if ((CameraWindow.transform.localPosition.x - AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x) > 0)
                {
                    dir = "Left";
                }
                */
                labeleffect(index);

                AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Image>().fillAmount += 2f * Time.deltaTime;
            }
            else
            {
                /*
                if ((CameraWindow.transform.localPosition.x - AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x) < 0)
                {
                    dir = "Left";
                }
                else if ((CameraWindow.transform.localPosition.x - AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x) > 0)
                {
                    dir = "Right";
                }*/

                labeleffect(index);

                AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Image>().fillAmount -= 2f * Time.deltaTime;
            }
        }
    }

    public void labeleffect(int k)
    {
        switch (gamemanager.MoveDir)
        {
            case "Left":
                AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillMethod = Image.FillMethod.Horizontal;

                if (AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillAmount == 1)
                {
                    AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Left;
                }
                else if (AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillAmount == 0)
                {
                    AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Right;
                }
                break;
            case "Right":
                AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillMethod = Image.FillMethod.Horizontal;

                if (AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillAmount == 1)
                {
                    AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Right;
                }
                else if (AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillAmount == 0)
                {
                    AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Left;
                }
                break;
            case "Up":
                AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillMethod = Image.FillMethod.Vertical;

                if (AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillAmount == 1)
                {
                    AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillOrigin = (int)Image.OriginVertical.Top;
                }
                else if (AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillAmount == 0)
                {
                    AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillOrigin = (int)Image.OriginVertical.Bottom;
                }
                break;
            case "Down":
                AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillMethod = Image.FillMethod.Vertical;

                if (AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillAmount == 1)
                {
                    AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillOrigin = (int)Image.OriginVertical.Bottom;
                }
                else if (AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillAmount == 0)
                {
                    AllMapLabels.transform.GetChild(k).gameObject.GetComponent<Image>().fillOrigin = (int)Image.OriginVertical.Top;
                }
                break;
        }
    }

    public void NarrStopPlay()
    {
        if (gamemanager.label.PlayNarr == true)
        {
            gamemanager.label.Narration.Stop();
            //Invoke("WaitStopPlay", 0.5f);
        }
        else if (gamemanager.label.PlayNarr == false)
        {
            gamemanager.label.Narration.Play();
            //gamemanager.label.PlayNarr = true;
        }
        Invoke("WaitStopPlay", 0.5f);
    }

    public void WaitStopPlay()
    {
        if (gamemanager.label.PlayNarr == true)
        {
            //gamemanager.label.Narration.Stop();
            gamemanager.labeldetail.Detail_Background.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = gamemanager.label.Narr_On;
            gamemanager.label.PlayNarr = false;
            gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_DetailSound, "XR_Detail:SoundOff", GetType().ToString());
        }
        else if (gamemanager.label.PlayNarr == false)
        {
            //gamemanager.label.Narration.Play();
            gamemanager.labeldetail.Detail_Background.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = gamemanager.label.Narr_Off;
            gamemanager.label.PlayNarr = true;
            gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_DetailSound, "XR_Detail:SoundOn", GetType().ToString());
        }
    }

    public void ChangeXRMode()
    {
        if (XRToggle.transform.GetChild(1).gameObject.activeSelf)
        {
            XRToggle.transform.GetChild(1).gameObject.SetActive(false);
            FirstXRClick = true;
        }

        if (!AllMapLabels.activeSelf)
        {
            gamemanager.Menu(gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(1).gameObject);
            XRToggle.transform.GetChild(0).gameObject.SetActive(true);
            gamemanager.announcemode.CloseObj();
            gamemanager.announcemode.OpenMode("XR");
        }
        else if (AllMapLabels.activeSelf)
        {
            //PanTiltControl.Stop();

            AllMapLabels.SetActive(false);
            gamemanager.allbar.navi_t = 0;
            gamemanager.allbar.NaviOn = true;
            gamemanager.allbar.moveNavi = true;

            labeldetail.SelectCloseButton();
            labeldetail.CloseDetailWindow();
            labeldetail.CancelSelect();

            for (int index = 1; index < AllMapLabels.transform.childCount; index++)
            {
                AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
            }

            gamemanager.Menu(gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(0).gameObject);
            XRToggle.transform.GetChild(0).gameObject.SetActive(false);
            gamemanager.announcemode.CloseObj();
            gamemanager.announcemode.OpenMode("Live");
        }
        gamemanager.UISetting();
        gamemanager.TipClose();
    }

    public void XREffect_ani()
    {
        switch (ContentsInfo.ContentsName)
        {
            case "Aegibong":
                if (SelectLabel != null)
                {
                    if (SelectLabel.name == "Spoonbill")
                    {
                        //this.GetComponent<Aegibong_Eco>().spoonbill.SelectSpoonbill();
                    }
                }
                break;
        }
    }
}
