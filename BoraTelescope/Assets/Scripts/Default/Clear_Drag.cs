using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Drag : MonoBehaviour
{
    public ClearMode clearmode;

    public void DragRange_Origin()
    {
        /*
        if (Input.touchCount == 1 && clearmode.TouchStop == false)
        {
            if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x != LabelDetail.Detail_Close_x)  // 네비게이션 비활성화, 상세설명 활성화
            {
                if (Input.GetTouch(0).position.y < 350)
                {
                    if (Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.x >= 430 && Input.GetTouch(0).position.y > 120)
                    {
                        clearmode.DragStop = false;
                    }
                    else
                    {
                        clearmode.DragStop = true;
                    }
                }
                else if (Input.GetTouch(0).position.y >= 350)
                {
                    if (Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.x <= 1400)
                    {
                        clearmode.DragStop = false;
                    }
                    else
                    {
                        clearmode.DragStop = true;
                    }
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x == LabelDetail.Detail_Close_x)   // 네비게이션 비활성화, 상세설명 비활성화
            {
                if (Input.GetTouch(0).position.x <= 430 && Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.y <= 350 && Input.GetTouch(0).position.y >= 0)
                {
                    clearmode.DragStop = true;
                }
                else
                {
                    if (Input.GetTouch(0).position.y > 120)
                    {
                        if(!clearmode.gamemanager.gameObject.GetComponent<BoraJoyStick>().enabled)
                        {
                            clearmode.DragStop = false;
                        }
                    }
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x != LabelDetail.Detail_Close_x)  // 네비게이션 활성화, 상세설명 활성화
            {
                if (Input.GetTouch(0).position.x >= 410 && Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.y > 120)
                {
                    clearmode.DragStop = false;
                }
                else
                {
                    clearmode.DragStop = true;
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x == LabelDetail.Detail_Close_x)  // 네비게이션 활성화, 상세설명 비활성화
            {
                if (Input.GetTouch(0).position.x >= 410 && Input.GetTouch(0).position.y > 120)
                {
                    clearmode.DragStop = false;
                }
                else
                {
                    clearmode.DragStop = true;
                }
            }
        }*/
    }

    public void DragRange_FSfunction()      // filter기능, Season기능
    {/*
        if (Input.touchCount == 1 && clearmode.TouchStop == false)
        {
            if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x != LabelDetail.Detail_Close_x)  // 네비게이션 비활성화, 상세설명 활성화
            {
                if (Mathf.Abs(FunctionCustom.functionorigin.filterfunction.FilterBar.transform.localPosition.y - 508) < 0.5f)
                {
                    if (Input.GetTouch(0).position.y < 350)
                    {
                        if (Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.x >= 430 && Input.GetTouch(0).position.y > 120)
                        {
                            clearmode.DragStop = false;
                        }
                        else
                        {
                            clearmode.DragStop = true;
                        }
                    }
                    else if (Input.GetTouch(0).position.y >= 350)
                    {
                        if (Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.x <= 1400)
                        {
                            if (Input.GetTouch(0).position.x >= 600 && Input.GetTouch(0).position.x <= 1360 && Input.GetTouch(0).position.y >= 900 && Input.GetTouch(0).position.y <= 1015)
                            {
                                clearmode.DragStop = true;
                            }
                            else
                            {
                                clearmode.DragStop = false;
                            }
                        }
                        else
                        {
                            clearmode.DragStop = true;
                        }
                    }
                }
                else if (Mathf.Abs(FunctionCustom.functionorigin.filterfunction.FilterBar.transform.localPosition.y - 720) < 0.5f)
                {
                    if (Input.GetTouch(0).position.y < 350)
                    {
                        if (Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.x >= 430 && Input.GetTouch(0).position.y > 120)
                        {
                            clearmode.DragStop = false;
                        }
                        else
                        {
                            clearmode.DragStop = true;
                        }
                    }
                    else if (Input.GetTouch(0).position.y >= 350)
                    {
                        if (Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.x <= 1400)
                        {
                            clearmode.DragStop = false;
                        }
                        else
                        {
                            clearmode.DragStop = true;
                        }
                    }
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x == LabelDetail.Detail_Close_x)   // 네비게이션 비활성화, 상세설명 비활성화
            {
                if (Mathf.Abs(FunctionCustom.functionorigin.filterfunction.FilterBar.transform.localPosition.y - 508) < 0.5f)
                {
                    if (Input.GetTouch(0).position.x <= 430 && Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.y <= 300 && Input.GetTouch(0).position.y >= 120)
                    {
                        clearmode.DragStop = true;
                    }
                    else
                    {
                        if (Input.GetTouch(0).position.y > 120)
                        {
                            if (Input.GetTouch(0).position.x >= 600 && Input.GetTouch(0).position.x <= 1360 && Input.GetTouch(0).position.y >= 900 && Input.GetTouch(0).position.y <= 1015)
                            {
                                clearmode.DragStop = true;
                            }
                            else
                            {
                                clearmode.DragStop = false;
                            }
                        }
                    }
                }
                else if (Mathf.Abs(FunctionCustom.functionorigin.filterfunction.FilterBar.transform.localPosition.y - 720) < 0.5f)
                {
                    if (Input.GetTouch(0).position.x <= 430 && Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.y <= 350 && Input.GetTouch(0).position.y >= 0)
                    {
                        clearmode.DragStop = true;
                    }
                    else
                    {
                        if (Input.GetTouch(0).position.y > 120)
                        {
                            clearmode.DragStop = false;
                        }
                    }
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x != LabelDetail.Detail_Close_x)  // 네비게이션 활성화, 상세설명 활성화
            {
                if (Mathf.Abs(FunctionCustom.functionorigin.filterfunction.FilterBar.transform.localPosition.y - 508) < 0.5f)
                {
                    if (Input.GetTouch(0).position.x >= 410 && Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.y >= 100 && Input.GetTouch(0).position.y <= 900)
                    {
                        clearmode.DragStop = false;
                    }
                    else
                    {
                        clearmode.DragStop = true;
                    }
                }
                else if (Mathf.Abs(FunctionCustom.functionorigin.filterfunction.FilterBar.transform.localPosition.y - 720) < 0.5f)
                {
                    if (Input.GetTouch(0).position.x >= 410 && Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.y > 120)
                    {
                        clearmode.DragStop = false;
                    }
                    else
                    {
                        clearmode.DragStop = true;
                    }
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x == LabelDetail.Detail_Close_x)  // 네비게이션 활성화, 상세설명 비활성화
            {
                if (Mathf.Abs(FunctionCustom.functionorigin.filterfunction.FilterBar.transform.localPosition.y - 508) < 0.5f)
                {
                    if (Input.GetTouch(0).position.x >= 410)
                    {
                        if (Input.GetTouch(0).position.y > 100 && Input.GetTouch(0).position.y < 900)
                        {
                            clearmode.DragStop = false;
                        }
                        else
                        {
                            clearmode.DragStop = true;
                        }
                    }
                    else
                    {
                        clearmode.DragStop = true;
                    }
                }
                else if (Mathf.Abs(FunctionCustom.functionorigin.filterfunction.FilterBar.transform.localPosition.y - 720) < 0.5f)
                {
                    if (Input.GetTouch(0).position.x >= 410 && Input.GetTouch(0).position.y > 120)
                    {
                        clearmode.DragStop = false;
                    }
                    else
                    {
                        clearmode.DragStop = true;
                    }
                }
            }
        }*/
    }

    public void PinchZoom_Origin()
    {/*
        if (ClearMode.dontzoom == false)
        {
            if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x != LabelDetail.Detail_Close_x)  // 네비게이션 비활성화, 상세설명 활성화
            {
                if (Input.GetTouch(0).position.y < 300)
                {
                    if (Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.x >= 360 && Input.GetTouch(0).position.y > 100)
                    {
                        if (clearmode.alreadyPinchZoom == false)
                        {
                            clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Start(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                            clearmode.alreadyPinchZoom = true;
                        }
                        clearmode.StartPinchZoom();
                    }
                    else
                    {
                        if (clearmode.alreadyPinchZoom == true)
                        {
                            clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                            clearmode.alreadyPinchZoom = false;
                        }
                    }
                }
                else if (Input.GetTouch(0).position.y >= 300)
                {
                    if (Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.x <= 1400)
                    {
                        if (clearmode.alreadyPinchZoom == false)
                        {
                            clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Start(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                            clearmode.alreadyPinchZoom = true;
                        }
                        clearmode.StartPinchZoom();
                    }
                    else
                    {
                        if (clearmode.alreadyPinchZoom == true)
                        {
                            clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                            clearmode.alreadyPinchZoom = false;
                        }
                    }
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x == LabelDetail.Detail_Close_x)   // 네비게이션 비활성화, 상세설명 비활성화
            {
                if (Input.GetTouch(0).position.x <= 360 && Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.y <= 300 && Input.GetTouch(0).position.y >= 0)
                {
                    if (clearmode.alreadyPinchZoom == true)
                    {
                        clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                        clearmode.alreadyPinchZoom = false;
                    }
                }
                else
                {
                    if (Input.GetTouch(0).position.y > 100)
                    {
                        if (clearmode.alreadyPinchZoom == false)
                        {
                            clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Start(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                            clearmode.alreadyPinchZoom = true;
                        }
                        clearmode.StartPinchZoom();
                    }
                    else
                    {
                        if (clearmode.alreadyPinchZoom == true)
                        {
                            clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                            clearmode.alreadyPinchZoom = false;
                        }
                    }
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x != LabelDetail.Detail_Close_x)  // 네비게이션 활성화, 상세설명 활성화
            {
                if (Input.GetTouch(0).position.x >= 410 && Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.y > 100)
                {
                    if (clearmode.alreadyPinchZoom == false)
                    {
                        clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Start(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                        clearmode.alreadyPinchZoom = true;
                    }
                    clearmode.StartPinchZoom();
                }
                else
                {
                    if (clearmode.alreadyPinchZoom == true)
                    {
                        clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                        clearmode.alreadyPinchZoom = false;
                    }
                }
            }
            else if (clearmode.gamemanager.allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose && clearmode.labeldetail.Detail_Background.transform.localPosition.x == LabelDetail.Detail_Close_x)  // 네비게이션 활성화, 상세설명 비활성화
            {
                if (Input.GetTouch(0).position.x >= 410 && Input.GetTouch(0).position.y > 100)
                {
                    if (clearmode.alreadyPinchZoom == false)
                    {
                        clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Start(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                        clearmode.alreadyPinchZoom = true;
                    }
                    clearmode.StartPinchZoom();
                }
                else
                {
                    if (clearmode.alreadyPinchZoom == true)
                    {
                        clearmode.gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_PinchZoom, "Clear_PinchZoom:Finish(" + clearmode.CameraWindow.transform.parent.gameObject.transform.position.z + ")", GetType().ToString());
                        clearmode.alreadyPinchZoom = false;
                    }
                }
            }
        }*/
    }
}
