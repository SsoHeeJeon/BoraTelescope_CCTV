//using PanTiltControl_v2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// AR��� �󺧸� �� �� ��ġ ��Ī
/// </summary>
public class XRModeLabelPosition
{
    public string LabelName;
    public float Label_X;
    public float Label_Y;
    public float LabelScale;

    public XRModeLabelPosition(string labelname, float label_x, float label_y, float labelsize)
    {
        LabelName = labelname;
        Label_X = label_x;
        Label_Y = label_y;
        LabelScale = labelsize;
    }
}

/// <summary>
/// ��ƿƮ����
/// </summary>
public class PanTiltRange
{
    public float Min_Pan;
    public float Max_Pan;
    public float Min_Tilt;
    public float Max_Tilt;
    public float StartPosition_x;
    public float StartPosition_y;
    public float ChangeValue_x;
    public float ChangeValue_y;
    public int WaitingTime;
    public string CCTVURL_1;

    public PanTiltRange(float minx, float maxx, float miny, float maxy, float startlabel_x, float startlabel_y, float valueX, float valueY, int waittime, string url_1)
    {
        Min_Pan = minx;
        Max_Pan = maxx;
        Min_Tilt = miny;
        Max_Tilt = maxy;
        StartPosition_x = startlabel_x;
        StartPosition_y = startlabel_y;
        ChangeValue_x = valueX;
        ChangeValue_y = valueY;
        WaitingTime = waittime;
        CCTVURL_1 = url_1;
    }
}

public class LabelText
{
    public string LabelName;
    public string LabelName_K;
    public string LabelName_E;
    public string LabelName_C;
    public string LabelName_J;
    public string Korean;
    public string English;
    public string Chinese;
    public string Japanese;

    public LabelText(string labelname, string labelK, string labelE, string labelC, string labelJ, string kor, string eng, string chi, string jap)
    {
        LabelName = labelname;
        LabelName_K = labelK;
        LabelName_E = labelE;
        LabelName_C = labelC;
        LabelName_J = labelJ;
        Korean = kor;
        English = eng;
        Chinese = chi;
        Japanese = jap;
    }
}

public class GameManager : ContentsInfo
{
    public enum Language_enum
    {
        Korea, English, Chinese, Japanese
    }
    public static Language_enum currentLang;

    public enum Speed_enum
    {
        slow,
        middle,
        fast,
    }
    public Speed_enum speed_enum;

    /// <summary>
    /// Script
    /// </summary>
    public Loading loading;
    //public TelescopeMode telescopemode;
    public XRMode xrmode;
    public XRMode_Manager xrmode_manager;
    public ClearMode clearmode;
    public WaitingMode waitingmode;
    //public PantiltOrigin pantiltorigin;
    public PinchZoomInOut pinchzoominout;
    public SetDragRange setdragrange;
    public Label label;
    public BoraJoyStick joystick;
    public MiniMap minimap;
    public UILanguage uilang;
    public LabelDetail labeldetail;
    public AllBarOnOff allbar;
    public SettingManager settingmanager;
    public ArrowMove arrowmove;
    public ZoomFunction zoomFunc;
    public AnnounceMode announcemode;
    public BroadcastCam broadcastcam;
    public LabelMake labelmake;
    public SelfiFunction selfifunction;
    public TouchUIObj touchuiobj;
    public Visitmanager visitmanager;
    public TourismLite tourLite;

    /// <summary>
    /// ���� UI
    /// </summary>
    public GameObject UI_All;
    public GameObject Homebtn;

    public GameObject NavigationBar;
    public GameObject LanguageBar;
    public GameObject ETCBar;

    public GameObject MenuBar;
    public GameObject FilterBtn;
    public GameObject CaptureBtn;
    public GameObject TourismModeBtn;
    public GameObject ETCModeBtn;
    public GameObject GuideModeBtn;
    public GameObject PastModeBtn;
    public GameObject View360Btn;
    public GameObject Settingbtn;
    public GameObject LanguageBtn;
    public GameObject Tipbtn;
    public GameObject ManagerBtn;
    public GameObject Visitbtn;

    public GameObject Arrow;
    public GameObject MiniMap_Background;
    public GameObject MiniMap_Camera;
    public GameObject MiniMap_CameraGuide;
    public GameObject ZoomObj;
    public GameObject ZoomBar;
    public GameObject PlayTime;
    public GameObject CategoryContent;
    public GameObject Tip_Obj;
    public GameObject AutoSelectImg;
    public GameObject BackGround;
    public GameObject CaptueObject;
    public GameObject Selfi_Obj;
    public GameObject CaptureSelfi;
    public GameObject StopSelfi;
    public GameObject ErrorMessage;

    public Transform[] spot_set = new Transform[4];

    public AudioSource ButtonEffect;
    public AudioClip ButtonSound;

    public Sprite HomeBase;
    public Sprite HomeBase_1;
    public Sprite HomeBase_2;

    float count;
    bool entermode = false;
    bool startcount = false;
    string password;
    public int Wcount;

    /// <summary>
    /// ����
    /// </summary>
    public static float waitingTime = 300;
    string ManagerModePassword = "025697178";
    public Vector3 Arrowpos_normal = new Vector3(213.0f, 197.0f);
    public Vector3 Arrowpos_extend = new Vector3(286.0f, 180.0f);
    public static uint startlabel_x;
    public static uint startlabel_y;
    public static string MainMode;
    public static string PrevMode;
    public static float touchCount;
    public static float ErrorMessageTime;
    public string MoveDir;

    public static bool UITouch = false;
    public static bool InternetConnectState = false;
    public static bool internetCon = false;
    public static bool MoveCamera = false;
    public static bool SettingLabelPosition = false;
    public bool WantNoLabel = false;
    public static bool AnyError = false;
    public bool touchfinish = false;
    public bool alreadyPinchZoom = false;
    public static bool StartMiniMapDrag = false;
    public bool WriteLogStart = false;
    public bool alreadywaitingLog = false;
    public bool GuideCheck;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager");
        DontDestroyOnLoad(GM);
        UISetting();
        InternetConnectState = false;
        ScreenCapture.startflasheffect = false;
        // �ð� �ʱ�ȭ
        touchCount = 0;
        touchfinish = false;
        UITouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InternetConnectState == true)
        {
            internetCon = IsInternetConnected();

            if (CaptureMode.CheckStart == true)
            {
                functionorigin.capturemode.CheckInternet();
                CaptureMode.CheckStart = false;
            }
        }

        minimap.BasicMinimap();
        if (StartMiniMapDrag == true)
        {
            minimap.ButtonMinimap();
        }

        if (FunctionCustom.filterOff == false)
        {
            FunctionCustom.functionorigin.filterfunction.FilterBarScroll();

            if (FilterFunction.FilterBarMoveOn == true)
            {
                FunctionCustom.functionorigin.filterfunction.FilterBarOnOff();
            }
        }

        if (MoveCamera == true)
        {
            if (SceneManager.GetActiveScene().name.Contains("XRMode"))
            {
                xrmode.cctvcontrol.MoveCamera_Arrow();
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                clearmode.MoveCamera_Arrow();
            }
        }

        // �����ڸ��� ����
        // 10���̻� �ΰ� ��ġ�ϰ� ������ ����Ű���� Ȱ��ȭ
        // ��й�ȣ ������ �����ڸ��� ����
        if (entermode == true)
        {
            if (ManagerBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text == ManagerModePassword)
            {
                password = "";
                ManagerBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                SettingLabel();
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha0))
                    {
                        password += "0";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        password += "1";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        password += "2";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        password += "3";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        password += "4";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        password += "5";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha6))
                    {
                        password += "6";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha7))
                    {
                        password += "7";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha8))
                    {
                        password += "8";
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha9))
                    {
                        password += "9";
                    }
                    ManagerBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = password;
                }
            }
        }

        if (startcount == true)
        {
            if (entermode == false)
            {
                if (SettingLabelPosition == false)
                {
                    if ((int)count >= 10)
                    {
                        SetManagerPage();
                        entermode = true;
                    }
                    else if ((int)count < 10)
                    {
                        count += Time.deltaTime;
                    }
                }
            }
        }

        // ��ġ�� ���ϰ� �ְ� ���� ���� ����尡 �ƴ϶��
        // ��ġ ������ 5��(�������) �� �Ǵ� �ð��� ������ ��ȯ
        //if (touchfinish == false && SceneManager.GetActiveScene().name != "WaitingMode" && FunctionCustom.SetPayment == false)
        if (!(SceneManager.GetActiveScene().name == "WaitingMode" || SceneManager.GetActiveScene().name == "Loading"))
        {
            // ��ġ ���ϴ� �ð��� �����Ͽ� ������ ��ȯ�ϱ� ����
            if (Input.GetMouseButtonDown(0) || Input.touchCount >= 1)        // ���콺 Ŭ����
            {
                touchCount = 0;
                touchfinish = true;
                if (!SceneManager.GetActiveScene().name.Contains("VisitMode") && ErrorMessage.activeSelf && NoticeWindow.NoticeState == "ResetNotice")
                {
                    ErrorMessage.SetActive(false);
                }
            }
            else if (Input.GetMouseButtonUp(0) || Input.touchCount == 0)     // ���콺 ��ư���� ����
            {
                touchfinish = false;
            }

            if (touchCount < waitingTime + 10)
            {
                touchCount += Time.deltaTime;
            }

            if ((int)touchCount >= waitingTime)
            {
                //Readpulse = false;
                touchCount = 0;
                //PantiltOrigin.State = PantiltOrigin.OriginState.SetOrigin;
                //pantiltorigin.StartOrigin = false;
                //pantiltorigin.FinishOrigin = false;

                if(CCTVViewer.switchinglist == CCTVViewer.SwitchingList.Second)
                {
                    xrmode.cctvcontrol.monitor.SwitchingCCTV();
                }

                Debug.Log("today waiting");
                Loading.nextScene = "WaitingMode";
                SceneManager.LoadScene("WaitingMode");
            }
            else if ((int)touchCount > 49 && (int)touchCount < 60)
            {
                if (!Tip_Obj.activeSelf)
                {
                    NoticeWindow.NoticeWindowOpen("ResetNotice");
                }
            }
            else if ((int)touchCount >= 60 && (int)touchCount < 61)
            {
                if (ErrorMessage.activeSelf)
                {
                    ErrorMessage.gameObject.SetActive(false);
                }

                if (!Tip_Obj.gameObject.activeSelf)
                {
                    if (WriteLogStart == false)
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.ClickHomeBtn, "Reset All Function", GetType().ToString());
                        WriteLogStart = true;
                    }

                    if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                    {
                        gamemanager.xrmode.cctvcontrol.DigitalZoom("Origin");
                    }

                    StartCoroutine(Home_Btn());
                }
            }
        }

        if (Input.GetKeyDown("a"))
        {
            OnApplicationQuit();
        }
    }

    /// <summary>
    /// ������ �����Ҷ�
    /// </summary>
    private void OnApplicationQuit()
    {
        //if (gamemanager.setrecord.enabled == true)
        //{
        //    setrecord.alltimerecord.FinishContents();
        //}

        //if (FunctionCustom.SetPayment == true)
        //{
        //    paymentsystem.ubcn.SerialClose();
        //}
        //PanTiltControl.DisConnect();

        CCTVViewer.staticReadThread = null;
        CCTVViewer.EndThread = false;
        WriteLog(NormalLogCode.Connect_Pantilt, "Connect_Pantilt:Off", GetType().ToString());

        var processList = System.Diagnostics.Process.GetProcessesByName("SunAPITest");
        if (processList.Length != 0)
        {
            processList[0].Kill();
        }
        WriteLog(NormalLogCode.Connect_Camera, "Connect_Camera:Off", GetType().ToString());
        WriteLog(NormalLogCode.EndContents, "EndContents", GetType().ToString());
        AwakeOnce = false;
        WriteLog(NormalLogCode.Connect_SystemControl, "Connect_SystemControl_Off", GetType().ToString());
        Disconnect_Button();
    }

    public void UISetting()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            xrmode = GameObject.Find("XRMode").GetComponent<XRMode>();
            xrmode_manager = xrmode.GetComponent<XRMode_Manager>();
            labeldetail = xrmode.GetComponent<LabelDetail>();

            // XR���(WantNoLabel == false) �⺻�� : �̴ϸ�, �޴���, �׺���̼�â, Tip, Capture��ư Ȱ��ȭ/ ȭ��ǥ, ����â ��Ȱ��ȭ
            // Live���(WantNoLabel == true) �⺻�� : �̴ϸ�, �޴���, ȭ��ǥ, Tip Ȱ��ȭ/ ȭ��ǥ, �׺���̼�â, ����â ��Ȱ��ȭ
            UI_All.gameObject.SetActive(true);
            for (int index = 0; index < UI_All.transform.childCount; index++)
            {
                UI_All.transform.GetChild(index).gameObject.SetActive(false);
            }
            MenuBar.SetActive(true);
            if (AnyError == true)
            {
                MenuBar.gameObject.GetComponent<Image>().enabled = true;
                for (int index = 0; index < MenuBar.gameObject.transform.childCount; index++)
                {
                    MenuBar.transform.GetChild(index).gameObject.SetActive(true);
                }
            }
            Arrow.gameObject.transform.position = Arrowpos_extend;
            NavigationBar.gameObject.SetActive(true);
            LanguageBar.gameObject.SetActive(true);
            NavigationBar.transform.GetChild(0).gameObject.SetActive(true);
            MiniMap_Background.transform.parent.gameObject.SetActive(true);
            MiniMap_Background.gameObject.SetActive(true);
            ZoomObj.gameObject.SetActive(true);
            ErrorMessage.transform.parent.gameObject.SetActive(true);
            ErrorMessage.gameObject.SetActive(false);
            Homebtn.SetActive(true);
            //settingmanager.Setting_background.SetActive(false);

            if (WantNoLabel == false)   //XR���
            {
                xrmode.AllMapLabels.gameObject.SetActive(true);

                for (int index = 0; index < MenuBar.transform.GetChild(0).transform.childCount; index++)
                {
                    if (MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                    {
                        MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                for (int index = 0; index < MenuBar.transform.GetChild(1).transform.childCount; index++)
                {
                    if (MenuBar.transform.GetChild(1).GetChild(index).gameObject.activeSelf)
                    {
                        if (MenuBar.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                        {
                            MenuBar.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                }
                if (FunctionCustom.OnceLiveXR == false)
                {
                    MenuBar.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (FunctionCustom.OnceLiveXR == true)
                {
                    MenuBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                //MenuBar.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 0);
                allbar.NaviOn = true;
            }
            else if (WantNoLabel == true)       //Live���
            {
                for (int index = 0; index < MenuBar.transform.GetChild(0).transform.childCount; index++)
                {
                    if (MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                    {
                        MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                for (int index = 0; index < MenuBar.transform.GetChild(1).transform.childCount; index++)
                {
                    if (MenuBar.transform.GetChild(1).GetChild(index).gameObject.activeSelf)
                    {
                        if (MenuBar.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                        {
                            MenuBar.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                }
                MenuBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                if (xrmode.AllMapLabels.transform.parent.childCount != 1)
                {
                    xrmode.AllMapLabels.transform.parent.GetChild(1).gameObject.SetActive(false);
                }
                xrmode.AllMapLabels.gameObject.SetActive(false);
                Arrow.SetActive(true);

                if (allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose)
                {
                    allbar.navi_t = 0;
                    allbar.NaviOn = true;
                    allbar.moveNavi = true;
                }

                Invoke("SeeNavibar", 0.3f);
            }

            //label.SelectCategortButton(CategoryContent.transform.GetChild(0).gameObject);
            minimap.SettingHotspot();
            TipOpen();
        }
        else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            clearmode = GameObject.Find("ClearMode").GetComponent<ClearMode>();
            labeldetail = clearmode.GetComponent<LabelDetail>();
            UI_All.gameObject.SetActive(true);
            for (int index = 0; index < UI_All.transform.childCount; index++)
            {
                UI_All.transform.GetChild(index).gameObject.SetActive(false);
            }
            MenuBar.SetActive(true);
            if (AnyError == true)
            {
                MenuBar.gameObject.GetComponent<Image>().enabled = true;
                for (int index = 0; index < MenuBar.gameObject.transform.childCount; index++)
                {
                    MenuBar.transform.GetChild(index).gameObject.SetActive(true);
                }
            }
            Arrow.gameObject.transform.position = Arrowpos_extend;
            NavigationBar.gameObject.SetActive(true);
            LanguageBar.gameObject.SetActive(true);
            NavigationBar.transform.GetChild(0).gameObject.SetActive(true);
            MiniMap_Background.transform.parent.gameObject.SetActive(true);
            MiniMap_Background.gameObject.SetActive(true);
            ZoomObj.gameObject.SetActive(true);
            ErrorMessage.transform.parent.gameObject.SetActive(true);

            for (int index = 0; index < MenuBar.transform.GetChild(0).transform.childCount; index++)
            {
                if (MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                {
                    MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            for (int index = 0; index < MenuBar.transform.GetChild(1).transform.childCount; index++)
            {
                if (MenuBar.transform.GetChild(1).GetChild(index).gameObject.activeSelf)
                {
                    if (MenuBar.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                    {
                        MenuBar.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
            MenuBar.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //BackGround.transform.parent.gameObject.SetActive(true);
            allbar.NaviRect.sizeDelta = new Vector2(AllBarOnOff.barOpen, 1080);
            //Tip_Obj.SetActive(true);
            //label.SelectCategortButton(CategoryContent.transform.GetChild(0).gameObject);
            //minimap.SettingHotspot();
            TipOpen();
            Invoke("SeeNavibar", 0.3f);
        }
        else if (SceneManager.GetActiveScene().name.Contains("TelescopeMode"))
        {
            //telescopemode = GameObject.Find("TelescopeMode").GetComponent<TelescopeMode>();
            UI_All.gameObject.SetActive(true);
            for (int index = 0; index < UI_All.transform.childCount; index++)
            {
                UI_All.transform.GetChild(index).gameObject.SetActive(false);
            }
            MenuBar.gameObject.SetActive(true);
            MenuBar.GetComponent<Image>().enabled = true;
            Arrow.gameObject.SetActive(true);
            MiniMap_Background.transform.parent.gameObject.SetActive(true);
            MiniMap_Background.gameObject.SetActive(true);
            Arrow.gameObject.transform.position = Arrowpos_normal;
            BackGround.transform.parent.gameObject.SetActive(true);
            settingmanager.Setting_background.SetActive(false);
            ErrorMessage.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Loading":
                    if (loading == null)
                    {
                        loading = GameObject.Find("Loading").GetComponent<Loading>();
                    }

                    switch (ContentsInfo.ContentsName)
                    {
                        case "GoldSunset":
                            loading.CustmMode.sprite = loading.Tourism;
                            break;
                    }

                    UI_All.gameObject.SetActive(false);
                    break;
                case "WaitingMode":
                    if (AnyError == false)
                    {
                        UI_All.gameObject.SetActive(false);
                        ErrorMessage.SetActive(false);
                    }
                    else if (AnyError == true)
                    {
                        UI_All.gameObject.SetActive(true);
                        for (int index = 0; index < UI_All.transform.childCount; index++)
                        {
                            UI_All.transform.GetChild(index).gameObject.SetActive(false);
                        }
                        MenuBar.gameObject.SetActive(true);
                        MenuBar.gameObject.GetComponent<Image>().enabled = false;
                        for (int index = 0; index < MenuBar.gameObject.transform.childCount; index++)
                        {
                            MenuBar.transform.GetChild(index).gameObject.SetActive(false);
                        }
                        NoticeWindow.NoticeWindowOpen("ErrorMessage");
                    }
                    break;
            }
        }
        announcemode.gameObject.SetActive(true);
        FunctionCustom.SetContentsFunc();
    }

    public void SeeNavibar()
    {
        NavigationBar.gameObject.SetActive(true);
    }

    public void Menu(GameObject btn)
    {
        switch (btn.name)
        {
            case "LiveMode":
                if (FunctionCustom.View360 == true)
                {
                    if (functionorigin.view360.obj360.transform.parent.gameObject.activeSelf)
                    {
                        functionorigin.view360.Close360();
                    }
                }

                if (FunctionCustom.PastMode == true)
                {
                    if (functionorigin.pastcontents.obj360.transform.parent.gameObject.activeSelf)
                    {
                        functionorigin.pastcontents.Close360();
                    }
                }

                if (FunctionCustom.filterOff == false)
                {
                    FunctionCustom.functionorigin.FilterReset();
                }

                if (FunctionCustom.GuideMode == true)
                {
                    if (functionorigin.guidemode.GuideObj.activeSelf)
                    {
                        functionorigin.guidemode.GuideObj.SetActive(false);
                    }
                }
                WantNoLabel = true;

                if (SelfiFunction.selfimode == true)
                {
                    if (CaptureSelfi.activeSelf)
                    {
                        CaptureSelfi.SetActive(false);
                    }
                    else if (!CaptureSelfi.activeSelf)
                    {
                        if (Selfi_Obj.activeSelf)
                        {
                            if (!selfifunction.FinalCam.gameObject.activeSelf)
                            {
                                NoticeWindow.NoticeWindowOpen("StopSelfi");
                            }
                            else if (selfifunction.FinalCam.gameObject.activeSelf)
                            {
                                NoticeWindow.NoticeWindowOpen("StopSelfiCustom");
                            }
                        }
                    }
                }

                if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    xrmode.Resetothers();
                    //// ����â ��Ȱ��ȭ
                    //if (allbar.LangRect.sizeDelta.x > AllBarOnOff.barClose)
                    //{
                    //    allbar.langnavi_t = 0;
                    //    allbar.langNaviOn = true;
                    //    allbar.movelangNavi = true;
                    //}

                    // �޴����� �������ܿ��� Live��� ��Ȱ��ȭ, AR��� Ȱ��ȭ
                    btn.transform.GetChild(0).gameObject.SetActive(true);
                    MenuBar.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);

                    if (FunctionCustom.OnceLiveXR == false)     // ���̺���, xr��� �и�
                    {
                        if (xrmode.AllMapLabels.activeSelf)
                        {
                            xrmode.AllMapLabels.SetActive(false);
                            if (xrmode.AllMapLabels.transform.parent.childCount != 0)
                            {
                                xrmode.AllMapLabels.transform.parent.GetChild(1).gameObject.SetActive(false);
                            }
                            gamemanager.announcemode.OpenMode("Live");
                        }

                        // �׺���̼� â ��Ȱ��ȭ(�����忡���� �׺���̼�â ������� ����)
                        if (allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose)
                        {
                            allbar.navi_t = 0;
                            allbar.NaviOn = true;
                            allbar.moveNavi = true;
                        }
                    }
                    else if (FunctionCustom.OnceLiveXR == true)        // ���̺���, xr��� ��ġ��
                    {
                        if (!xrmode.AllMapLabels.activeSelf)
                        {
                            xrmode.XRToggle.transform.GetChild(0).gameObject.SetActive(false);
                        }
                        else if (xrmode.AllMapLabels.activeSelf)
                        {
                            Menu(gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(1).gameObject);
                            xrmode.XRToggle.transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
                else if (!SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    if (SceneManager.GetActiveScene().name.Contains("VisitMode") && visitmanager.DrawVisit.activeSelf)
                    {
                        NoticeWindow.NoticeWindowOpen("VisitCancel");
                    }
                    else
                    {
                        // �׺���̼� â ��Ȱ��ȭ(�����忡���� �׺���̼�â ������� ����)
                        if (allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose)
                        {
                            allbar.navi_t = 0;
                            allbar.NaviOn = true;
                            allbar.moveNavi = true;
                        }

                        // ����â ��Ȱ��ȭ
                        if (allbar.LangRect.sizeDelta.x > AllBarOnOff.barClose)
                        {
                            allbar.langnavi_t = 0;
                            allbar.langNaviOn = true;
                            allbar.movelangNavi = true;
                        }
                        Arrow.SetActive(true);
                        Arrow.transform.position = Arrowpos_extend;

                        // �޴����� �������ܿ��� Live��� ��Ȱ��ȭ, AR��� Ȱ��ȭ
                        for (int index = 0; index < MenuBar.transform.GetChild(0).transform.childCount; index++)
                        {
                            if (MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                            {
                                MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            }
                        }
                        uilang.SceneChagneSetOrigin();
                        try
                        {
                            btn.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        catch
                        {

                        }
                        functionorigin.capturemode.CaptureEndCamera();
                        // �����̼� ���� ����
                        label.Narration.Stop();

                        WriteLog(NormalLogCode.ChangeMode, "ChangeMode : Start(" + PrevMode + " - " + "LiveMode)", GetType().ToString());

                        Loading.nextScene = btn.name;
                        SceneManager.LoadScene("Loading");
                    }
                }
                break;
            case "XRMode":
                if (FunctionCustom.View360 == true)
                {
                    if (functionorigin.view360.obj360.transform.parent.gameObject.activeSelf)
                    {
                        functionorigin.view360.Close360();
                    }
                }

                if (FunctionCustom.PastMode == true)
                {
                    if (functionorigin.pastcontents.obj360.transform.parent.gameObject.activeSelf)
                    {
                        functionorigin.pastcontents.Close360();
                    }
                }

                if (FunctionCustom.filterOff == false)
                {
                    FunctionCustom.functionorigin.FilterReset();
                }

                if (FunctionCustom.GuideMode == true)
                {
                    if (functionorigin.guidemode.GuideObj.activeSelf)
                    {
                        functionorigin.guidemode.GuideObj.SetActive(false);
                    }
                }

                if (SelfiFunction.selfimode == true)
                {
                    if (CaptureSelfi.activeSelf)
                    {
                        CaptureSelfi.SetActive(false);
                    }
                    else if (!CaptureSelfi.activeSelf)
                    {
                        if (Selfi_Obj.activeSelf)
                        {
                            if (!selfifunction.FinalCam.gameObject.activeSelf)
                            {
                                NoticeWindow.NoticeWindowOpen("StopSelfi");
                            }
                            else if (selfifunction.FinalCam.gameObject.activeSelf)
                            {
                                NoticeWindow.NoticeWindowOpen("StopSelfiCustom");
                            }
                        }
                    }
                }

                if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    if (!Tip_Obj.activeSelf)
                    {
                        if (ModeActive[1] == true)      // AR��尡 Ȱ��ȭ�Ǿ��ִٸ�
                        {
                            // �׺���̼�â Ȱ��ȭ
                            if (allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose)
                            {
                                allbar.navi_t = 0;
                                allbar.NaviOn = true;
                                allbar.moveNavi = true;
                            }
                            else if (allbar.NaviRect.sizeDelta.x < AllBarOnOff.barOpen)
                            {
                                allbar.navi_t = 0;
                                allbar.moveNavi = true;
                                allbar.NaviOn = false;
                            }

                            // ����â ��Ȱ��ȭ
                            if (allbar.LangRect.sizeDelta.x > AllBarOnOff.barClose)
                            {
                                allbar.langnavi_t = 0;
                                allbar.langNaviOn = true;
                                allbar.movelangNavi = true;
                            }

                            if (FunctionCustom.OnceLiveXR == false)
                            {
                                btn.transform.GetChild(0).gameObject.SetActive(true);
                                MenuBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            }
                            else if (FunctionCustom.OnceLiveXR == true)
                            {
                                MenuBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                            }

                            // AR��� ���� ��Ȱ��ȭ�Ǿ��ִٸ� �� Ȱ��ȭ
                            if (!xrmode.AllMapLabels.gameObject.activeSelf)
                            {
                                xrmode.AllMapLabels.gameObject.SetActive(true);
                                if (xrmode.AllMapLabels.transform.parent.childCount != 1)
                                {
                                    xrmode.AllMapLabels.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                                }

                                gamemanager.announcemode.OpenMode("XR");
                            }

                            WantNoLabel = false;
                        }
                        else if (ModeActive[1] == false)        //AR��尡 ��Ȱ��ȭ �Ǿ��ִٸ� �����޼��� Ȱ��ȭ�ϰ� ���� ���� Live �����ϱ�
                        {
                            NoticeWindow.NoticeWindowOpen("ErrorMessage");
                            //ErrorMessage.gameObject.SetActive(true);
                            Menu(MenuBar.transform.GetChild(0).transform.GetChild(0).gameObject);
                        }
                    }
                    else if (Tip_Obj.activeSelf)
                    {
                        TipClose();
                    }
                }
                else if (!SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    if (SceneManager.GetActiveScene().name.Contains("VisitMode") && visitmanager.DrawVisit.activeSelf)
                    {
                        NoticeWindow.NoticeWindowOpen("VisitCancel");
                    }
                    else
                    {
                        // �׺���̼�â Ȱ��ȭ
                        if (allbar.NaviRect.sizeDelta.x < AllBarOnOff.barOpen)
                        {
                            allbar.navi_t = 0;
                            allbar.moveNavi = true;
                            allbar.NaviOn = false;
                        }

                        // ����â ��Ȱ��ȭ
                        if (allbar.LangRect.sizeDelta.x > AllBarOnOff.barClose)
                        {
                            allbar.langnavi_t = 0;
                            allbar.langNaviOn = true;
                            allbar.movelangNavi = true;
                        }

                        // �޴����� �������ܿ��� Live��� ��Ȱ��ȭ, AR��� Ȱ��ȭ
                        for (int index = 0; index < MenuBar.transform.GetChild(0).transform.childCount; index++)
                        {
                            if (MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                            {
                                MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            }
                        }
                        uilang.SceneChagneSetOrigin();
                        btn.transform.GetChild(0).gameObject.SetActive(true);
                        functionorigin.capturemode.CaptureEndCamera();
                        // �����̼� ���� ����
                        label.Narration.Stop();
                        WantNoLabel = false;

                        WriteLog(NormalLogCode.ChangeMode, "ChangeMode : Start(" + PrevMode + " - " + "XRMode)", GetType().ToString());

                        Loading.nextScene = btn.name;
                        SceneManager.LoadScene("Loading");
                    }
                }
                break;
            case "ClearMode":
                if (FunctionCustom.View360 == true)
                {
                    if (functionorigin.view360.obj360.transform.parent.gameObject.activeSelf)
                    {
                        functionorigin.view360.Close360();
                    }
                }

                if (FunctionCustom.PastMode == true)
                {
                    if (functionorigin.pastcontents.obj360.transform.parent.gameObject.activeSelf)
                    {
                        functionorigin.pastcontents.Close360();
                    }
                }

                if (FunctionCustom.filterOff == false)
                {
                    FunctionCustom.functionorigin.FilterReset();
                }

                if (FunctionCustom.GuideMode == true)
                {
                    if (functionorigin.guidemode.GuideObj.activeSelf)
                    {
                        functionorigin.guidemode.GuideObj.SetActive(false);
                    }
                }

                if (SelfiFunction.selfimode == true)
                {
                    if (CaptureSelfi.activeSelf)
                    {
                        CaptureSelfi.SetActive(false);
                        CaptureBtn.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (!CaptureSelfi.activeSelf)
                    {
                        if (Selfi_Obj.activeSelf)
                        {
                            selfifunction.FinishSelfi();
                            CaptureBtn.transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                }

                if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    if (FunctionCustom.ChangeSeason == true)
                    {
                        if (FunctionCustom.functionorigin.SeasonPano.seasonBar.transform.localPosition.y < 720)
                        {
                            FunctionCustom.functionorigin.SeasonPano.SeasonChange();
                        }
                    }

                    // �׺���̼�â Ȱ��ȭ
                    if (allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose)
                    {
                        allbar.navi_t = 0;
                        allbar.NaviOn = true;
                        allbar.moveNavi = true;
                    }
                    else if (allbar.NaviRect.sizeDelta.x < AllBarOnOff.barOpen)
                    {
                        allbar.navi_t = 0;
                        allbar.moveNavi = true;
                        allbar.NaviOn = false;
                    }

                    // ����â ��Ȱ��ȭ
                    if (allbar.LangRect.sizeDelta.x > AllBarOnOff.barClose)
                    {
                        allbar.langnavi_t = 0;
                        allbar.langNaviOn = true;
                        allbar.movelangNavi = true;
                    }

                    btn.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (!SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    if (SceneManager.GetActiveScene().name.Contains("VisitMode") && visitmanager.DrawVisit.activeSelf)
                    {
                        NoticeWindow.NoticeWindowOpen("VisitCancel");
                    }
                    else
                    {
                        // �׺���̼�â Ȱ��ȭ
                        if (allbar.NaviRect.sizeDelta.x < AllBarOnOff.barOpen)
                        {
                            allbar.navi_t = 0;
                            allbar.moveNavi = true;
                            allbar.NaviOn = false;
                        }

                        // ����â ��Ȱ��ȭ
                        if (allbar.LangRect.sizeDelta.x > AllBarOnOff.barClose)
                        {
                            allbar.langnavi_t = 0;
                            allbar.langNaviOn = true;
                            allbar.movelangNavi = true;
                        }

                        // �޴����� �������ܿ��� Live��� ��Ȱ��ȭ, AR��� Ȱ��ȭ
                        for (int index = 0; index < MenuBar.transform.GetChild(0).transform.childCount; index++)
                        {
                            if (MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                            {
                                MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            }
                        }
                        uilang.SceneChagneSetOrigin();
                        btn.transform.GetChild(0).gameObject.SetActive(true);
                        functionorigin.capturemode.CaptureEndCamera();
                        // �����̼� ���� ����
                        label.Narration.Stop();

                        WriteLog(NormalLogCode.ChangeMode, "ChangeMode : Start(" + PrevMode + " - " + "ClearMode)", GetType().ToString());

                        Loading.nextScene = btn.name;
                        SceneManager.LoadScene("Loading");
                    }
                }
                break;
            case "PastMode":
                functionorigin.pastcontents.Close360();
                for (int index = 0; index < MenuBar.transform.GetChild(0).transform.childCount; index++)
                {
                    MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
                uilang.SceneChagneSetOrigin();
                btn.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "GuideMode":
                functionorigin.guidemode.CloaseGuide();
                for (int index = 0; index < MenuBar.transform.GetChild(0).transform.childCount; index++)
                {
                    MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
                uilang.SceneChagneSetOrigin();
                btn.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Language":
                allbar.langnavi_t = 0;
                if (allbar.LangRect.sizeDelta.x > AllBarOnOff.barClose)        // ���� ��Ȱ��ȭ
                {
                    btn.transform.GetChild(0).gameObject.SetActive(false);
                    allbar.langnavi_t = 0;
                    allbar.langNaviOn = true;
                    allbar.movelangNavi = true;
                }
                else if (allbar.LangRect.sizeDelta.x < AllBarOnOff.barOpen)      // ���� Ȱ��ȭ
                {
                    btn.transform.GetChild(0).gameObject.SetActive(true);
                    allbar.langnavi_t = 0;
                    allbar.movelangNavi = true;
                    allbar.langNaviOn = false;
                }
                break;
            case "Tip":
                if (!SceneManager.GetActiveScene().name.Contains("Visitmode"))
                {
                    if (!Tip_Obj.activeSelf)        // Tip �̹����� ��Ȱ��ȭ���¸� Ȱ��ȭ
                    {
                        btn.transform.GetChild(0).gameObject.SetActive(true);
                        AnnounceMode.DontAnnounce = true;
                        TipOpen();
                    }
                    else if (Tip_Obj.activeSelf)      // Tip �̹����� Ȱ��ȭ ���¸� ��Ȱ��ȭ
                    {
                        btn.transform.GetChild(0).gameObject.SetActive(false);
                        TipClose();
                    }
                }
                break;
            case "Capture":
                if (CaptureBtn.activeSelf && (SceneManager.GetActiveScene().name.Contains("XRMode") || SceneManager.GetActiveScene().name.Contains("ClearMode")))
                {
                    ScreenCapture.startflasheffect = false;
                    if (SelfiFunction.selfimode == false)
                    {
                        functionorigin.capturemode.CaptureCamera();
                        CaptureBtn.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else if (SelfiFunction.selfimode == true)
                    {
                        if (CaptureSelfi.activeSelf)
                        {
                            CaptureSelfi.SetActive(false);
                        }
                        else if (!CaptureSelfi.activeSelf)
                        {
                            if (Selfi_Obj.activeSelf)
                            {
                                if (!selfifunction.FinalCam.gameObject.activeSelf)
                                {
                                    NoticeWindow.NoticeWindowOpen("StopSelfi");
                                }
                                else if (selfifunction.FinalCam.gameObject.activeSelf)
                                {
                                    NoticeWindow.NoticeWindowOpen("StopSelfiCustom");
                                }
                            }
                            else if (!Selfi_Obj.activeSelf)
                            {
                                CaptureSelfi.SetActive(true);
                                CaptureBtn.transform.GetChild(0).gameObject.SetActive(true);
                            }
                        }
                    }
                }
                else if (CaptureBtn.activeSelf && SceneManager.GetActiveScene().name.Contains("VisitMode"))
                {
                    NoticeWindow.NoticeWindowOpen("VisitClickCap");
                }
                break;
            case "Setting":
                if (Wcount == 2)
                {
                    settingmanager.Setting_background.SetActive(true);
                    settingmanager.PWError.SetActive(false);
                    btn.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    if (touchCount > 0.5f && touchCount < 1f)
                    {
                        Wcount = 0;
                    }
                    else
                    {
                        Wcount++;
                    }
                }
                break;
            case "Navi_Close":      // �׺���̼� â�� x ��ư �����ϸ� �׺���̼�â ��Ȱ��ȭ
                allbar.navi_t = 0;
                allbar.moveNavi = true;
                break;
            case "LangNavi_Close":      // ���� â�� x ��ư �����ϸ� ���� ��Ȱ��ȭ
                allbar.langnavi_t = 0;
                allbar.movelangNavi = true;
                LanguageBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case "Visit":
                if (!SceneManager.GetActiveScene().name.Contains("VisitMode"))
                {
                    if (SelfiFunction.selfimode == true)
                    {
                        if (CaptureSelfi.activeSelf)
                        {
                            CaptureSelfi.SetActive(false);
                        }
                        else if (!CaptureSelfi.activeSelf)
                        {
                            if (Selfi_Obj.activeSelf)
                            {
                                if (!selfifunction.FinalCam.gameObject.activeSelf)
                                {
                                    NoticeWindow.NoticeWindowOpen("StopSelfi");
                                }
                                else if (selfifunction.FinalCam.gameObject.activeSelf)
                                {
                                    NoticeWindow.NoticeWindowOpen("StopSelfiCustom");
                                }
                            }
                        }
                    }
                    label.Narration.Stop();
                    UI_All.SetActive(false);
                    //Camera.main.gameObject.SetActive(false);
                    for (int i = 0; i < MenuBar.transform.GetChild(0).childCount; i++)
                    {
                        try
                        {
                            MenuBar.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(false);
                        }
                        catch { }
                    }
                    for (int i = 0; i < MenuBar.transform.GetChild(1).childCount; i++)
                    {
                        try
                        {
                            MenuBar.transform.GetChild(1).GetChild(i).GetChild(0).gameObject.SetActive(false);
                        }
                        catch { }
                    }
                    uilang.SceneChagneSetOrigin();
                    btn.transform.GetChild(0).gameObject.SetActive(true);
                    functionorigin.capturemode.CaptureEndCamera();

                    WriteLog(NormalLogCode.ChangeMode, "ChangeMode : Start(" + PrevMode + " - " + "VisitMode)", GetType().ToString());

                    Loading.nextScene = "VisitMode";
                    SceneManager.LoadScene("Loading");
                }
                break;
            case "TourismMode":
                if (SceneManager.GetActiveScene().name.Contains("TourismMode"))
                {
                    gamemanager.label.Narration.Stop();
                    if (ContentsInfo.ContentsName == "GoldSunset")
                    {
                        tourLite.AllRoute.SetActive(true);
                        tourLite.AnnounceRoute.SetActive(false);
                    }
                }
                else if (!SceneManager.GetActiveScene().name.Contains("TourismMode"))
                {
                    WriteLog(NormalLogCode.ChangeMode, "ChangeMode : Start(" + PrevMode + " - " + "TourismMode)", GetType().ToString());
                    TourismModeBtn.transform.GetChild(0).gameObject.SetActive(true);

                    Loading.nextScene = "TourismMode";
                    SceneManager.LoadScene("Loading");
                }
                break;
        }
    }

    /// <summary>
    /// �׺���̼�â���� �� �����ϸ� ��忡 ���� ������ �� ����
    /// </summary>
    /// <param name="label"></param>
    public void Navigation(GameObject label)
    {
        if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            gamemanager.clearmode.SelectNaviLabel(label);
            gamemanager.clearmode.WriteLabelLog(label);
        }
        else if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (SettingLabelPosition == true)
            {
                xrmode_manager.ChangePositionLabel(label);
            }
            else if (SettingLabelPosition == false)
            {
                xrmode.SelectNaviLabel(label);
            }
        }
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    /// <param name="btn"></param>
    public void ChangeLanguage(GameObject btn)
    {
        switch (btn.name)
        {
            case "Korea":
                currentLang = Language_enum.Korea;
                uilang.SelectKorea();
                Tip_Obj.GetComponent<Image>().sprite = label.Tip_K;
                break;
            case "English":
                currentLang = Language_enum.English;
                uilang.NotSelectKorea();
                Tip_Obj.GetComponent<Image>().sprite = label.Tip_E;
                break;
            case "Chinese":
                currentLang = Language_enum.Chinese;
                uilang.NotSelectKorea();
                Tip_Obj.GetComponent<Image>().sprite = label.Tip_C;
                break;
            case "Japanese":
                currentLang = Language_enum.Japanese;
                uilang.NotSelectKorea();
                Tip_Obj.GetComponent<Image>().sprite = label.Tip_J;
                break;
        }

        WriteLog(NormalLogCode.ChangeLanguage, "ChangeLanguage : " + currentLang, GetType().ToString());

        if (SceneManager.GetActiveScene().name != "Loading")
        {
            label.SelectLanguageButton();
            //if (SceneManager.GetActiveScene().name.Contains("XRMode") || SceneManager.GetActiveScene().name.Contains("ClearMode"))
            //{
            //    labelmake.MapLabel();
            //    labelmake.NavigationText();
            //}

            if (SceneManager.GetActiveScene().name.Contains("Tourism"))
            {
                tourLite.ChangeLang();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Loading")
        {
            label.SelectCategortButton(CategoryContent.transform.GetChild(0).gameObject);
        }

        if (FunctionCustom.GuideMode == true)
        {
            if (functionorigin.guidemode.GuideObj.activeSelf)
            {
                functionorigin.guidemode.GuideLanguage();
            }
        }

        allbar.langnavi_t = 0;
        allbar.langNaviOn = true;
        allbar.movelangNavi = true;
        LanguageBtn.transform.GetChild(0).gameObject.SetActive(false);

        if (Tip_Obj.activeSelf)
        {
            TipOpen();
        }
    }

    public void SelectCapSel(GameObject btn)
    {
        switch (btn.name)
        {
            case "Basic":
                functionorigin.capturemode.CaptureCamera();
                SelfiFunction.selfimode = false;
                CaptureSelfi.SetActive(false);
                break;
            case "Selfi":
                selfifunction.StartSelfi();
                CaptureSelfi.SetActive(false);
                Selfi_Obj.SetActive(true);
                if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    Selfi_Obj.transform.position = xrmode.CameraWindow.transform.position;
                    //selfifunction.SelfiPhoto.transform.parent.gameObject.transform.localPosition = new Vector3(-960,-540,927);

                    //selfifunction.SelfiCam.orthographicSize = 1150;
                    selfifunction.SelfiCam.orthographicSize = 540;

                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    Selfi_Obj.transform.position = clearmode.CameraWindow.transform.position;
                    //selfifunction.SelfiPhoto.transform.parent.gameObject.transform.localPosition = new Vector3(-960, -540, 927);
                    //selfifunction.FinalCam.transform.position = clearMode.CameraWindow.transform.position;
                    selfifunction.SelfiCam.orthographicSize = 1560;
                }

                SelfiFunction.selfimode = true;
                break;
            case "Close":
                CaptureSelfi.SetActive(false);
                CaptureBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
        }
    }

    public void TipOpen()
    {
        Tipbtn.transform.GetChild(0).gameObject.SetActive(true);

        if (!Tip_Obj.activeSelf)
        {
            Tip_Obj.SetActive(true);
        }

        //NavigationBar.gameObject.SetActive(false);
        //Arrow.gameObject.SetActive(false);
        //MiniMap_Background.transform.parent.gameObject.SetActive(false);

        //if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        //{
        //    xrmode.AllMapLabels.gameObject.SetActive(false);
        //}
        //else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        //{
        //    clearmode.AllMapLabels.SetActive(false);
        //}
    }

    public void TipClose()      // �ٽ�
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            //NavigationBar.gameObject.SetActive(true);
            //MiniMap_Background.transform.parent.gameObject.SetActive(true);
            //Arrow.SetActive(false);
            //Arrow.transform.position = Arrowpos_extend;
            if (WantNoLabel == false)
            {
                //if (!see360.obj360.transform.parent.gameObject.activeSelf)
                {
                    allbar.navi_t = 0;
                    allbar.NaviOn = false;
                    allbar.moveNavi = true;
                    xrmode.AllMapLabels.gameObject.SetActive(true);
                }
                if (AnnounceMode.DontAnnounce == false)
                {
                    gamemanager.announcemode.OpenMode("XR");
                }
                else if (AnnounceMode.DontAnnounce == true)
                {
                    AnnounceMode.DontAnnounce = false;
                }
            }
            else if (WantNoLabel == true)
            {
                allbar.navi_t = 0;
                allbar.NaviOn = true;
                allbar.moveNavi = true;
                xrmode.AllMapLabels.gameObject.SetActive(false);

                if (AnnounceMode.DontAnnounce == false)
                {
                    gamemanager.announcemode.OpenMode("Live");
                }
                else if (AnnounceMode.DontAnnounce == true)
                {
                    AnnounceMode.DontAnnounce = false;
                }
            }

            if (CheckingView.CheckingHour == false && (int)touchCount >= 60)
            {
                CheckingView.StartChecking = true;
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            //NavigationBar.gameObject.SetActive(true);
            //Arrow.SetActive(false);
            //MiniMap_Background.transform.parent.gameObject.SetActive(true);

            //clearmode.AllMapLabels.gameObject.SetActive(true);
            //for (int i = 0; i < clearmode.AllMapLabels.transform.childCount; i++)
            //{
            //    clearmode.AllMapLabels.transform.GetChild(0).GetComponent<Button>().enabled = true;
            //}
            //if (see360.obj360.transform.parent.gameObject.activeSelf)
            //{
            //    clearmode.AllMapLabels.gameObject.SetActive(false);
            //}

            allbar.navi_t = 0;
            allbar.NaviOn = false;
            allbar.moveNavi = true;

            if (AnnounceMode.DontAnnounce == false)
            {
                gamemanager.announcemode.OpenMode("Clear");
            }
            else if (AnnounceMode.DontAnnounce == true)
            {
                AnnounceMode.DontAnnounce = false;
            }

            if (CheckingView.CheckingHour == false && (int)touchCount >= 60)
            {
                CheckingView.StartChecking = true;
            }
        }
        Tipbtn.transform.GetChild(0).gameObject.SetActive(false);
        Tip_Obj.SetActive(false);       // Tip ��Ȱ��ȭ
    }


    /// <summary>
    /// ��ư Ŭ���ϸ� ȿ���� ���
    /// </summary>
    public void ButtonClickSound()
    {
        ButtonEffect.clip = ButtonSound;
        ButtonEffect.Play();
    }

    /// <summary>
    /// �Ŵ������� ���� ���� �ΰ� 10�� ��ġ�ϸ� Ű���� Ȱ��ȭ
    /// </summary>
    public void SetManagerPage()
    {
        System.Diagnostics.Process ps = new System.Diagnostics.Process();
        ps.StartInfo.FileName = "osk.exe";
        password = "";
        ManagerBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        ps.Start();
    }

    /// <summary>
    /// ���� ��尡 AR��� �� ��쿡�� �Ŵ������ ����
    /// </summary>
    public void CountEnterManager()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            startcount = true;
            entermode = false;
        }
    }

    /// <summary>
    /// �Ŵ������ ����
    /// </summary>
    public void CountFinishManager()
    {
        count = 0;
        startcount = false;
    }

    /// <summary>
    /// �Ŵ�����忡 �� ���, �ȵ� ��쿡 ���� ��ũ��Ʈ Ȱ��ȭ/��Ȱ��ȭ
    /// </summary>
    public void SettingLabel()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode") && SettingLabelPosition == false)
        {
            count = 0;
            entermode = false;
        }
        SettingLabelPosition = true;
        if (SettingLabelPosition == true)       // �Ŵ������� �� ���
        {
            xrmode.enabled = false;
            xrmode_manager.enabled = true;
            xrmode_manager.ManagerMode.gameObject.SetActive(true);

            if (FunctionCustom.OnceLiveXR == true)
            {
                // �׺���̼�â Ȱ��ȭ
                if (allbar.NaviRect.sizeDelta.x < AllBarOnOff.barOpen)
                {
                    allbar.navi_t = 0;
                    allbar.moveNavi = true;
                    allbar.NaviOn = false;
                }
            }
        }
        else if (SettingLabelPosition == false)     // �Ŵ�����尡 �ƴ� ���
        {
            xrmode.enabled = true;
            xrmode_manager.ManagerMode.gameObject.SetActive(false);
            xrmode_manager.enabled = false;
        }
        //SceneManager.LoadScene("ARMode_" + ContentsName);
    }

    public void ResetPosition()
    {
        gamemanager.WriteLog(LogSendServer.NormalLogCode.ClickHomeBtn, "HomeButton Click", GetType().ToString());

        StartCoroutine(Home_Btn());
    }

    IEnumerator Home_Btn()
    {
        if (Selfi_Obj.activeSelf)
        {
            if (!ErrorMessage.activeSelf)
            {
                if (!selfifunction.FinalCam.gameObject.activeSelf)
                {
                    NoticeWindow.NoticeWindowOpen("StopSelfi");
                }
                else if (selfifunction.FinalCam.gameObject.activeSelf)
                {
                    NoticeWindow.NoticeWindowOpen("StopSelfiCustom");
                }
                touchCount = 0;
                yield break;
            }
            else if (ErrorMessage.activeSelf)
            {
                ErrorMessage.SetActive(false);
                selfifunction.FinishSelfi();
                Selfi_Obj.SetActive(false);
                CaptureBtn.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            //PanTiltControl.Stop();
            yield return new WaitForSeconds(0.1f);
            xrmode.Resetothers();
            //xrmode.labeldetail.SelectCloseButton();
            if (labeldetail.SeeDetail_Open == true)
            {
                xrmode.labeldetail.CloseDetailWindow();
            }
            //xrmode.labeldetail.CancelSelect();

            for (int index = 0; index < xrmode.AllMapLabels.transform.childCount; index++)
            {
                xrmode.AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
            }

            //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Fast);
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Fast);
            //gamemanager.speed_enum = GameManager.Speed_enum.fast;
            //PanTiltControl.SetPulse((uint)startlabel_x, (uint)startlabel_y);
            xrmode.cctvcontrol.GOPanTilt((uint)startlabel_x, (uint)startlabel_y);

            if (!Tip_Obj.activeSelf && ContentsInfo.ContentsName != "Woosuk")        // Tip �̹����� ��Ȱ��ȭ���¸� Ȱ��ȭ
            {
                Tipbtn.transform.GetChild(0).gameObject.SetActive(true);
                Tip_Obj.SetActive(true);
                TipOpen();
            }
            //PinchZoomInOut.ZoomMove = true;
            //PinchZoomInOut.ZoomIN = false;

            MiniMap_CameraGuide.gameObject.SetActive(false);

            if (!MenuBar.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.activeSelf)
            {
                allbar.navi_t = 0;
                allbar.moveNavi = true;
                allbar.NaviOn = false;
                allbar.langnavi_t = 0;
                allbar.langNaviOn = true;
                allbar.movelangNavi = true;
                allbar.ETCnavi_t = 0;
                allbar.ETCNaviOn = true;
                allbar.moveETCNavi = true;
            }

            if (FunctionCustom.filterOff == false)
            {
                FunctionCustom.functionorigin.FilterReset();
            }

            WriteLogStart = false;
        }
        else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            //clearmode.labeldetail.SelectCloseButton();
            clearmode.labeldetail.ClickClose();
            //clearmode.labeldetail.CancelSelect();
            ClearMode.SeeSelectLabel_move = false;
            ClearMode.SeeSelectLabel_zoom = false;
            ClearMode.SeeSelectnavi_move = false;
            clearmode.CameraWindow.transform.localPosition = ClearMode.StartPosition;
            clearmode.CameraWindow.transform.parent.gameObject.transform.position = new Vector3(0, 0, ClearMode.StartZoom);
            if (!Tip_Obj.activeSelf)        // Tip �̹����� ��Ȱ��ȭ���¸� Ȱ��ȭ
            {
                Tipbtn.transform.GetChild(0).gameObject.SetActive(true);
                Tip_Obj.SetActive(true);
                TipOpen();
            }
            //PinchZoomInOut.ZoomMove = true;
            //PinchZoomInOut.ZoomIN = false;

            MiniMap_CameraGuide.gameObject.SetActive(false);

            if (!MenuBar.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.activeSelf)
            {
                allbar.navi_t = 0;
                allbar.moveNavi = true;
                allbar.NaviOn = false;
                allbar.langnavi_t = 0;
                allbar.langNaviOn = true;
                allbar.movelangNavi = true;
                allbar.ETCnavi_t = 0;
                allbar.ETCNaviOn = true;
                allbar.moveETCNavi = true;
            }

            if (FunctionCustom.filterOff == false)
            {
                FunctionCustom.functionorigin.FilterReset();
            }

            if (FunctionCustom.ChangeSeason == true)
            {
                Clear_Pano.SeeHereEffect.SetActive(true);
            }

            WriteLogStart = false;
        }
        else if (SceneManager.GetActiveScene().name.Contains("VisitMode"))
        {
            if ((int)touchCount >= 60)
            {
                GameObject obj = new GameObject();
                obj.name = "LiveMode";
                Menu(obj);
            }
            else
            {
                visitmanager.OnCLickVisitBtn();
            }
        } else if (SceneManager.GetActiveScene().name.Contains("Tourism"))
        {
            if (ContentsInfo.ContentsName == "GoldSunset")
            {
                gamemanager.label.Narration.Stop();
                tourLite.AllRoute.SetActive(true);
                tourLite.AnnounceRoute.SetActive(false);
            }
        }
    }
}
