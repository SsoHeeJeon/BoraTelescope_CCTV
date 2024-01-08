using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using PanTiltControl_v2;

public class PantiltOrigin : MonoBehaviour
{
    public GameManager gamemanager;
    public enum OriginState
    {
        Wait, SetOrigin
    }
    public static OriginState State;

    uint currentPan;
    uint currentTilt;
    bool firstAct = false;

    float count;
    float counts;
    bool countstart = false;
    bool sendLog_1 = false;
    bool sendLog_2 = false;

    public bool StartOrigin = false;
    public bool FinishOrigin = false;

    bool logwrite = false;

    // Start is called before the first frame update
    public void ReadytoStart()
    {
        if (!firstAct)
        {
            //GameManager.startlabel_x = 0;
            //GameManager.startlabel_y = 0;
            currentPan = 0;
            currentTilt = 0;
            firstAct = true;
        }

        State = new OriginState();

        //if (PanTiltControl.IsConnected == false)
        //{
        //    PanTiltControl.Connect("COM14", 38400);
        //    CheckConnect();
        //}
    }

    public void CheckConnect()
    {
        //if (PanTiltControl.IsConnected == false)
        //{
        //    GameManager.AnyError = true;
        //    gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_Connect_Pantilt, "Fail_Connect_Pantilt", GetType().ToString());
        //}
        //else if (PanTiltControl.IsConnected == true)
        //{
        //    sendLog_1 = true;
        //    gamemanager.WriteLog(LogSendServer.NormalLogCode.Connect_Pantilt, "Connect_Pantilt:On", GetType().ToString());
        //    if (GameManager.AnyError == false)
        //    {
        //        GameManager.AnyError = false;
        //    }
        //    State = OriginState.SetOrigin;
        //    StartOrigin = false;
        //    FinishOrigin = false;
        //    Debug.Log("today CheckConnect");
        //}
    }
    /*
    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case OriginState.Wait:
                break;
            case OriginState.SetOrigin:
                if (StartOrigin == false)
                {
                    SetOrigin();
                }
                else if (StartOrigin == true)
                {
                    if (FinishOrigin == false)
                    {
                        CheckOrigin();
                    }
                    else if (FinishOrigin == true)
                    {
                        currentPan = (uint)PanTiltControl.NowPanPulse;
                        currentTilt = (uint)PanTiltControl.NowTiltPulse;

                        CheckStartPosition();
                    }
                }
                break;
        }
    }

    public void SetOrigin()
    {
        PanTiltControl.OriginMidFlag = false;
        PanTiltControl.OriginEndFlag = false;
        currentPan = 0;
        currentTilt = 0;
        counts = 0;
        PanTiltControl.NowPanPulse = 0;
        PanTiltControl.NowTiltPulse = 0;
        
        PanTiltControl.Origin();
        Debug.Log("today Pantilt Origin");
        gamemanager.WriteLog(LogSendServer.NormalLogCode.Etc_PantiltOrigin, "PantiltOrigin : Start", GetType().ToString());

        StartOrigin = true;
        sendLog_2 = true;
    }
    
    public void CheckOrigin()
    {
        if (PanTiltControl.OriginMidFlag == true)
        {
            if (PanTiltControl.OriginEndFlag == true)
            {
                if (sendLog_2 == true)
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Etc_PantiltOrigin, "PantiltOrigin : Finish", GetType().ToString());
                    sendLog_2 = false;
                }

                FinishOrigin = true;
                Debug.Log("today finish");
            }
            else if (PanTiltControl.OriginEndFlag == false)
            {
                if (sendLog_1 == false)
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Etc_PantiltOrigin, "PantiltOrigin : OriginMidFlag", GetType().ToString());
                    sendLog_1 = true;
                }
                if (countstart == false)
                {
                    count = 0;
                    sendLog_1 = false;
                    countstart = true;
                }
                count += Time.deltaTime;
                if ((int)count >= 60)
                {
                    count = 0;
                    countstart = false;
                    GameManager.AnyError = true;

                    if (sendLog_1 == false)
                    {
                        gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_EtcPantilt, "PantiltOrigin : Error(PanEndSensor)", GetType().ToString());
                        sendLog_1 = true;
                    }

                    State = OriginState.Wait;
                    if (SceneManager.GetActiveScene().name == "Loading")
                    {
                        if (Loading.nextScene != "WaitingMode")
                        {
                            gamemanager.loading.MoveClearMode();
                        }
                    }
                }
            }
        }
        else if (PanTiltControl.OriginMidFlag == false)
        {
            count += Time.deltaTime;
            if ((int)count >= 60)
            {
                counts = 0;
                GameManager.AnyError = true;
                if (sendLog_1 == false)
                {
                    gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_EtcPantilt, "PantiltOrigin : Error(PanEndSensor)", GetType().ToString());
                    sendLog_1 = true;
                }

                State = OriginState.Wait;
                if (SceneManager.GetActiveScene().name == "Loading")
                {
                    gamemanager.loading.MoveClearMode();

                }
            }
            countstart = false;
        }
    }

    public void SetStartPosition()
    {
        PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Fast);
        PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Fast);
        if (logwrite == false)
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Etc_PantiltStartPosition, "StartPoint", GetType().ToString());
            logwrite = true;
        }

        float tiltYposition = GameManager.startlabel_y;
        PanTiltControl.SetPulse(GameManager.startlabel_x, (uint)tiltYposition);
    }

    public void CheckStartPosition()
    {
        print("Pantilt true = " + (Mathf.Abs(currentPan - GameManager.startlabel_x) < 1 && Mathf.Abs(currentTilt - GameManager.startlabel_y) < 1));
        if (Mathf.Abs(currentPan - GameManager.startlabel_x) < 1 && Mathf.Abs(currentTilt - GameManager.startlabel_y) < 1)
        {
            State = OriginState.Wait;
            if (SceneManager.GetActiveScene().name == "Loading")
            {
                gamemanager.loading.MoveNextMode();
            } else if (SceneManager.GetActiveScene().name.Contains("XRMode"))
            {
                gamemanager.xrmode_manager.enabled = true;
                gamemanager.xrmode_manager.ReadFile();
            }
        }
        else
        {
            counts += Time.deltaTime;
            if ((int)counts >= 60)
            {
                counts = 0;
                GameManager.AnyError = true;
                gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_EtcPantilt, "PantiltOrigin : Error(PanEndSensor)", GetType().ToString());

                State = OriginState.Wait;
                if (SceneManager.GetActiveScene().name == "Loading")
                {
                    gamemanager.loading.MoveClearMode();
                }
            }

            SetStartPosition();
        }
    }*/
}
