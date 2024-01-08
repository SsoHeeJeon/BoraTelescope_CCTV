using Google.Protobuf.WellKnownTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearMode : Clear_Drag
{
    public GameManager gamemanager;
    public LabelDetail labeldetail;
    public GameObject CameraWindow;
    public GameObject AllMapLabels;
    public Camera UICam;

    public Image TestImage;
    private float full_x;
    private float full_y;
    public float min_x;
    private float min_y;
    public float max_x;
    private float max_y;
    private float ZoomOut_min_x;
    private float ZoomOut_min_y;
    private float ZoomOut_max_x;
    private float ZoomOut_max_y;
    private float cameraout_x;
    private float cameraout_y;
    public float arrowmove_t;
    public string drag_dir;
    public static float zoommove_t;
    private float labelmove_t;
    private float distancevalue_t;
    private float dragmove_t;
    private Vector3 firsttouchmove;
    private Vector3 secondtouchmove;
    private int prevtouchcount;
    //private float onecount;
    private float zoom_int = 1.0f;

    private float playtime;

    int touchcount_int;
    float m_fOldToucDis = 0f;       // 터치 이전 거리를 저장합니다.
    float m_fFieldOfView = 1851f;     // 카메라의 FieldOfView의 기본값을 60으로 정합니다.

    float bx;
    float by;
    float fx;
    float fy;
    string dir;
    Vector3 beforepos;

    public GameObject clicknaviobj;
    public static Vector3 originZoom;
    public static Vector3 changeZoom;
    public static Vector3 StartPosition;
    public static float StartZoom;
    public static float LabelMaxZoomIn;
    public static float MaxZoomIn;
    public static float MaxZoomOut;
    public static float MinLabelSize = 1.0f;
    public static float MaxLabelSize = 5.5f;
    public static float Cameraz = -1631;

    Vector3 originPosition;
    Vector3 changePosition;
    float originsize;
    float changeSize;

    public static bool Navi_OnOff = false;
    public static bool gosecond = false;
    public static bool dontclick = false;
    public static bool dontmove = false;
    public static bool dontzoom = false;
    public static bool ZoomReset = false;
    public static bool setCount = false;
    public static bool SeeSelectLabel_move = false;
    public static bool SeeSelectLabel_zoom = false;
    public static bool SeeSelectnavi_move = false;
    public static bool clickNavi = false;
    public static bool StartMove = false;

    public bool DragStop = false;
    public bool TouchStop = false;

    public bool alreadyPinchZoom = false;
    private bool alreadyDragMove = false;
    private bool alreadyZoom = false;
    bool maplabelLog = false;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //labeldetail = GameObject.Find("ClearMode").GetComponent<LabelDetail>();
        CameraRange();
        gamemanager.UISetting();

        if(GameManager.PrevMode == "XRMode" || GameManager.PrevMode == "LiveMode")
        {
            Invoke("AutoClose", 3f);
        }
        gamemanager.WriteLog(LogSendServer.NormalLogCode.ChangeMode, "ChangeMode : Finish(" + GameManager.PrevMode + " - " + "ClearMode)", GetType().ToString());
        GameManager.PrevMode = "ClearMode";
        playtime = 0;
        if (FunctionCustom.ChangeSeason == true)
        {
            FunctionCustom.functionorigin.SeasonPano.ReadytoStart();
        }

        if (FunctionCustom.filterOff == false)
        {
            FunctionCustom.functionorigin.filterfunction.FilterBar.gameObject.GetComponent<FilterEffect>().ReadyToFilter();
        }

        if (FunctionCustom.Recordversion == true)
        {
            //gamemanager.setrecord.ChangeRecordCamPos();
        }
        gamemanager.minimap.SettingHotspot();
        touchcount_int = 0;
        prevtouchcount = 0;
        zoommove_t = 0;
        labelmove_t = 0;
        distancevalue_t = 1;
        setCount = false;
        dontmove = false;
        dontzoom = false;
        SeeSelectLabel_move = false;
        SeeSelectLabel_zoom = false;
        SeeSelectnavi_move = false;
        clearmode.CameraWindow.transform.localPosition = ClearMode.StartPosition;
        CameraWindow.transform.parent.gameObject.transform.position = new Vector3(0,0,StartZoom);
        clicknaviobj = null;
        gamemanager.pinchzoominout.ZoomState.gameObject.SetActive(false);
        //gamemanager.labelmake.ReadytoStart();
        gamemanager.label.SelectCategortButton(gamemanager.label.CategoryContent.transform.GetChild(0).gameObject);
        Clear_Pano.SeeHereEffect.SetActive(true);

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
        zoommove_t += Time.deltaTime * 4;
        labelmove_t += Time.deltaTime * 0.05f * distancevalue_t;
        zoom_int = ((CameraWindow.transform.parent.gameObject.transform.position.z - MaxZoomIn) / -522) + 1.2f;

        touchcount_int = Input.touchCount;

        CameraRange();
        ChangeLabelScale();
        Labelactive();
        MoveZoomOut();
        //CountTouchDir();
        /*
        if (FunctionCustom.View360 == false && FunctionCustom.PastMode == false)
        {

            if (FunctionCustom.filterOff == true)
            {
                DragRange_Origin();
            }
            else if (FunctionCustom.filterOff == false)
            {
                DragRange_FSfunction();
            }

            if (FunctionCustom.ChangeSeason == true && FunctionCustom.functionorigin.SeasonPano.seasonBar.transform.localPosition.y == 508)
            {
                DragRange_FSfunction();
            }
        } else 
        {
            if ((FunctionCustom.PastMode == true && !FunctionCustom.functionorigin.pastcontents.obj360.activeSelf) || (FunctionCustom.View360 == true && !FunctionCustom.functionorigin.view360.obj360.activeSelf))
            {
                if (FunctionCustom.filterOff == true)
                {
                    DragRange_Origin();
                }
                else if (FunctionCustom.filterOff == false)
                {
                    DragRange_FSfunction();
                }

                if (FunctionCustom.ChangeSeason == true && FunctionCustom.functionorigin.SeasonPano.seasonBar.transform.localPosition.y == 508)
                {
                    DragRange_FSfunction();
                }
            }
        }*/

        if (FunctionCustom.ChangeSeason == true && !Clear_Pano.SeeHereEffect.activeSelf && FunctionCustom.functionorigin.SeasonPano.seasonBar.transform.position.y != 508)
        {
            if(playtime >= 120)
            {
                if (!gamemanager.ErrorMessage.activeSelf)
                {
                    NoticeWindow.NoticeWindowOpen("SeeFourSeason");
                    playtime = 0;
                }
            }
        }
        
        if (UICam.gameObject.activeSelf)
        {
            UICam.gameObject.transform.localPosition = new Vector3(0, 0, -1630);
            UICam.gameObject.GetComponent<Camera>().orthographicSize = -CameraWindow.transform.parent.gameObject.transform.position.z + (MaxZoomIn + 665);  // -카메라 줌 크기 + (최대줌인:1851+최소orhtographicSize:665)
        }
        
        if (touchcount_int != prevtouchcount)
        {
            setCount = true;
            changeTouchCount();
        }
        else if (touchcount_int == prevtouchcount)
        {
            if (TouchStop == false)
            {
                if (touchcount_int == 1 && dontmove == true && Input.touches[0].phase != TouchPhase.Stationary)
                {
                    dontmove = false;

                    beforepos = Input.mousePosition;
                    bx = beforepos.x;
                    by = beforepos.y;
                }
                if (touchcount_int == 2 && dontzoom == true)
                {
                    dontzoom = false;
                    m_fOldToucDis = 0;
                }
            }
        }

        if (touchcount_int == 1)
        {
            if (alreadyPinchZoom == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyPinchZoom = false;
            }

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if (TouchStop == false)
                {
                    beforepos = Input.mousePosition;
                    bx = beforepos.x;
                    by = beforepos.y;
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                //if (dontmove == false && DragStop == false && TouchStop == false)
                if(GameManager.UITouch == false)
                {
                    if (Input.mousePosition.x != bx)
                    {
                        fx = (bx - Input.mousePosition.x) * zoom_int;
                        fy = (by - Input.mousePosition.y) * zoom_int;

                        dragmove_t += Time.deltaTime;

                        if ((int)dragmove_t >= 0 && (int)dragmove_t < 1)
                        {
                            firstmoveposition();
                        }
                        if (fx < 0) // 카메라가 왼쪽으로 움직임
                        {
                            drag_dir = "Left";
                        }
                        else if (fx > 0) // 카메라가 오른쪽으로 움직임
                        {
                            drag_dir = "Right";
                        }

                        if (fy < 0) // 카메라가 아래로 움직임
                        {
                            drag_dir = "Down";
                        }
                        else if (fy > 0) // 카메라가 위로 움직임
                        {
                            drag_dir = "Up";
                        }
                        dir = drag_dir;
                        if (ZoomReset == false)
                        {
                            touchmovecamera();
                        }

                        //if (gamemanager.see360.obj360.activeSelf)
                        //{
                        //    touchmove360();
                        //}
                    }
                }
                else
                {
                    DragMoveLog();
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                touchcount_int = 0;
                dontclick = false;
                gosecond = false;
                dragmove_t = 0;
                firsttouchmove = Vector3.zero;
                secondtouchmove = Vector3.zero;

                DragMoveLog();
            }
        }
        else if (touchcount_int >= 2)
        {
            DragMoveLog();
            firsttouchmove = Vector3.zero;
            secondtouchmove = Vector3.zero;

            PosValue = (Input.touches[0].position + Input.touches[1].position) / 2;
            Xposvalue = PosValue.x - 960;
            Yposvalue = PosValue.y - 540;

            if(dist!=0)
            {
                storedist = dist;
            }
            dist = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

            if(storedist!=0)
            {
                float distspeed = dist - storedist;
                speed = Mathf.Abs(distspeed / 1.5f);
                print("dist = " + distspeed);
            }


            PinchZoom();
        }
        else if (touchcount_int == 0)
        {
            PosValue = Vector2.zero;
            Xposvalue = 0;
            Yposvalue = 0;
            zoomcheck= false;
            DragMoveLog();
            if (alreadyPinchZoom == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyPinchZoom = false;
            }
            DragStop = false;
            firsttouchmove = Vector3.zero;
            secondtouchmove = Vector3.zero;
            MoveZoomOut();
        }

        if (SeeSelectLabel_move == true)
        {
            SelectMapLabel_move();
        }
        if (SeeSelectLabel_zoom == true)
        {
            SelectMapLabel_zoom();
        }

        if (ZoomReset == true)
        {
            ResetZoom();
        }
        if (SeeSelectnavi_move == true)
        {
            ZoomOutMinMax();
        }
    }

    public void changeTouchCount()
    {
        if (setCount == true)
        {
            if (prevtouchcount == 2 && touchcount_int == 1)
            {
                dontmove = true;
                dontzoom = true;
            }
            else if (prevtouchcount == 0 && touchcount_int == 1)
            {
                dontmove = false;
            }
            else if (prevtouchcount == 2 && touchcount_int == 0)
            {
                dontmove = true;
                dontzoom = true;
            }
            else if (prevtouchcount == 0 && touchcount_int == 2)
            {
                dontzoom = true;
            }
            else if (prevtouchcount == 1 && touchcount_int == 2)
            {
                dontzoom = true;
            }
            prevtouchcount = touchcount_int;
            setCount = false;
        }
    }

    public void CameraRange()
    {
        if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            //가상카메라 임계점설정
            full_x = (int)TestImage.rectTransform.rect.width;
            full_y = (int)TestImage.rectTransform.rect.height;

            cameraout_x = TestImage.transform.parent.gameObject.GetComponent<SetWidth>().CameraWidth / 2;
            cameraout_y = (TestImage.transform.parent.gameObject.GetComponent<SetWidth>().CameraWidth * 1080 / 1920) / 2;

            min_x = -full_x / 2 + TestImage.transform.position.x + cameraout_x;
            max_x = full_x / 2 + TestImage.transform.position.x - cameraout_x;
            min_y = -full_y / 2 + TestImage.transform.position.y + cameraout_y;
            max_y = full_y / 2 + TestImage.transform.position.y - cameraout_y;

            if (cameraout_x != 0 && cameraout_y != 0)
            {
                if (ZoomOut_min_x == 0)
                {
                    ZoomOut_min_x = min_x;
                    ZoomOut_min_y = min_y;
                    ZoomOut_max_x = max_x;
                    ZoomOut_max_y = max_y;
                }
            }
            else
            {
                ZoomOut_min_x = 0;
                ZoomOut_min_y = 0;
                ZoomOut_max_x = 0;
                ZoomOut_max_y = 0;
            }
        }
    }

    public void ChangeLabelScale()
    {
        originsize = AllMapLabels.transform.GetChild(0).gameObject.transform.localScale.x;
        changeSize = Mathf.Clamp(changeSize, MinLabelSize, MaxLabelSize);
        changeSize = MaxLabelSize - (CameraWindow.transform.parent.gameObject.transform.position.z - MaxZoomOut) / (MaxZoomIn-MaxZoomOut) * (MaxLabelSize-MinLabelSize);

        for (int index = 0; index < AllMapLabels.transform.childCount; index++)
        {
            AllMapLabels.transform.GetChild(index).gameObject.transform.localScale = Vector3.Lerp(new Vector3(originsize, originsize, originsize), new Vector3(changeSize, changeSize, changeSize), zoommove_t * 0.005f);
        }
    }

    public void Resetothers()
    {
        if (StartMove == false)
        {
            if (Mathf.Abs(labeldetail.Detail_Background.transform.localPosition.x - LabelDetail.Detail_Close_x) > 1f && clicknaviobj != null)
            {
                SeeSelectLabel_move = false;
                SeeSelectLabel_zoom = false;
                clicknaviobj = null;
                for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                {
                    AllMapLabels.transform.GetChild(index).GetComponent<Button>().enabled = true;
                }
                labeldetail.CloseDetailWindow();
            }
            if (gamemanager.joystick.enabled)
            {
                for (int index = 1; index < gamemanager.Arrow.transform.childCount; index++)
                {
                    gamemanager.Arrow.transform.GetChild(index).gameObject.SetActive(true);
                }
                gamemanager.joystick.enabled = false;

            }

            if (FunctionCustom.filterOff == false)
            {
                FunctionCustom.functionorigin.FilterReset();
            }

            if (FunctionCustom.ChangeSeason == true)
            {
                if (FunctionCustom.functionorigin.SeasonPano.seasonBar.transform.localPosition.y < 720)
                {
                    FunctionCustom.functionorigin.SeasonPano.SeasonChange();
                }
            }

            StartMove = true;
        }
    }

    public void MoveCamera_Arrow()
    {
        // 일정 속도 이상 올라가지 않게 제어하기
        if (arrowmove_t <= 0.2f)
        {
            arrowmove_t += 0.1f * Time.deltaTime;
        }
        Resetothers();
        switch (gamemanager.MoveDir)
        {
            case "Left":
                if (CameraWindow.transform.localPosition.x >= min_x)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x - (400 * arrowmove_t), CameraWindow.transform.localPosition.y, CameraWindow.transform.localPosition.z);
                }
                else if (CameraWindow.transform.localPosition.x < min_x)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, CameraWindow.transform.localPosition.y, CameraWindow.transform.localPosition.z);
                }
                break;
            case "Right":
                if (CameraWindow.transform.localPosition.x <= max_x)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x + (400 * arrowmove_t), CameraWindow.transform.localPosition.y, CameraWindow.transform.localPosition.z);
                }
                else if (CameraWindow.transform.localPosition.x < max_x)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, CameraWindow.transform.localPosition.y, CameraWindow.transform.localPosition.z);
                }
                break;
            case "Up":
                if (CameraWindow.transform.localPosition.y <= max_y || CameraWindow.transform.localPosition.y + 400 <= max_y)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, CameraWindow.transform.localPosition.y + (400 * arrowmove_t), CameraWindow.transform.localPosition.z);
                }
                else if (CameraWindow.transform.localPosition.y >= max_y || CameraWindow.transform.localPosition.y + 400 >= max_y)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, CameraWindow.transform.localPosition.y, CameraWindow.transform.localPosition.z);
                }
                break;
            case "Down":
                if (CameraWindow.transform.localPosition.y >= min_y || CameraWindow.transform.localPosition.y - 400 >= min_y)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, CameraWindow.transform.localPosition.y - (400 * arrowmove_t), CameraWindow.transform.localPosition.z);
                }
                else if (CameraWindow.transform.localPosition.y <= min_y || CameraWindow.transform.localPosition.y - 400 <= min_y)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, CameraWindow.transform.localPosition.y, CameraWindow.transform.localPosition.z);
                }
                break;
        }
        dir = gamemanager.MoveDir;
    }


    public void firstmoveposition()
    {
        if (TouchStop == false)
        {
            gosecond = true;
            Invoke("secondmoveposition", 0.01f);
        }
    }

    public void secondmoveposition()
    {
        if (gosecond == true && TouchStop == false)
        {
            beforepos = Input.mousePosition;

            bx = beforepos.x;
            by = beforepos.y;

            secondtouchmove = Input.mousePosition;

            dragmove_t = 0;
            firsttouchmove = Vector3.zero;
            secondtouchmove = Vector3.zero;
            if (Vector3.Distance(firsttouchmove, secondtouchmove) < 0.05f && dragmove_t >= 0.35f)
            {
                dontclick = true;
                gosecond = false;
            }
        }
    }

    public void touchmovecamera()
    {
        if (alreadyDragMove == false)
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_FinishDrag, "Clear_StartDrag_(" + CameraWindow.transform.localPosition.x + ", " + CameraWindow.transform.localPosition.y + ")", GetType().ToString());
            alreadyDragMove = true;
        }
        Resetothers();
        if (CameraWindow.transform.position.x > min_x && CameraWindow.transform.position.x < max_x && CameraWindow.transform.position.y > min_y && CameraWindow.transform.position.y < max_y)
        {
            if (CameraWindow.transform.position.x + fx <= min_x)
            {
                if (CameraWindow.transform.position.y + fy <= min_y)
                {
                    CameraWindow.transform.position = new Vector3(min_x + 0.1f, min_y + 0.1f, CameraWindow.transform.position.z);
                }
                else if (CameraWindow.transform.position.y + fy >= max_y)
                {
                    CameraWindow.transform.position = new Vector3(min_x + 0.1f, max_y - 0.1f, CameraWindow.transform.position.z);
                }
                else if (CameraWindow.transform.position.y + fy < max_y && CameraWindow.transform.position.y + fy > min_y)
                {
                    CameraWindow.transform.position = new Vector3(min_x + 0.1f, CameraWindow.transform.position.y + fy, CameraWindow.transform.position.z);
                }
            }
            else if (CameraWindow.transform.position.x + fx >= max_x)
            {
                if (CameraWindow.transform.position.y + fy <= min_y)
                {
                    CameraWindow.transform.position = new Vector3(max_x - 0.1f, min_y + 0.1f, CameraWindow.transform.position.z);
                }
                else if (CameraWindow.transform.position.y + fy >= max_y)
                {
                    CameraWindow.transform.position = new Vector3(max_x - 0.1f, max_y - 0.1f, CameraWindow.transform.position.z);
                }
                else if (CameraWindow.transform.position.y + fy < max_y && CameraWindow.transform.position.y + fy > min_y)
                {
                    CameraWindow.transform.position = new Vector3(max_x - 0.1f, CameraWindow.transform.position.y + fy, CameraWindow.transform.position.z);
                }
            }
            else if (CameraWindow.transform.position.x + fx > min_x && CameraWindow.transform.position.x + fx < max_x)
            {
                if (CameraWindow.transform.position.y + fy <= min_y)
                {
                    CameraWindow.transform.position = new Vector3(CameraWindow.transform.position.x + fx, min_y + 0.1f, CameraWindow.transform.position.z);
                }
                else if (CameraWindow.transform.position.y + fy >= max_y)
                {
                    CameraWindow.transform.position = new Vector3(CameraWindow.transform.position.x + fx, max_y - 0.1f, CameraWindow.transform.position.z);
                }
                else if (CameraWindow.transform.position.y + fy > min_y && CameraWindow.transform.position.y + fy < max_y)
                {
                    CameraWindow.transform.position = new Vector3(CameraWindow.transform.position.x + fx, CameraWindow.transform.position.y + fy, CameraWindow.transform.position.z);

                }
            }
        }
    }

    public void CountTouchDir()
    {
        print("touchcount_int" + touchcount_int);
        if (touchcount_int == 1)
        {
            if (alreadyPinchZoom == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyPinchZoom = false;
            }

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if (TouchStop == false)
                {
                    beforepos = Input.mousePosition;
                    bx = beforepos.x;
                    by = beforepos.y;
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                if (dontmove == false && DragStop == false && TouchStop == false)
                {
                    if (Input.mousePosition.x != bx)
                    {
                        fx = (bx - Input.mousePosition.x) * zoom_int;
                        fy = (by - Input.mousePosition.y) * zoom_int;

                        dragmove_t += Time.deltaTime;

                        if ((int)dragmove_t >= 0 && (int)dragmove_t < 1)
                        {
                            firstmoveposition();
                        }
                        if (fx < 0) // 카메라가 왼쪽으로 움직임
                        {
                            drag_dir = "Left";
                        }
                        else if (fx > 0) // 카메라가 오른쪽으로 움직임
                        {
                            drag_dir = "Right";
                        }

                        if (fy < 0) // 카메라가 아래로 움직임
                        {
                            drag_dir = "Down";
                        }
                        else if (fy > 0) // 카메라가 위로 움직임
                        {
                            drag_dir = "Up";
                        }
                        dir = drag_dir;
                        if (ZoomReset == false)
                        {
                            touchmovecamera();
                        }

                        //if (gamemanager.see360.obj360.activeSelf)
                        //{
                        //    touchmove360();
                        //}
                    }
                }
                else
                {
                    DragMoveLog();
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                touchcount_int = 0;
                dontclick = false;
                gosecond = false;
                dragmove_t = 0;
                firsttouchmove = Vector3.zero;
                secondtouchmove = Vector3.zero;

                DragMoveLog();
            }
        }
        else if (touchcount_int >= 2)
        {
            DragMoveLog();
            firsttouchmove = Vector3.zero;
            secondtouchmove = Vector3.zero;
            PinchZoom();
        }
        else if (touchcount_int == 0)
        {
            DragMoveLog();
            if (alreadyPinchZoom == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyPinchZoom = false;
            }
            DragStop = false;
            firsttouchmove = Vector3.zero;
            secondtouchmove = Vector3.zero;
            zoomcheck = false;
            MoveZoomOut();
        }
    }

    public void PinchZoom()
    {
        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            //PinchZoom_Origin();
            if(GameManager.UITouch == false)
            {
                if (alreadyPinchZoom == false)
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Start(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                    alreadyPinchZoom = true;
                }
                StartPinchZoom();
            }
        }
        else if (Input.touchCount != 2)
        {
            m_fFieldOfView = 0;
            m_fOldToucDis = 0;
            zoom_int = 1 + (m_fFieldOfView / 1852.0f) * 3;

            if (alreadyPinchZoom == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyPinchZoom = false;
            }
        }
    }
    Vector2 PosValue;
    public float Xposvalue;
    public float Yposvalue;
    public bool zoomcheck;
    public float zoomvalue;
    public float speed;
    float x = 0;
    float y = 0;
    float dist;
    float storedist;
    public void StartPinchZoom()
    {
        int nTouch = Input.touchCount;
        float m_fToucDis = 0f;
        float fDis = 0f;

        m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;
        fDis = (m_fToucDis - m_fOldToucDis) * 0.01f;

        // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
        if (fDis < 1000f)
        {
            m_fFieldOfView += fDis;
            zoom_int += fDis;
        }
        // 최대는 100, 최소는 20으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
        m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, MaxZoomOut, MaxZoomIn);
        //zoom_int = 1 + (m_fFieldOfView / 1852.0f) * 3;

        // 확대 / 축소가 갑자기 되지않도록 보간합니다.
        CameraWindow.transform.parent.gameObject.transform.position = Vector3.Lerp(CameraWindow.transform.parent.gameObject.transform.position, new Vector3(CameraWindow.transform.parent.gameObject.transform.position.x, CameraWindow.transform.parent.gameObject.transform.position.y, m_fFieldOfView), zoommove_t * 0.005f);


        if (!zoomcheck)
        {
            zoomcheck = true;
            zoomvalue = (CameraWindow.transform.parent.gameObject.transform.position.z - 2451) / -600;
            Xposvalue *= zoomvalue;
            Yposvalue *= zoomvalue;
            x = CameraWindow.transform.position.x + Xposvalue;
            y = CameraWindow.transform.position.y + Yposvalue;
        }
        CameraWindow.transform.localPosition = Vector2.Lerp(CameraWindow.transform.position, new Vector2(x, y), Time.deltaTime* speed);
        CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);
        MoveZoomOut();
        m_fOldToucDis = m_fToucDis;
    }

    /// <summary>
    /// ZoomIn에서 ZoomOut 할 때 임계점에 있다면 카메라 위치 이동시켜주기
    /// </summary>
    public void MoveZoomOut()
    {
        if (CameraWindow.transform.localPosition.x <= min_x)
        {
            if (CameraWindow.transform.localPosition.y <= min_y)
            {
                CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(min_x + 0, min_y + 0.05f, Cameraz), zoommove_t);
            }
            else if (CameraWindow.transform.localPosition.y >= max_y)
            {
                CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(min_x + 0.05f, max_y - 0.05f, Cameraz), zoommove_t);
            }
            else if (CameraWindow.transform.localPosition.y > min_y && CameraWindow.transform.localPosition.y < max_y)
            {
                CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(min_x + 0.05f, CameraWindow.transform.localPosition.y, Cameraz), zoommove_t);
            }
        }
        else if (CameraWindow.transform.localPosition.x >= max_x)
        {
            if (CameraWindow.transform.localPosition.y <= min_y)
            {
                CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(max_x - 0.05f, min_y + 0.05f, Cameraz), zoommove_t);
            }
            else if (CameraWindow.transform.localPosition.y >= max_y)
            {
                CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(max_x - 0.05f, max_y - 0.05f, Cameraz), zoommove_t);
            }
            else if (CameraWindow.transform.localPosition.y > min_y && CameraWindow.transform.localPosition.y < max_y)
            {
                CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(max_x - 0.05f, CameraWindow.transform.localPosition.y, Cameraz), zoommove_t);
            }
        }
        else if (CameraWindow.transform.localPosition.x > min_x && CameraWindow.transform.localPosition.x < max_x)
        {
            if (CameraWindow.transform.localPosition.y <= min_y)
            {
                CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(CameraWindow.transform.localPosition.x, min_y + 0.05f, Cameraz), zoommove_t);
            }
            else if (CameraWindow.transform.localPosition.y >= max_y)
            {
                CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(CameraWindow.transform.localPosition.x, max_y - 0.05f, Cameraz), zoommove_t);
            }
            else if (CameraWindow.transform.localPosition.y > min_y && CameraWindow.transform.localPosition.y < max_y)
            {
                //CameraWindow.transform.localPosition = Vector3.Lerp(CameraWindow.transform.localPosition, new Vector3(CameraWindow.transform.localPosition.x, CameraWindow.transform.localPosition.y, CameraWindow.transform.localPosition.z), zoommove_t);
            }
        }
    }


    public void Labelactive()
    {
        for (int index = 0; index < AllMapLabels.transform.childCount; index++)
        {
            if (CameraWindow.transform.localPosition.x + 4472 - 100 >= AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x
                && CameraWindow.transform.localPosition.x - 4472 + 300 <= AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.x
                && CameraWindow.transform.localPosition.y + 2515 - 20 >= AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.y
                && CameraWindow.transform.localPosition.y - 2515 + 20 <= AllMapLabels.transform.GetChild(index).gameObject.transform.localPosition.y)
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
        switch (dir)
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

    public void SelectMapLabel(GameObject label)
    {
        if (ZoomReset == false && SeeSelectLabel_move == false && SeeSelectLabel_zoom == false)
        {
            TouchStop = true;

            CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);
            for (int index = 0; index < AllMapLabels.transform.childCount; index++)
            {
                AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
            }

            for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
            {
                gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
            }

            if (clicknaviobj == null)
            {
                gamemanager.allbar.navi_t = 0;
                gamemanager.allbar.moveNavi = true;
                gamemanager.allbar.NaviOn = true;

                clicknaviobj = label;

                originPosition = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);

                changePosition = new Vector3(label.transform.localPosition.x + 202.0f, label.transform.localPosition.y, Cameraz);

                //if (label.transform.localPosition.x >= max_x)
                //{
                //    changePosition = new Vector3(max_x, label.transform.localPosition.y, Cameraz);
                //}
                //else
                //{
                //    if (label.transform.localPosition.x + 202.0f <= max_x)
                //    {
                //        changePosition = new Vector3(label.transform.localPosition.x + 202.0f, label.transform.localPosition.y, Cameraz);
                //    }
                //    else if (label.transform.localPosition.x + 202.0f > max_x)
                //    {
                //        changePosition = new Vector3(label.transform.localPosition.x, label.transform.localPosition.y, Cameraz);
                //    }
                //}

                originZoom = new Vector3(0, 0, CameraWindow.transform.parent.gameObject.transform.position.z);

                if (ContentsInfo.ContentsName == "Aegibong")
                {
                    //clearmode.gameObject.GetComponent<Aegibong_Eco>().ChangelabelZoom();
                }
                else
                {
                    changeZoom = new Vector3(0, 0, LabelMaxZoomIn);
                }
                zoommove_t = 0;

                if (maplabelLog == false)
                {
                    gamemanager.clearmode.WriteLabelLog(label);
                    maplabelLog = true;
                }

                float distance = Mathf.Abs(originPosition.x - changePosition.x);
                if (distance < 2000)
                {
                    distancevalue_t = 5;
                }
                else if (distance >= 2000)
                {
                    distancevalue_t = 1;
                }

                if (Mathf.Abs(CameraWindow.transform.localPosition.x - changePosition.x) <= 1.0f && Mathf.Abs(CameraWindow.transform.localPosition.y - changePosition.y) <= 1)
                {
                    SeeSelectLabel_zoom = true;
                    print(changePosition);
                }
                else
                {
                    labelmove_t = 0;
                    SeeSelectLabel_move = true;
                    SeeSelectLabel_zoom = false;
                    gamemanager.label.SelectLabel(label.name);
                }
            }
            else if (clicknaviobj != null)
            {
                if (clicknaviobj.name == label.name)         // 선택되어있는 라벨을 다시 선택 => Zoom Out 후 선택 취소
                {
                    clicknaviobj = null;
                    clickNavi = false;
                }
                else if (clicknaviobj.name != label.name)          // 라벨을 선택한 상태에서 다른 라벨 선택 => Zoom Out 후 다른 라벨 찾아가기
                {
                    gamemanager.allbar.navi_t = 0;
                    gamemanager.allbar.moveNavi = true;
                    gamemanager.allbar.NaviOn = true;

                    clicknaviobj = label;

                    clickNavi = true;
                }
                labeldetail.CloseDetailWindow();
            }
        }
    }

    public void SelectMapLabel_move()
    {
        Debug.Log("SelectMapLabel_move");
        if (CameraWindow.transform.position != changePosition)
        {
            if (changePosition.x >= min_x && changePosition.x <= max_x && changePosition.y >= min_y && changePosition.y <= max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.localPosition.x - changePosition.x) <= 10.0f && Mathf.Abs(CameraWindow.transform.localPosition.y - changePosition.y) <= 10.0f)
                {
                    CameraWindow.transform.position = new Vector3(changePosition.x, changePosition.y, Cameraz);
                    //labelmove_t = 0;
                    SeeSelectLabel_zoom = true;
                    maplabelLog = false;
                }
                else
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, changePosition, labelmove_t);
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);
                }
            }
            else
            {
                OutMinMax();
            }
        }
    }

    public void OutMinMax()
    {
        //CameraWindow.transform.position = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);
        //CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);
        SeeSelectLabel_zoom = true;
        if (changePosition.x < min_x)
        {
            if (changePosition.y < min_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(min_x + 0.1f, min_y + 0.1f, Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) <= 5f)
                {
                    if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) > 10f)
                    {
                        CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        //if (CameraWindow.transform.parent.transform.position.z < changeZoom.z - 0.1f)
                        //{
                        //    SeeSelectLabel_zoom = true;
                        //}
                    }
                    else if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) <= 10f)
                    {
                        if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) > 1f)
                        {
                            CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        }
                        else if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) < 1f)
                        {
                            CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                            labelmove_t = 0;
                        }

                        if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z && SeeSelectLabel_zoom != true)
                        {
                            //clicknaviobj = null;
                            SeeSelectLabel_zoom = true;
                        }
                    }
                }
            }
            else if (changePosition.y > max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(min_x + 0.1f, max_y - 0.1f, Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) <= 5f)
                {
                    if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) > 10f)
                    {
                        CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        //if (CameraWindow.transform.parent.transform.position.z < changeZoom.z - 0.1f)
                        //{
                        //    SeeSelectLabel_zoom = true;
                        //}
                    }
                    else if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) <= 10f)
                    {
                        if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) > 1f)
                        {
                            CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        }
                        else if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) < 1f)
                        {
                            CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                            labelmove_t = 0;
                        }

                        if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z && SeeSelectLabel_zoom != true)
                        {
                            //clicknaviobj = null;
                            SeeSelectLabel_zoom = true;
                        }
                    }
                }
            }
            else if (changePosition.y >= min_y && changePosition.y <= max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(min_x + 0.1f, changePosition.y, Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) <= 5f)
                {
                    if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) > 10f)
                    {
                        CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        //if (CameraWindow.transform.parent.transform.position.z < changeZoom.z - 0.1f)
                        //{
                        //    SeeSelectLabel_zoom = true;
                        //}
                    }
                    else if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) <= 10f)
                    {
                        if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) > 1f)
                        {
                            CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        }
                        else if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) < 1f)
                        {
                            CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                            labelmove_t = 0;
                        }

                        if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z && SeeSelectLabel_zoom != true)
                        {
                            //clicknaviobj = null;
                            SeeSelectLabel_zoom = true;
                        }
                    }
                }
            }
        }
        else if (changePosition.x > max_x)
        {
            if (changePosition.y < min_y)
            {
                if (Mathf.Abs((CameraWindow.transform.position.x - (max_x - 0.1f))) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(max_x - 0.1f, min_y + 0.1f, Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) <= 5f)
                {
                    if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) > 10f)
                    {
                        CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        //if (CameraWindow.transform.parent.transform.position.z < changeZoom.z - 0.1f)
                        //{
                        //    if (Mathf.Abs(CameraWindow.transform.position.x - changePosition.x) < 5f)
                        //    {
                        //        SeeSelectLabel_zoom = true;
                        //    }
                        //}
                    }
                    else if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) <= 10f)
                    {
                        if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) > 1f)
                        {
                            CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        }
                        else if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) < 1f)
                        {
                            CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                            labelmove_t = 0;
                        }

                        if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z && SeeSelectLabel_zoom != true)
                        {
                            //clicknaviobj = null;
                            SeeSelectLabel_zoom = true;
                        }
                    }
                }
            }
            else if (changePosition.y > max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(max_x - 0.1f, max_y - 0.1f, Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) <= 5f)
                {
                    if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) > 10f)
                    {
                        CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        //if (CameraWindow.transform.parent.transform.position.z < changeZoom.z)
                        //{
                        //    if (Mathf.Abs(CameraWindow.transform.position.x - changePosition.x) < 5f)
                        //    {
                        //        SeeSelectLabel_zoom = true;
                        //    }
                        //}
                    }
                    else if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) <= 10f)
                    {
                        if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) > 1f)
                        {
                            CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        }
                        else if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) < 1f)
                        {
                            CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                            labelmove_t = 0;
                        }

                        if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z && SeeSelectLabel_zoom != true)
                        {
                            //clicknaviobj = null;
                            SeeSelectLabel_zoom = true;
                        }
                    }
                }
            }
            else if (changePosition.y >= min_y && changePosition.y <= max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(max_x - 0.1f, changePosition.y, Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) <= 5f)
                {
                    if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) > 10f)
                    {
                        CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        //if (CameraWindow.transform.parent.transform.position.z < changeZoom.z - 0.1f)
                        //{
                        //    if (Mathf.Abs(CameraWindow.transform.position.x - changePosition.x) < 5f)
                        //    {
                        //        SeeSelectLabel_zoom = true;
                        //    }
                        //}
                    }
                    else if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) <= 10f)
                    {
                        if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) > 1f)
                        {
                            CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        }
                        else if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) < 1f)
                        {
                            CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                            labelmove_t = 0;
                        }

                        if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z && SeeSelectLabel_zoom != true)
                        {
                            //clicknaviobj = null;
                            SeeSelectLabel_zoom = true;
                        }
                    }
                }
            }
        }
        else if (changePosition.x >= min_x && changePosition.x <= max_x)
        {
            if (changePosition.y < min_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.y - (min_y + 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, min_y + 0.1f, Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.y - (min_y + 0.1f)) <= 5f)
                {
                    if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) > 10f)
                    {
                        CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        //if (CameraWindow.transform.parent.transform.position.z < changeZoom.z - 0.1f)
                        //{
                        //    if (Mathf.Abs(CameraWindow.transform.position.x - changePosition.x) < 5f)
                        //    {
                        //        SeeSelectLabel_zoom = true;
                        //    }
                        //}
                    }
                    else if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) <= 10f)
                    {
                        if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) > 1f)
                        {
                            CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        }
                        else if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) < 1f)
                        {
                            CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                            labelmove_t = 0;
                        }

                        if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z && SeeSelectLabel_zoom != true)
                        {
                            //clicknaviobj = null;
                            SeeSelectLabel_zoom = true;
                        }
                    }
                }
            }
            else if (changePosition.y > max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.y - (max_y - 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, max_y - 0.1f, Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.y - (max_y - 0.1f)) <= 5f)
                {
                    if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) > 10f)
                    {
                        CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        //if (CameraWindow.transform.parent.transform.position.z < changeZoom.z - 0.1f)
                        //{
                        //    if (Mathf.Abs(CameraWindow.transform.position.x - changePosition.x) < 15f)
                        //    {
                        //        SeeSelectLabel_zoom = true;
                        //    }
                        //}
                    }
                    else if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) <= 10f)
                    {
                        if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) > 1f)
                        {
                            CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(changePosition.x, changePosition.y, Cameraz), labelmove_t);
                        }
                        else if (Mathf.Abs((CameraWindow.transform.position.y - changePosition.y)) < 1f)
                        {
                            CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                            labelmove_t = 0;
                        }

                        if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z && SeeSelectLabel_zoom != true)
                        {
                            //clicknaviobj = null;
                            SeeSelectLabel_zoom = true;
                        }
                    }
                }
            }
        }
    }

    public void SelectMapLabel_zoom()
    {
        CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);
        if (Mathf.Abs(CameraWindow.transform.parent.gameObject.transform.position.z - changeZoom.z) > 0.1f)        // Zoom Out
        {
            if (alreadyZoom == false)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Zoom, "Clear_ZoomIn:Start_(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyZoom = true;
            }
            CameraWindow.transform.parent.gameObject.transform.position = new Vector3(0, 0, Mathf.Lerp(CameraWindow.transform.parent.gameObject.transform.position.z, changeZoom.z, zoommove_t * 0.0035f));
            
            labeldetail.DetailClose_but.enabled = false;
            for (int index = 0; index < AllMapLabels.transform.childCount; index++)
            {
                AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
            }

            Debug.Log("today " + changePosition);
            if (Mathf.Abs(CameraWindow.transform.parent.gameObject.transform.position.z - changeZoom.z) <= 5f)
            {
                if (changePosition.x >= max_x)
                {
                    changePosition = new Vector3(max_x, changePosition.y, Cameraz);
                    Debug.Log("today 1364 " + changePosition);
                }
                else
                {
                    if (changePosition.x - 202.0f > max_x)
                    {
                        changePosition = new Vector3(changePosition.x - 202.0f, changePosition.y, Cameraz);
                        Debug.Log("1371");
                    }
                }

                if (labeldetail.SeeLabelDetail == false && labeldetail.SeeDetail_Open == false)
                {
                    labeldetail.DetailOpen();
                }
            }
        }
        else if (Mathf.Abs(CameraWindow.transform.parent.gameObject.transform.position.z - changeZoom.z) <= 0.1f)      // ZoomIn
        {
            CameraWindow.transform.parent.gameObject.transform.position = changeZoom;

            if (alreadyZoom == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Zoom, "Clear_ZoomIn:Finish_(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyZoom = false;
            }

            if (CameraWindow.transform.parent.gameObject.transform.position.z == changeZoom.z)
            {
                Invoke("WaitOpenButton", 0.5f);
            }

            //if (changePosition.x >= max_x)
            //{
            //    changePosition = new Vector3(max_x, changePosition.y, Cameraz);
            //}
            //else
            //{
            //    if (changePosition.x - 202.0f > max_x)
            //    {
            //        changePosition = new Vector3(changePosition.x - 202.0f, changePosition.y, Cameraz);
            //    }
            //}

            //zoommove_t = 0;
            //SeeSelectLabel_zoom = false;

            //if (labeldetail.SeeLabelDetail == false && labeldetail.SeeDetail_Open == false)
            //{
            //    labeldetail.DetailOpen();
            //}

            //if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) < 1.0f)
            //{
            //    labelmove_t = 0;
            //    SeeSelectLabel_move = false;
            //}
            //else
            //{
            //    CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, -1631);
            //    labelmove_t = 0;
            //    SeeSelectLabel_move = false;
            //}

            if (Mathf.Abs((CameraWindow.transform.position.x - changePosition.x)) < 10.0f)
            {
                labelmove_t = 0;
                SeeSelectLabel_move = false;
                if (labeldetail.SeeLabelDetail == false && labeldetail.SeeDetail_Open == false)
                {
                    labeldetail.DetailOpen();
                    Invoke("WaitOpenButton", 0.5f);
                }
                zoommove_t = 0;
                SeeSelectLabel_zoom = false;
            }
            else
            {
                //CameraWindow.transform.localPosition = new Vector3(changePosition.x, changePosition.y, Cameraz);
                //labelmove_t = 0;
                //SeeSelectLabel_move = true;
                if (labeldetail.SeeLabelDetail == false && labeldetail.SeeDetail_Open == false)
                {
                    labeldetail.DetailOpen();
                }
            }

            if (labeldetail.Detail_Background.transform.localPosition.x == LabelDetail.Detail_Open_x)
            {
                labeldetail.DetailClose_but.enabled = true;
            }
        }
    }

    public void WaitOpenButton()
    {
        for (int index = 0; index < AllMapLabels.transform.childCount; index++)
        {
            AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
        }

        //GameObject NaviLabel = gamemanager.NavigationBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
        {
            gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
        }

        TouchStop = false;
    }

    public void ResetZoom()
    {
        if (CameraWindow.transform.localPosition.x >= ZoomOut_min_x && CameraWindow.transform.localPosition.x <= ZoomOut_max_x
            && CameraWindow.transform.localPosition.y >= ZoomOut_min_y && CameraWindow.transform.localPosition.y <= ZoomOut_max_y)
        {
            SeeSelectnavi_move = false;
            CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);
        }
        else
        {
            labelmove_t = 0;
            SeeSelectnavi_move = true;
        }
        if (Mathf.Abs(CameraWindow.transform.parent.gameObject.transform.position.z - changeZoom.z) > 0.1f)
        {
            if (alreadyZoom == false)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Zoom, "Clear_ZoomOut:Start_(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyZoom = true;
            }

            CameraWindow.transform.parent.gameObject.transform.position = new Vector3(0, 0, Mathf.Lerp(CameraWindow.transform.parent.gameObject.transform.position.z, changeZoom.z, zoommove_t * 0.01f));
        }
        else if (Mathf.Abs(CameraWindow.transform.parent.gameObject.transform.position.z - changeZoom.z) <= 0.1f)
        {
            CameraWindow.transform.parent.gameObject.transform.position = changeZoom;

            if (alreadyZoom == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Zoom, "Clear_ZoomOut:Finish_(" + CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                alreadyZoom = false;
            }

            //zoom_int = 1;

            if (SeeSelectLabel_zoom == true)
            {
                SeeSelectLabel_zoom = false;
            }
            if (ZoomReset == true && CameraWindow.transform.parent.gameObject.transform.position.z <= changeZoom.z)
            {
                for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                {
                    AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                }

                //GameObject NaviLabel = gamemanager.NavigationBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
                {
                    gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                }

                if (clickNavi == false)
                {
                    TouchStop = false;
                    ZoomReset = false;
                    zoommove_t = 0;
                }
            }

            if (clickNavi == true && labeldetail.SeeDetail_Open == false && labeldetail.SeeLabelDetail == false)
            {
                TouchStop = false;
                ZoomReset = false;
                zoommove_t = 0;

                GameObject NewSelect = clicknaviobj;
                //Debug.Log(clicknaviobj.name);
                clicknaviobj = null;
                if (NewSelect != null)
                {
                    SelectMapLabel(NewSelect);
                }
            }
        }
    }

    public void ZoomOutMinMax()
    {
        if (CameraWindow.transform.localPosition.x < min_x)
        {
            if (CameraWindow.transform.localPosition.y < min_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(min_x + 0.1f, min_y + 0.1f,Cameraz), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) <= 5f)
                {
                    CameraWindow.transform.localPosition = new Vector3(min_x + 0.1f, min_y + 0.1f, Cameraz);
                }
            }
            else if (CameraWindow.transform.localPosition.y > max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(min_x + 0.1f, max_y - 0.1f), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) <= 5f)
                {
                    CameraWindow.transform.localPosition = new Vector3(min_x + 0.1f, max_y - 0.1f, Cameraz);
                }
            }
            else if (CameraWindow.transform.localPosition.y >= min_y && CameraWindow.transform.localPosition.y <= max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(min_x + 0.1f, CameraWindow.transform.localPosition.y), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (min_x + 0.1f)) <= 5f)
                {
                    CameraWindow.transform.localPosition = new Vector3(min_x + 0.1f, CameraWindow.transform.localPosition.y, Cameraz);
                }
            }
        }
        else if (CameraWindow.transform.localPosition.x > max_x)
        {
            if (CameraWindow.transform.localPosition.y < min_y)
            {
                if (Mathf.Abs((CameraWindow.transform.position.x - (max_x - 0.1f))) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(max_x - 0.1f, min_y + 0.1f), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) <= 5f)
                {
                    CameraWindow.transform.localPosition = new Vector3(max_x - 0.1f, min_y + 0.1f, Cameraz);
                }
            }
            else if (CameraWindow.transform.localPosition.y > max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(max_x - 0.1f, max_y - 0.1f), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) <= 5f)
                {
                    CameraWindow.transform.localPosition = new Vector3(max_x - 0.1f, max_y - 0.1f, Cameraz);
                }
            }
            else if (CameraWindow.transform.localPosition.y >= min_y && CameraWindow.transform.localPosition.y <= max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(max_x - 0.1f, CameraWindow.transform.localPosition.y), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.x - (max_x - 0.1f)) <= 5f)
                {
                    CameraWindow.transform.localPosition = new Vector3(max_x - 0.1f, CameraWindow.transform.localPosition.y, Cameraz);
                }
            }
        }
        else if (CameraWindow.transform.localPosition.x >= min_x && CameraWindow.transform.localPosition.x <= max_x)
        {
            if (CameraWindow.transform.localPosition.y < min_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.y - (min_y + 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(CameraWindow.transform.localPosition.x, min_y + 0.1f), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.y - (min_y + 0.1f)) <= 5f)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, min_y + 0.1f, Cameraz);
                }
            }
            else if (CameraWindow.transform.localPosition.y > max_y)
            {
                if (Mathf.Abs(CameraWindow.transform.position.y - (max_y - 0.1f)) > 5f)
                {
                    CameraWindow.transform.position = Vector3.Lerp(CameraWindow.transform.position, new Vector3(CameraWindow.transform.localPosition.x, max_y - 0.1f), labelmove_t);
                }
                else if (Mathf.Abs(CameraWindow.transform.position.y - (max_y - 0.1f)) <= 5f)
                {
                    CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, max_y - 0.1f, Cameraz);
                }
            }
            else if (CameraWindow.transform.localPosition.y >= min_y && CameraWindow.transform.localPosition.y <= max_y)
            {
                CameraWindow.transform.localPosition = new Vector3(CameraWindow.transform.localPosition.x, CameraWindow.transform.localPosition.y, Cameraz);
                labelmove_t = 0;
                SeeSelectnavi_move = false;
            }
        }
    }

    public void SelectNaviLabel(GameObject label)
    {
        if (ZoomReset == false && SeeSelectLabel_zoom == false && SeeSelectLabel_move == false)
        {
            maplabelLog = true;

            TouchStop = true;
            gamemanager.allbar.navi_t = 0;
            gamemanager.allbar.NaviOn = true;
            gamemanager.allbar.moveNavi = true;

            if (CameraWindow.transform.parent.gameObject.transform.position.z <= StartZoom)     // Zoom Out
            {
                for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                {
                    if (label.name == AllMapLabels.transform.GetChild(index).gameObject.name && AllMapLabels.transform.GetChild(index).gameObject.activeSelf)
                    {
                        SelectMapLabel(AllMapLabels.transform.GetChild(index).gameObject);
                    }
                }
            }
            else if (CameraWindow.transform.parent.gameObject.transform.position.z > StartZoom)        // Zoom In
            {
                if (clicknaviobj != null)
                {
                    if (clicknaviobj.name == label.name)
                    {
                        clickNavi = false;
                        clicknaviobj = null;
                    }
                    else
                    {
                        clickNavi = true;

                        for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                        {
                            if (label.name == AllMapLabels.transform.GetChild(index).gameObject.name && AllMapLabels.transform.GetChild(index).gameObject.activeSelf)
                            {
                                clicknaviobj = AllMapLabels.transform.GetChild(index).gameObject;
                                originPosition = new Vector3(CameraWindow.transform.position.x, CameraWindow.transform.position.y, Cameraz);
                                changePosition = new Vector3(clicknaviobj.transform.localPosition.x + 202.0f, clicknaviobj.transform.localPosition.y, Cameraz);
                            }
                        }
                    }

                    labeldetail.CloseDetailWindow();
                }
                else if (clicknaviobj == null)
                {
                    for (int index = 0; index < AllMapLabels.transform.childCount; index++)
                    {
                        if (label.name == AllMapLabels.transform.GetChild(index).gameObject.name && AllMapLabels.transform.GetChild(index).gameObject.activeSelf)
                        {
                            SelectMapLabel(AllMapLabels.transform.GetChild(index).gameObject);
                        }
                    }
                }
            }
        }
    }

    public void ZoomOutCamera()
    {
        originZoom = new Vector3(0, 0, CameraWindow.transform.parent.gameObject.transform.position.z);
        zoommove_t = 0;
        if (clicknaviobj != null)
        {
            changePosition = new Vector3(clicknaviobj.transform.localPosition.x + 202.0f, clicknaviobj.transform.localPosition.y, Cameraz);
            if (CameraWindow.transform.parent.gameObject.transform.position.z < StartZoom)
            {
                changeZoom = new Vector3(0, 0, CameraWindow.transform.parent.gameObject.transform.position.z);
            }
            else if (CameraWindow.transform.parent.gameObject.transform.position.z >= StartZoom && CameraWindow.transform.parent.gameObject.transform.position.z < MaxZoomIn)
            {
                changeZoom = new Vector3(0, 0, StartZoom);
            } else if(CameraWindow.transform.parent.gameObject.transform.position.z == MaxZoomIn)
            {
                changeZoom = new Vector3(0, 0, MaxZoomIn);
            }
            
            //changeZoom = new Vector3(0, 0, CameraWindow.transform.parent.gameObject.transform.position.z);

            TouchStop = true;
            ZoomReset = true;

            //if (CameraWindow.transform.parent.gameObject.transform.position.z != changeZoom.z)
            //{
            //    TouchStop = true;
            //    ZoomReset = true;
            //}
            //else if (CameraWindow.transform.parent.gameObject.transform.position.z == changeZoom.z)
            //{
            //    SeeSelectLabel_zoom = true;
            //}
        }
        else if (clicknaviobj == null)
        {
            if (originZoom.z < StartZoom)
            {
                changeZoom = new Vector3(0, 0, CameraWindow.transform.parent.gameObject.transform.position.z);
            }
            else if (originZoom.z >= StartZoom)
            {
                changeZoom = new Vector3(0, 0, StartZoom);
            }

            for (int index = 0; index < AllMapLabels.transform.childCount; index++)
            {
                AllMapLabels.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
            }
            TouchStop = true;
            ZoomReset = true;
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
            labeldetail.Detail_Background.transform.GetChild(2).GetComponent<Image>().sprite = gamemanager.label.Narr_On;
            gamemanager.label.PlayNarr = false;
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_DetailSound, "Clear_Detail:SoundOff", GetType().ToString());
        }
        else if (gamemanager.label.PlayNarr == false)
        {
            //gamemanager.label.Narration.Play();
            labeldetail.Detail_Background.transform.GetChild(2).GetComponent<Image>().sprite = gamemanager.label.Narr_Off;
            gamemanager.label.PlayNarr = true;
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_DetailSound, "Clear_Detail:SoundOn", GetType().ToString());
        }
    }

    public void WriteLabelLog(GameObject Label)
    {
        if (Label.transform.parent.gameObject.name == "Content")
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_SelectNavi, "Clear-SelectNavi : " + Label.name, GetType().ToString());
        }
        else if (Label.transform.parent.gameObject.name.Contains("Label"))
        {
            if (MinimapCustom.Hotspotclick == false)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_SelectLabel, "Clear-SelectLabel : " + Label.name, GetType().ToString());
            }
        }
    }

    public void DragMoveLog()
    {
        if (alreadyDragMove == true)
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_FinishDrag, "Clear_FinishDrag_(" + CameraWindow.transform.localPosition.x + ", " + CameraWindow.transform.localPosition.y + ")", GetType().ToString());
            alreadyDragMove = false;
        }
    }

    public void touchmove360()
    {
        //gamemanager.see360.obj360.transform.rotation = Quaternion.Euler(gamemanager.see360.obj360.transform.rotation.x + fy * 0.1f, gamemanager.see360.obj360.transform.rotation.eulerAngles.y + fx * 0.5f, gamemanager.see360.obj360.transform.rotation.z);
        //CameraWindow.transform.position = new Vector3(CameraWindow.transform.position.x + fx, CameraWindow.transform.position.y + fy, CameraWindow.transform.position.z);
    }

    public void XREffect_ani()
    {
        switch (ContentsInfo.ContentsName)
        {
            case "Aegibong":
                if (clicknaviobj != null)
                {
                    if (clicknaviobj.name == "Spoonbill")
                    {
                        //this.GetComponent<Aegibong_Eco>().spoonbill.SelectSpoonbill();
                    }
                }
                break;
        }
    }
}
