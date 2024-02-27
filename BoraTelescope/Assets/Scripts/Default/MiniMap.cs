//using PanTiltControl_v2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniMap : MinimapCustom
{
    public RectTransform mapX;
    public RectTransform mapCamX;

    float minimapCamera_x;
    float totalminimap;
    float valueminimapx;

    bool alreadyminimap = false;

    public void BasicMinimap()
    {
        totalminimap = (mapX.rect.width - mapCamX.rect.width);

        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (alreadyminimap == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_FinishMinimap, "XR_FinishMinimap_(" + gamemanager.xrmode.CameraWindow.transform.localPosition.x + ", " + gamemanager.xrmode.CameraWindow.transform.localPosition.y + ")", GetType().ToString());
                alreadyminimap = false;
            }

            if (XRMode_Manager.MinPan >= 0)
            {
                minimapCamera_x = ((gamemanager.xrmode.currentMotor_x - XRMode_Manager.MinPan) / (XRMode_Manager.MaxPan - XRMode_Manager.MinPan)) * (totalminimap) - mapX.rect.width / 2 + mapCamX.rect.width / 2;
                //if (minimapCamera_x > 0)
                {
                    gamemanager.MiniMap_Camera.transform.localPosition = new Vector3(minimapCamera_x, gamemanager.MiniMap_Camera.transform.localPosition.y, gamemanager.MiniMap_Camera.transform.localPosition.z);
                } 
            }
            else if (XRMode_Manager.MinPan < 0)
            {
                float a = gamemanager.xrmode.currentMotor_x;
                if (gamemanager.xrmode.currentMotor_x > XRMode_Manager.MaxPan)
                {
                    a = gamemanager.xrmode.currentMotor_x - 360;
                }

                minimapCamera_x = ((a - XRMode_Manager.MinPan) / (XRMode_Manager.MaxPan - XRMode_Manager.MinPan)) * (totalminimap) - mapX.rect.width / 2 + mapCamX.rect.width / 2;
                Debug.Log(minimapCamera_x);
                if (minimapCamera_x > -470)
                {
                    gamemanager.MiniMap_Camera.transform.localPosition = new Vector3(minimapCamera_x, gamemanager.MiniMap_Camera.transform.localPosition.y, gamemanager.MiniMap_Camera.transform.localPosition.z);
                }
                else if (minimapCamera_x <= -470)
                {
                    gamemanager.MiniMap_Camera.transform.localPosition = new Vector3(396, gamemanager.MiniMap_Camera.transform.localPosition.y, gamemanager.MiniMap_Camera.transform.localPosition.z);
                }
            }


            if (gamemanager.MiniMap_CameraGuide.gameObject.activeSelf && Mathf.Abs(gamemanager.MiniMap_Camera.transform.localPosition.x - gamemanager.MiniMap_CameraGuide.transform.localPosition.x) < 0.05f)
            {
                gamemanager.MiniMap_CameraGuide.gameObject.SetActive(false);
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            if (alreadyminimap == true)
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_FinishMinimap, "Clear_FinishMinimap_(" + gamemanager.clearmode.CameraWindow.transform.localPosition.x + ", " + gamemanager.clearmode.CameraWindow.transform.localPosition.y + ")", GetType().ToString());
                ClearMode.StartMove = false;
                alreadyminimap = false;
            }

            if (totalminimap != 0)
            {
                minimapCamera_x = ((gamemanager.clearmode.CameraWindow.transform.position.x - gamemanager.clearmode.min_x) / (gamemanager.clearmode.max_x - gamemanager.clearmode.min_x)) * (totalminimap) - mapX.rect.width / 2 + mapCamX.rect.width / 2;
                gamemanager.MiniMap_Camera.transform.localPosition = new Vector3(minimapCamera_x, gamemanager.MiniMap_Camera.transform.localPosition.y, gamemanager.MiniMap_Camera.transform.localPosition.z);
            }
        }
    }

    public void ButtonMinimap()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (!gamemanager.Arrow.activeSelf)
            {
                if (gamemanager.joystick.enabled)
                {
                    gamemanager.joystick.enabled = false;
                }
                if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose)
                {
                    gamemanager.Arrow.SetActive(true);
                }
                gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                gamemanager.Arrow.transform.GetChild(0).transform.localPosition = Vector3.zero;
            }
            gamemanager.xrmode.Resetothers();
            
            if (!gamemanager.MiniMap_CameraGuide.gameObject.activeSelf)
            {
                if ((Input.GetTouch(0).position.x >= 540 && Input.GetTouch(0).position.x <= 1450) || (Input.mousePosition.x >= 540 && Input.mousePosition.x <= 1450))
                {
                    if (alreadyminimap == false)
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_StartMinimap, "XR_StartMinimap_(" + gamemanager.xrmode.CameraWindow.transform.localPosition.x + ", " + gamemanager.xrmode.CameraWindow.transform.localPosition.y + ")", GetType().ToString());
                        alreadyminimap = true;
                    }
                    gamemanager.MiniMap_CameraGuide.gameObject.SetActive(true);
                    valueminimapx = ((Input.GetTouch(0).position.x - 540) / 910 * totalminimap) - mapX.rect.width / 2 + mapCamX.rect.width / 2;
                    //gamemanager.MiniMap_Camera.transform.localPosition = new Vector3(gamemanager.minimapCamera_x, gamemanager.MiniMap_Camera.transform.localPosition.y, gamemanager.MiniMap_Camera.transform.localPosition.z);
                    gamemanager.MiniMap_CameraGuide.transform.localPosition = new Vector3(valueminimapx, gamemanager.MiniMap_Camera.transform.localPosition.y + 2, gamemanager.MiniMap_Camera.transform.localPosition.z);

                    gamemanager.xrmode.xpulse = ((valueminimapx - mapCamX.rect.width / 2 + mapX.rect.width / 2) / totalminimap * (XRMode_Manager.MaxPan - XRMode_Manager.MinPan)) + XRMode_Manager.MinPan;
                    gamemanager.xrmode.ypulse = gamemanager.xrmode.currentMotor_y;
                    gamemanager.xrmode.MoveCamera_Navi();
                }
            }
            else if (gamemanager.MiniMap_CameraGuide.gameObject.activeSelf)
            {
                valueminimapx = ((Input.GetTouch(0).position.x - 540) / 910 * totalminimap) - mapX.rect.width / 2 + mapCamX.rect.width / 2;
                if (Input.GetTouch(0).position.x >= 540 && Input.GetTouch(0).position.x <= 1450)
                {
                    if (Mathf.Abs(valueminimapx - gamemanager.MiniMap_CameraGuide.transform.localPosition.x) > 0.5f)
                    {
                        if (alreadyminimap == false)
                        {
                            gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_StartMinimap, "XR_StartMinimap_(" + gamemanager.xrmode.CameraWindow.transform.localPosition.x + ", " + gamemanager.xrmode.CameraWindow.transform.localPosition.y + ")", GetType().ToString());
                            alreadyminimap = true;
                        }
                        //gamemanager.MiniMap_Camera.transform.localPosition = new Vector3(gamemanager.minimapCamera_x, gamemanager.MiniMap_Camera.transform.localPosition.y, gamemanager.MiniMap_Camera.transform.localPosition.z);
                        gamemanager.MiniMap_CameraGuide.transform.localPosition = new Vector3(valueminimapx, gamemanager.MiniMap_Camera.transform.localPosition.y + 2, gamemanager.MiniMap_Camera.transform.localPosition.z);

                        gamemanager.xrmode.xpulse = ((valueminimapx - mapCamX.rect.width / 2 + mapX.rect.width / 2) / totalminimap * (XRMode_Manager.MaxPan - XRMode_Manager.MinPan)) + XRMode_Manager.MinPan;
                        gamemanager.xrmode.ypulse = gamemanager.xrmode.currentMotor_y;
                        gamemanager.xrmode.MoveCamera_Navi();
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            if (!gamemanager.Arrow.activeSelf && gamemanager.allbar.NaviRect.sizeDelta.x < AllBarOnOff.barOpen && gamemanager.allbar.LangRect.sizeDelta.x < AllBarOnOff.barOpen && gamemanager.allbar.ETCRect.sizeDelta.x < AllBarOnOff.barOpen)
            {
                if (gamemanager.joystick.enabled)
                {
                    gamemanager.joystick.enabled = false;
                }
                gamemanager.Arrow.SetActive(true);
                gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                gamemanager.Arrow.transform.GetChild(0).transform.localPosition = Vector3.zero;
            }

            gamemanager.clearmode.Resetothers();
            totalminimap = (mapX.rect.width - mapCamX.rect.width);

            if (Input.GetTouch(0).position.x >= 540 && Input.GetTouch(0).position.x <= 1450 && Input.GetTouch(0).position.y <= 100)
            {
                if (alreadyminimap == false)
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_StartMinimap, "Clear_StartMinimap_(" + gamemanager.clearmode.CameraWindow.transform.localPosition.x + ", " + gamemanager.clearmode.CameraWindow.transform.localPosition.y + ")", GetType().ToString());
                    alreadyminimap = true;
                }

                minimapCamera_x = ((Input.GetTouch(0).position.x - 540) / 910 * totalminimap) - mapX.rect.width / 2 + mapCamX.rect.width / 2;
                gamemanager.MiniMap_Camera.transform.localPosition = new Vector3(minimapCamera_x, gamemanager.MiniMap_Camera.transform.localPosition.y, gamemanager.MiniMap_Camera.transform.localPosition.z);
                float CameraMinimapPosition = minimapCamera_x / totalminimap * (gamemanager.clearmode.max_x - gamemanager.clearmode.min_x) + ((gamemanager.clearmode.min_x + gamemanager.clearmode.max_x) / 2);

                gamemanager.clearmode.CameraWindow.transform.localPosition = new Vector3(CameraMinimapPosition, gamemanager.clearmode.CameraWindow.transform.position.y, ClearMode.Cameraz);
            }
        }
    }

    /// <summary>
    /// 미니맵에 터치 시작
    /// </summary>
    public void Minimap_TouchOn()
    {
        //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Fast);
        gamemanager.speed_enum = GameManager.Speed_enum.fast;
        gamemanager.arrowmove.enabled = false;
        GameManager.StartMiniMapDrag = true;
    }

    /// <summary>
    /// 미니맵에 터치 완료
    /// </summary>
    public void Minimap_TouchOff()
    {
        gamemanager.arrowmove.enabled = true;
        GameManager.StartMiniMapDrag = false;
    }
}
