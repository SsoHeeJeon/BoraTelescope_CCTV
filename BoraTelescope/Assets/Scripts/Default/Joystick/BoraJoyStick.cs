//using PanTiltControl_v2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoraJoyStick : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 50;
    public VariableJoystick variableJoystick;
    public GameManager GM;
    // Update is called once per frame
    private void Start()
    {
        GM = GetComponent<GameManager>();
    }

    Vector3 direction;
    bool Vertical;
    bool Horizontal;
    public bool alreadyPinchZoom = false;
    public bool alreadyjoystick = false;

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

            GM.clearmode.CameraWindow.transform.localPosition = new Vector3(GM.clearmode.CameraWindow.transform.localPosition.x + (direction.x * speed),
                GM.clearmode.CameraWindow.transform.localPosition.y + (direction.z * speed), GM.clearmode.CameraWindow.transform.localPosition.z);

            if (direction == Vector3.zero)
            {
                if (alreadyjoystick == true)
                {
                    GM.WriteLog(LogSendServer.NormalLogCode.Clear_Joystick, "Clear_Joystick : Finish", GetType().ToString());
                    alreadyjoystick = false;
                }
            }
            else
            {
                if (alreadyjoystick == false)
                {
                    GM.WriteLog(LogSendServer.NormalLogCode.Clear_Joystick, "Clear_Joystick : Start", GetType().ToString());
                    alreadyjoystick = true;
                }
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            Vertical = true;
            Horizontal = true;
            if (variableJoystick.Horizontal >= 0.2f)
            {
                //if (GM.xrmode.currentMotor_x <= XRMode_Manager.MaxPan)
                {
                    SunAPITest.CCTVControl.UseUrl = "http://" + SunAPITest.CCTVControl.url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Right";
                    GM.xrmode.cctvcontrol.httpRequest.Request("GET", SunAPITest.CCTVControl.UseUrl, SunAPITest.CCTVControl.uid, SunAPITest.CCTVControl.pwd);
                    //PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.RIGHT);
                    //GM.MoveDir = "Right";
                    //Debug.Log("joystick + " + GM.MoveDir);
                    //GM.cctvcontrol.MoveCamera_Arrow();
                }
            }
            else if (variableJoystick.Horizontal <= -0.2f)
            {
                //if (GM.xrmode.currentMotor_x >= XRMode_Manager.MinPan)
                {
                    SunAPITest.CCTVControl.UseUrl = "http://" + SunAPITest.CCTVControl.url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Left";
                    GM.xrmode.cctvcontrol.httpRequest.Request("GET", SunAPITest.CCTVControl.UseUrl, SunAPITest.CCTVControl.uid, SunAPITest.CCTVControl.pwd);
                    //PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.LEFT);
                    //GM.MoveDir = "Left";
                    //Debug.Log("joystick + " + GM.MoveDir);
                    //GM.cctvcontrol.MoveCamera_Arrow();
                }
            }
            else
            {
                //PanTiltControl.Stop();
                Horizontal = false;
            }

            if (variableJoystick.Vertical >= 0.2f)
            {
                //if (GM.xrmode.currentMotor_y < XRMode_Manager.MaxTilt)
                {
                    SunAPITest.CCTVControl.UseUrl = "http://" + SunAPITest.CCTVControl.url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Up";
                    GM.xrmode.cctvcontrol.httpRequest.Request("GET", SunAPITest.CCTVControl.UseUrl, SunAPITest.CCTVControl.uid, SunAPITest.CCTVControl.pwd);
                    //PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.UP);
                    //GM.MoveDir = "Up";
                    //Debug.Log("joystick + " + GM.MoveDir);
                    //GM.cctvcontrol.MoveCamera_Arrow();
                }
            }
            else if (variableJoystick.Vertical <= -0.2f)
            {
                //if (GM.xrmode.currentMotor_y > XRMode_Manager.MinTilt)
                {
                    SunAPITest.CCTVControl.UseUrl = "http://" + SunAPITest.CCTVControl.url + "/stw-cgi/ptzcontrol.cgi?msubmenu=move&action=control&Direction=Down";
                    GM.xrmode.cctvcontrol.httpRequest.Request("GET", SunAPITest.CCTVControl.UseUrl, SunAPITest.CCTVControl.uid, SunAPITest.CCTVControl.pwd);
                    //PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.DOWN);
                    //GM.MoveDir = "Down";
                    //Debug.Log("joystick + " + GM.MoveDir);
                    //GM.cctvcontrol.MoveCamera_Arrow();
                }
            }
            else
            {
                //PanTiltControl.Stop();
                Vertical = false;
            }
            
            if (alreadyjoystick == false)
            {
                GM.WriteLog(LogSendServer.NormalLogCode.AR_Joystick, "XR_Joystick : Start", GetType().ToString());
                alreadyjoystick = true;
            }
            /*
            if (!Horizontal && !Vertical)
            {
                if (alreadyjoystick == false)
                {
                    GM.WriteLog(LogSendServer.NormalLogCode.AR_Joystick, "XR_Joystick : Finish 97", GetType().ToString());
                    alreadyjoystick = true;
                }
                PanTiltControl.Stop();
            }
            else
            {
                if (alreadyjoystick == true)
                {
                    GM.WriteLog(LogSendServer.NormalLogCode.AR_Joystick, "XR_Joystick : Start 106", GetType().ToString());
                    alreadyjoystick = false;
                }
            }*/
        }/*
        else if (SceneManager.GetActiveScene().name.Contains("TelescopeMode") || SceneManager.GetActiveScene().name.Contains("TeleScopeMode"))
        {
            Vertical = true;
            Horizontal = true;
            if (variableJoystick.Horizontal >= 0.2f)
            {
                if (GM.telescopemode.currentMotor_x <= TelescopeMode.Maxpan)
                {
                    PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.RIGHT);
                }
                else
                {
                    PanTiltControl.Stop();
                }
            }
            else if (variableJoystick.Horizontal <= -0.2f)
            {
                if (GM.telescopemode.currentMotor_x >= TelescopeMode.Minpan)
                {
                    PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.LEFT);
                }
                else
                {
                    PanTiltControl.Stop();
                }
            }
            else
            {
                //PanTiltControl.Stop();
                Horizontal = false;
            }

            if (variableJoystick.Vertical >= 0.2f)
            {
                if (GM.telescopemode.currentMotor_y < TelescopeMode.Maxtilt)
                {
                    PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.UP);
                }
                else
                {
                    PanTiltControl.Stop();
                }
            }
            else if (variableJoystick.Vertical <= -0.2f)
            {
                if (GM.telescopemode.currentMotor_y > TelescopeMode.Mintilt)
                {
                    PanTiltControl.ButtonAction(PanTiltControl.ButtonDIR.DOWN);
                }
                else
                {
                    PanTiltControl.Stop();
                }
            }
            else
            {
                //PanTiltControl.Stop();
                Vertical = false;
            }

            if (alreadyjoystick == false)
            {
                GM.WriteLog(LogSendServer.NormalLogCode.AR_Joystick, "XR_Joystick : Start", GetType().ToString());
                alreadyjoystick = true;
            }*/
            /*
            if (!Horizontal && !Vertical)
            {
                if (alreadyPinchZoom == false)
                {
                    GM.WriteLog(LogSendServer.NormalLogCode.AR_Joystick, "XR_Joystick : Finish 175", GetType().ToString());
                    alreadyPinchZoom = true;
                }
                PanTiltControl.Stop();
            }
            else
            {
                if (alreadyPinchZoom == true)
                {
                    GM.WriteLog(LogSendServer.NormalLogCode.AR_Joystick, "XR_Joystick : Start 184", GetType().ToString());
                    alreadyPinchZoom = false;
                }
            }*/
        //}
    }

    public void pantiltstop()
    {
        if (alreadyPinchZoom == false)
        {
            GM.WriteLog(LogSendServer.NormalLogCode.AR_Joystick, "XR_Joystick : Finish 195", GetType().ToString());
            alreadyPinchZoom = true;
        }
        //PanTiltControl.Stop();
        GM.xrmode.cctvcontrol.StopControl();
    }
}
