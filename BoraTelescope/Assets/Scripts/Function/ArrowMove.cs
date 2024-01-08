using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using PanTiltControl_v2;

public class ArrowMove : MonoBehaviour
{
    public GameManager gamemanager;

    public float touchtime;
    float arrowval = 40f;
    bool stoponce = false;
    public static bool alreadyarrowLog = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && (SceneManager.GetActiveScene().name.Contains("XRMode") || SceneManager.GetActiveScene().name.Contains("TelescopeMode")))
        {
            if (GameManager.UITouch == false)
            {
                if (GameManager.UITouch == false && FunctionCustom.View360 == false && FunctionCustom.PastMode == false)
                {
                    if (GameManager.StartMiniMapDrag == false || SetDragRange.StopMove == false)
                    {
                        touchtime += Time.deltaTime;
                    }
                }
                else
                {
                    if (GameManager.UITouch == false || (FunctionCustom.View360 == true && !FunctionCustom.functionorigin.view360.obj360.activeSelf) || (FunctionCustom.PastMode == true && !FunctionCustom.functionorigin.pastcontents.obj360.activeSelf))
                    {
                        touchtime += Time.deltaTime;
                    }
                }

                if (gamemanager.joystick.enabled == false)
                {
                    if (touchtime > 0.15f)
                    {
                        if (gamemanager.ZoomBar.transform.localPosition.y == 30.5f)
                        {
                            if (gamemanager.speed_enum != GameManager.Speed_enum.slow)
                            {
                                print("slow");
                                //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Slow);
                                //gamemanager.speed_enum = GameManager.Speed_enum.slow;
                            }
                        }
                        else
                        {
                            if (gamemanager.speed_enum != GameManager.Speed_enum.middle)
                            {
                                print("middle");
                                //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
                                gamemanager.speed_enum = GameManager.Speed_enum.middle;
                            }
                        }

                        if (Input.GetTouch(0).position.y >= 50)
                        {
                            DragArrow();
                        }
                        //gamemanager.setdragrange.ALLFuncDragRange();
                    }
                    //else if (touchtime <= 0.15f)
                    //{
                    //    PanTiltControl.Stop();
                    //}
                }
                stoponce = true;
            }
        }
        else if (Input.touchCount > 1 || Input.touchCount == 0)
        {
            touchtime = 0;
            //Arrow.transform.GetChild(0).gameObject.transform.localPosition = new Vector3(-3.6f, 0);

            if (stoponce == true)
            {
                Arrow_pointerUp();
                stoponce = false;
            }
            /*
            if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barOpen)
            {
                gamemanager.Arrow.gameObject.SetActive(false);
            }
            else if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose)
            {
                if (FunctionCustom.GuideMode == false && FunctionCustom.PastMode == false && FunctionCustom.View360 == false)
                {
                    gamemanager.Arrow.gameObject.SetActive(true);
                    gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                }
                else
                {
                    if (FunctionCustom.GuideMode == true && FunctionCustom.functionorigin.guidemode.GuideObj.activeSelf)
                    {
                        gamemanager.Arrow.gameObject.SetActive(false);
                    }
                    else if (FunctionCustom.PastMode == true && FunctionCustom.functionorigin.pastcontents.obj360.activeSelf)
                    {
                        gamemanager.Arrow.gameObject.SetActive(false);
                    }
                    else if (FunctionCustom.View360 == true && FunctionCustom.functionorigin.view360.obj360.activeSelf)
                    {
                        gamemanager.Arrow.gameObject.SetActive(false);
                    }
                    else
                    {
                        gamemanager.Arrow.gameObject.SetActive(true);
                        gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                    }
                }
            }*/
        }
    }

    /// <summary>
    /// 화살표버튼 선택했을 경우 그  UI를 받아와서 로그 남기기 (한번만)
    /// </summary>
    /// <param name="btn"></param>
    public void Arrow_pointerDown(GameObject btn)
    {
        if (gamemanager.ZoomBar.transform.localPosition.y == 30.5f)
        {
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Slow);
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Slow);
            gamemanager.speed_enum = GameManager.Speed_enum.slow;
        }
        else
        {
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Middle);
            gamemanager.speed_enum = GameManager.Speed_enum.middle;
        }
        gamemanager.MoveDir = btn.name;
        GameManager.MoveCamera = true;
        if (alreadyarrowLog == false)
        {
            if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_StartArrow, "Clear_StartArrow(" + btn.name + ")_(" + gamemanager.clearmode.CameraWindow.transform.localPosition.x + ", " + gamemanager.clearmode.CameraWindow.transform.localPosition.y + ")", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("XRMode"))
            {
                if (XRMode.PanFreq != XRMode.panFreq_ARR)
                {
                    XRMode.PanFreq = XRMode.panFreq_ARR;
                    //PanTiltControl_v2.PanTiltControl.SetFreq(PanTiltControl_v2.PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
                    UnityEngine.Debug.Log(XRMode.PanFreq);
                }
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_StartArrow, "XR_StartArrow(" + btn.name + ")_(" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("TelescopeMode"))
            {
                //MoveDir = btn.name;
                //MoveCamera = true;
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Live_StartArrow, "Live_StartArrow(" + btn.name + ")_(" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
            }
            alreadyarrowLog = true;
        }
    }


    /// <summary>
    /// 화살표 버튼 선택을 종료했을 경우(화살표를 선택하여 움직임을 멈춤) 로그 남기기(한번만)
    /// </summary>
    public void Arrow_pointerUp()
    {
        if (alreadyarrowLog == true)
        {
            if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                ClearMode.StartMove = false;
                gamemanager.clearmode.arrowmove_t = 0;
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_FinishArrow, "Clear_FinishArrow(" + gamemanager.clearmode.CameraWindow.transform.localPosition.x + ", " + gamemanager.clearmode.CameraWindow.transform.localPosition.y + ")", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("XRMode"))
            {
                XRMode.StartMove = false;
                gamemanager.xrmode.cctvcontrol.StopControl();
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_FinishArrow, "XR_FinishArrow (" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("TelescopeMode"))
            {
                gamemanager.xrmode.cctvcontrol.StopControl();
                gamemanager.WriteLog(LogSendServer.NormalLogCode.LIve_FinishArrow, "Live_FinishArrow(" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
            }
            alreadyarrowLog = false;
        }
        GameManager.MoveCamera = false;
    }

    public void DragArrow()
    {
        Vector3 touchpo = Input.GetTouch(0).position;
        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Stationary:
                if (touchtime > 0.15f && touchtime < 0.2f)
                {
                    gamemanager.Arrow.gameObject.SetActive(false);
                    gamemanager.Arrow.transform.position = touchpo;
                    gamemanager.Arrow.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;

                    if (gamemanager.MiniMap_CameraGuide.gameObject.activeSelf)
                    {
                        gamemanager.MiniMap_CameraGuide.gameObject.SetActive(false);
                    }
                    gamemanager.xrmode.cctvcontrol.StopControl();
                }
                else if (touchtime > 0.2f)
                {
                    if (touchpo == gamemanager.Arrow.transform.position)
                    {
                        gamemanager.Arrow.gameObject.SetActive(false);
                        gamemanager.Arrow.transform.position = touchpo;
                        gamemanager.Arrow.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;

                        if (gamemanager.MiniMap_CameraGuide.gameObject.activeSelf)
                        {
                            gamemanager.MiniMap_CameraGuide.gameObject.SetActive(false);
                        }

                        gamemanager.xrmode.cctvcontrol.StopControl();
                    }
                    else if (touchpo != gamemanager.Arrow.transform.position)
                    {
                        if (touchpo.x <= gamemanager.Arrow.transform.position.x + 118 && touchpo.x >= gamemanager.Arrow.transform.position.x - 118 && touchpo.y <= gamemanager.Arrow.transform.position.y + 118 && touchpo.y >= gamemanager.Arrow.transform.position.y - 118)
                        {
                            gamemanager.Arrow.transform.GetChild(0).gameObject.transform.position = touchpo;
                        }
                        else
                        {
                            float arrx;
                            float arry;
                            if (touchpo.x >= gamemanager.Arrow.transform.position.x + 118)
                            {
                                arrx = gamemanager.Arrow.transform.position.x + 100;
                            }
                            else if (touchpo.x <= gamemanager.Arrow.transform.position.x - 118)
                            {
                                arrx = gamemanager.Arrow.transform.position.x - 100;
                            }
                            else
                            {
                                arrx = touchpo.x;
                            }

                            if (touchpo.y >= gamemanager.Arrow.transform.position.y + 118)
                            {
                                arry = gamemanager.Arrow.transform.position.y + 100;
                            }
                            else if (touchpo.y <= gamemanager.Arrow.transform.position.y - 118)
                            {
                                arry = gamemanager.Arrow.transform.position.y - 100;
                            }
                            else
                            {
                                arry = touchpo.y;
                            }

                            gamemanager.Arrow.transform.GetChild(0).gameObject.transform.position = new Vector3(arrx, arry);
                        }

                        if ((touchpo.x - gamemanager.Arrow.transform.position.x) > arrowval)
                        {
                            gamemanager.MoveDir = "Right";
                        }
                        else if ((touchpo.x - gamemanager.Arrow.transform.position.x) < -arrowval)
                        {
                            gamemanager.MoveDir = "Left";
                        }
                        else if ((touchpo.x - gamemanager.Arrow.transform.position.x) <= arrowval && (touchpo.x - gamemanager.Arrow.transform.position.x) >= -arrowval)
                        {
                            if ((touchpo.y - gamemanager.Arrow.transform.position.y) <= arrowval && (touchpo.y - gamemanager.Arrow.transform.position.y) >= -arrowval)
                            {
                                gamemanager.MoveDir = null;
                                gamemanager.xrmode.cctvcontrol.StopControl();

                                if (alreadyarrowLog == false)
                                {
                                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_DragFinish, "XR_DragFinish (" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
                                    alreadyarrowLog = true;
                                }
                            }
                        }

                        if ((touchpo.y - gamemanager.Arrow.transform.position.y) > arrowval)
                        {
                            gamemanager.MoveDir = "Up";
                        }
                        else if ((touchpo.y - gamemanager.Arrow.transform.position.y) < -arrowval)
                        {
                            gamemanager.MoveDir = "Down";
                        }
                        else if ((touchpo.y - gamemanager.Arrow.transform.position.y) <= arrowval && (touchpo.y - gamemanager.Arrow.transform.position.y) >= -arrowval)
                        {
                            if (alreadyarrowLog == false)
                            {
                                if ((touchpo.x - gamemanager.Arrow.transform.position.x) <= arrowval && (touchpo.x - gamemanager.Arrow.transform.position.x) >= -arrowval)
                                {
                                    gamemanager.MoveDir = null;
                                    gamemanager.xrmode.cctvcontrol.StopControl();

                                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_FinishArrow, "XR_FinishArrow (" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
                                }
                                alreadyarrowLog = true;
                            }
                        }

                        GameManager.MoveCamera = true;

                        if (alreadyarrowLog == false)
                        {
                            if (XRMode.PanFreq != XRMode.panFreq_ARR)
                            {
                                XRMode.PanFreq = XRMode.panFreq_ARR;
                                //PanTiltControl_v2.PanTiltControl.SetFreq(PanTiltControl_v2.PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
                                Debug.Log(XRMode.PanFreq);
                            }

                            gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_DragStart, "XR_DragStart(" + gamemanager.MoveDir + ")_(" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
                            alreadyarrowLog = true;
                        }
                    }
                }
                break;
            case TouchPhase.Moved:
                if (touchtime > 0.15f && touchtime <= 0.17f)
                {
                    gamemanager.Arrow.gameObject.SetActive(false);
                    gamemanager.Arrow.transform.position = touchpo;
                    gamemanager.Arrow.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;

                    if (gamemanager.MiniMap_CameraGuide.gameObject.activeSelf)
                    {
                        gamemanager.MiniMap_CameraGuide.gameObject.SetActive(false);
                    }
                }
                else if (touchtime > 0.16f)
                {
                    float arrx;
                    float arry;
                    if (touchpo.x >= gamemanager.Arrow.transform.position.x + 118)
                    {
                        arrx = gamemanager.Arrow.transform.position.x + 100;
                    }
                    else if (touchpo.x <= gamemanager.Arrow.transform.position.x - 118)
                    {
                        arrx = gamemanager.Arrow.transform.position.x - 100;
                    }
                    else
                    {
                        arrx = touchpo.x;
                    }

                    if (touchpo.y >= gamemanager.Arrow.transform.position.y + 118)
                    {
                        arry = gamemanager.Arrow.transform.position.y + 100;
                    }
                    else if (touchpo.y <= gamemanager.Arrow.transform.position.y - 118)
                    {
                        arry = gamemanager.Arrow.transform.position.y - 100;
                    }
                    else
                    {
                        arry = touchpo.y;
                    }

                    gamemanager.Arrow.transform.GetChild(0).gameObject.transform.position = new Vector3(arrx, arry);

                    Debug.Log((touchpo.x - gamemanager.Arrow.transform.position.x) <= arrowval && (touchpo.x - gamemanager.Arrow.transform.position.x) >= -arrowval);
                    if ((touchpo.x - gamemanager.Arrow.transform.position.x) > arrowval)
                    {
                        gamemanager.MoveDir = "Right";
                    }
                    else if ((touchpo.x - gamemanager.Arrow.transform.position.x) < -arrowval)
                    {
                        gamemanager.MoveDir = "Left";
                    }
                    else if ((touchpo.x - gamemanager.Arrow.transform.position.x) <= arrowval && (touchpo.x - gamemanager.Arrow.transform.position.x) >= -arrowval)
                    {
                        if ((touchpo.y - gamemanager.Arrow.transform.position.y) <= arrowval && (touchpo.y - gamemanager.Arrow.transform.position.y) >= -arrowval)
                        {
                            gamemanager.MoveDir = null;
                            gamemanager.xrmode.cctvcontrol.StopControl();
                            //PanTiltControl.Stop();

                            if (alreadyarrowLog == false)
                            {
                                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_DragFinish, "XR_DragFinish (" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
                                alreadyarrowLog = true;
                            }
                        }
                    }

                    if ((touchpo.y - gamemanager.Arrow.transform.position.y) > arrowval)
                    {
                        gamemanager.MoveDir = "Up";
                    }
                    else if ((touchpo.y - gamemanager.Arrow.transform.position.y) < -arrowval)
                    {
                        gamemanager.MoveDir = "Down";
                    }
                    else if ((touchpo.y - gamemanager.Arrow.transform.position.y) <= arrowval && (touchpo.y - gamemanager.Arrow.transform.position.y) >= -arrowval)
                    {
                        if ((touchpo.x - gamemanager.Arrow.transform.position.x) <= arrowval && (touchpo.x - gamemanager.Arrow.transform.position.x) >= -arrowval)
                        {
                            gamemanager.MoveDir = null;
                            gamemanager.xrmode.cctvcontrol.StopControl();
                            //PanTiltControl.Stop();

                            if (alreadyarrowLog == false)
                            {
                                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_DragFinish, "XR_DragFinish (" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
                            }
                        }
                    }

                    GameManager.MoveCamera = true;

                    if (alreadyarrowLog == false)
                    {
                        if (XRMode.PanFreq != XRMode.panFreq_ARR)
                        {
                            XRMode.PanFreq = XRMode.panFreq_ARR;
                            //PanTiltControl_v2.PanTiltControl.SetFreq(PanTiltControl_v2.PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
                            Debug.Log(XRMode.PanFreq);
                        }

                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_DragStart, "XR_DragStart(" + gamemanager.MoveDir + ")_(" + gamemanager.xrmode.currentMotor_x + ", " + gamemanager.xrmode.currentMotor_y + ")", GetType().ToString());
                        alreadyarrowLog = true;
                    }
                }
                break;
            case TouchPhase.Ended:
                touchtime = 0;
                gamemanager.Arrow.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;

                Arrow_pointerUp();

                if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barOpen)
                {
                    gamemanager.Arrow.gameObject.SetActive(false);
                }
                else if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose)
                {
                    gamemanager.Arrow.gameObject.SetActive(true);
                    gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                }
                break;
        }
    }

    public void OtherDragRange()
    {
        touchtime = 0;
        //if (joystick.enabled == false)
        {
            gamemanager.Arrow.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        }

        if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barOpen)
        {
            gamemanager.Arrow.gameObject.SetActive(false);
        }
        else if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose)
        {
            gamemanager.Arrow.gameObject.SetActive(true);
            gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
        }
    }
}
