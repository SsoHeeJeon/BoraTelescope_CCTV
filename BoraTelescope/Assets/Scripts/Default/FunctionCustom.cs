using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class FunctionCustom : MonoBehaviour
{
    public static GameManager gamemanager;
    public static FunctionOrigin functionorigin;

    public static Transform spot_XR;
    public static Transform[] spot;

    public enum TelescopeState
    {
        Left,
        Right,
    }
    public static TelescopeState telescopestate = 0;
    public static string[] PCinfo;

    public static bool OtherList = false;
    public static bool filterOff = false;
    public static bool Recordversion = false;
    public static bool SetPayment = false;
    public static bool ChangeSeason = false;
    public static bool OnceLiveXR = false;
    public static bool View360 = false;
    public static bool PastMode = false;
    public static bool GuideMode = false;
    public static bool VisitBook = false;
    public static bool TourismMode = false;

    public static void SetContentsFunc()
    {
        spot_XR = gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(1).transform;
        spot = new Transform[gamemanager.spot_set.Length];
        spot = (Transform[])gamemanager.spot_set.Clone();

        switch (ContentsInfo.ContentsName)
        {
            case "Basic":
                CaptureOn();
                //AddModeOn(1, "Heritage");
                //AddModeOn(2, "Etc");
                ModeBut_Off("Heritage");
                ModeBut_Off("Etc");
                ModeBut_Off("AutoGuide");
                ModeBut_Off("PastMode");
                //RecordCamOff();
                //PaymentSystem_Off();
                OtherList = false;
                break;
            case "GoldSunset":
                TelescopeOtherList();
                CaptureOn();
                VisitBookOn();
                AddModeOn(0, "TourismMode");
                ModeBut_Off("Etc");
                ModeBut_Off("AutoGuide");
                ModeBut_Off("PastMode");
                ModeBut_Off("FIlter");
                OnceLiveXR_Off();
                View360 = false;
                SelfiFunction.selfimode = false;
                Season_On();
                break;
            case "Woosuk":
                TelescopeOtherList();
                CaptureOff();
                VisitBookOn();
                //AddModeOn(0, "TourismMode");
                ModeBut_Off("Etc");
                ModeBut_Off("AutoGuide");
                ModeBut_Off("PastMode");
                ModeBut_Off("FIlter");
                OnceLiveXR_Off();
                View360 = false;
                SelfiFunction.selfimode = false;
                Season_Off();
                break;
        }
    }

    public static void TelescopeOtherList()
    {
        string path = @"c:\XRTeleSpinCam\bora_info.txt";
        if (File.Exists(path))
        {
            PCinfo = System.IO.File.ReadAllLines(path);
            PCinfo[0] = PCinfo[0].Replace("pc_system_id ", string.Empty);

            switch (ContentsInfo.ContentsName)
            {
                case "Apsan":
                    if (PCinfo[0] == "18")
                    {
                        telescopestate = TelescopeState.Left;
                    }
                    else if (PCinfo[0] == "19")
                    {
                        telescopestate = TelescopeState.Right;
                    }
                    break;
                case "Aegibong":
                    if (PCinfo[0] == "20" || PCinfo[0] == "21")
                    {
                        telescopestate = TelescopeState.Left;
                    }
                    else if (PCinfo[0] == "22" || PCinfo[0] == "23")
                    {
                        telescopestate = TelescopeState.Right;
                    }
                    break;
                case "Typhoon":
                    if (PCinfo[0] == "20" || PCinfo[0] == "21")
                    {
                        telescopestate = TelescopeState.Left;
                    }
                    else if (PCinfo[0] == "22" || PCinfo[0] == "23")
                    {
                        telescopestate = TelescopeState.Right;
                    }
                    break;
            }
        }

        OtherList = true;
    }

    public static void AddModeOn(int num, string modename)
    {
        if(num == 0)
        {
            gamemanager.MenuBar.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }

        switch (modename)
        {
            case "TourismMode":
                TourismMode = true;
                gamemanager.TourismModeBtn.SetActive(true);

                if (OnceLiveXR == false)
                {
                    gamemanager.TourismModeBtn.transform.localPosition = spot[num].localPosition;
                }else if(OnceLiveXR == true)
                {
                    gamemanager.TourismModeBtn.transform.localPosition = spot[num - 1].localPosition;
                }
                break;
            case "Etc":
                gamemanager.ETCModeBtn.SetActive(true);
                gamemanager.ETCBar.gameObject.SetActive(true);

                if (OnceLiveXR == false)
                {
                    gamemanager.ETCModeBtn.transform.localPosition = spot[num].localPosition;
                }
                else if (OnceLiveXR == true)
                {
                    gamemanager.ETCModeBtn.transform.localPosition = spot[num - 1].localPosition;
                }
                break;
            case "Guide":
                gamemanager.GuideModeBtn.SetActive(true);
                gamemanager.AutoSelectImg.gameObject.SetActive(true);
                
                if (OnceLiveXR == false)
                {
                    gamemanager.GuideModeBtn.transform.localPosition = spot[num].localPosition;
                }
                else if (OnceLiveXR == true)
                {
                    gamemanager.GuideModeBtn.transform.localPosition = spot[num - 1].localPosition;
                }
                
                GuideMode = true;
                break;
            case "PastMode":
                gamemanager.PastModeBtn.SetActive(true);
                
                if (OnceLiveXR == false)
                {
                    gamemanager.PastModeBtn.transform.localPosition = spot[num].localPosition;
                }
                else if (OnceLiveXR == true)
                {
                    gamemanager.PastModeBtn.transform.localPosition = spot[num - 1].localPosition;
                }
                
                PastMode = true;
                break;
            case "FIlter":
                functionorigin.filterfunction.FilterBar.transform.localPosition = new Vector3(functionorigin.filterfunction.FilterBar.transform.localPosition.x, 720, functionorigin.filterfunction.FilterBar.transform.localPosition.z);

                functionorigin.filterfunction.FilterBar.gameObject.SetActive(true);
                gamemanager.FilterBtn.SetActive(true);        // filter 버튼

                if (OnceLiveXR == false)
                {
                    gamemanager.FilterBtn.transform.localPosition = spot[num].localPosition;
                }
                else if (OnceLiveXR == true)
                {
                    gamemanager.FilterBtn.transform.localPosition = spot[num - 1].localPosition;
                }

                filterOff = false;
                break;
        }
    }

    public static void ModeBut_Off(string modename)
    {
        switch (modename)
        {
            case "TourismMode":
                if (gamemanager.TourismModeBtn != null)
                {
                    gamemanager.TourismModeBtn.SetActive(false);
                }
                break;
            case "Etc":
                if (gamemanager.ETCModeBtn != null)
                {
                    gamemanager.ETCModeBtn.SetActive(false);
                }
                if (gamemanager.ETCBar != null)
                {
                    gamemanager.ETCBar.gameObject.SetActive(false);
                }
                break;
            case "AutoGuide":
                if (gamemanager.GuideModeBtn != null)
                {
                    gamemanager.GuideModeBtn.SetActive(false);
                }
                if (gamemanager.AutoSelectImg != null)
                {
                    gamemanager.AutoSelectImg.gameObject.SetActive(false);
                }
                GuideMode = false;
                break;
            case "PastMode":
                if (gamemanager.PastModeBtn != null)
                {
                    gamemanager.PastModeBtn.SetActive(false);
                }
                PastMode = false;
                break;
            case "FIlter":
                if (functionorigin.filterfunction.FilterBar != null)
                {
                    functionorigin.filterfunction.FilterBar.gameObject.SetActive(false);
                }
                if (gamemanager.FilterBtn != null)
                {
                    gamemanager.FilterBtn.SetActive(false);        // filter 버튼
                }
                filterOff = true;
                break;
        }
    }

    /*
    public static void RecordCamOn()
    {
        gamemanager.setrecord.enabled = true;
        Recordversion = true;
    }

    public static void RecordCamOff()
    {
        gamemanager.setrecord.enabled = false;
        Recordversion = false;
    }
    */
    public static void CaptureOn()
    {
        if (SceneManager.GetActiveScene().name != "TelescopeMode")
        {
            if (SelfiFunction.selfimode == false)
            {
                gamemanager.CaptueObject.SetActive(true);
                gamemanager.CaptureBtn.gameObject.SetActive(true);        // Capture 버튼
            } else if(SelfiFunction.selfimode == true)
            {
                gamemanager.CaptureBtn.gameObject.SetActive(true);        // Capture 버튼

            }
        }
        else if (SceneManager.GetActiveScene().name == "TelescopeMode")
        {
            gamemanager.MenuBar.SetActive(true);
            gamemanager.MenuBar.GetComponent<Image>().enabled = true;
            for (int index = 0; index < gamemanager.MenuBar.transform.childCount; index++)
            {
                gamemanager.MenuBar.transform.GetChild(index).gameObject.SetActive(false);
            }
            for (int index = 0; index < gamemanager.MenuBar.transform.GetChild(0).transform.childCount; index++)
            {
                gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(index).gameObject.SetActive(false);
            }
            gamemanager.MenuBar.transform.GetChild(0).gameObject.SetActive(true);
            gamemanager.MenuBar.transform.GetChild(6).gameObject.SetActive(true);
            gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            gamemanager.CaptureBtn.gameObject.SetActive(true);
            //gamemanager.CaptureBtn.transform.position = gamemanager.MenuBar.transform.GetChild(4).position;
            gamemanager.CaptueObject.transform.position = new Vector3(960, 540);
        }
    }

    public static void CaptureOff()
    {
        if (gamemanager.CaptureBtn.activeSelf)
        {
            gamemanager.LanguageBtn.transform.localPosition = new Vector3(gamemanager.LanguageBtn.transform.localPosition.x, gamemanager.LanguageBtn.transform.localPosition.y - 88);
            gamemanager.Tipbtn.transform.localPosition = new Vector3(gamemanager.Tipbtn.transform.localPosition.x, gamemanager.Tipbtn.transform.localPosition.y - 88);
            gamemanager.Visitbtn.transform.localPosition = new Vector3(gamemanager.Visitbtn.transform.localPosition.x, gamemanager.Visitbtn.transform.localPosition.y - 88);

            SelfiFunction.selfimode = false;
            gamemanager.CaptureBtn.gameObject.SetActive(false);        // Capture 버튼
        }
    }

    public static void OnceLiveXR_On()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            gamemanager.xrmode.XRToggle.SetActive(true);
        }
        gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(2).localPosition = spot_XR.localPosition;
        GameManager.MainMode = "LiveMode";
        OnceLiveXR = true;
    }

    public static void OnceLiveXR_Off()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (gamemanager.xrmode.XRToggle.activeSelf)
            {
                gamemanager.xrmode.XRToggle.SetActive(false);
            }
        }
        gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(2).localPosition = spot[0].localPosition;
        OnceLiveXR = false;
    }

    public static void Season_On()
    {
        if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            functionorigin.SeasonPano = gamemanager.clearmode.gameObject.GetComponent<Clear_Pano>();
            functionorigin.SeasonPano.seasonBtn.gameObject.SetActive(true);
            //functionorigin.SeasonPano.ReadytoStart();
        }
        ChangeSeason = true;
    }

    public static void Season_Off()
    {
        if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            functionorigin.SeasonPano.seasonBtn.gameObject.SetActive(false);
            functionorigin.SeasonPano.seasonBtn.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        ChangeSeason = false;
    }

    public static void View360_On()
    {
        gamemanager.View360Btn.SetActive(true);
        functionorigin.view360.Open360();
        View360 = true;
    }

    public static void View360_Off()
    {
        gamemanager.View360Btn.SetActive(false);
        View360 = false;
    }

    public static void VisitBookOn()
    {
        VisitBook = true;
    }

    public static void VisitBookOff()
    {
        VisitBook = false;
    }

    /*
    public static void PaymentSystem_On()
    {
        SetPayment = true;
        gamemanager.checkplaytime.enabled = true;
        gamemanager.GetComponent<ReadJsonFile>().PaymentSystem_jsonfile();
        gamemanager.PlayTime.SetActive(true);
        if (SceneManager.GetActiveScene().name == "WaitingMode")
        {
            gamemanager.paymentsystem.enabled = true;
            gamemanager.paymentsystem.TouchImg.SetActive(false);
            gamemanager.paymentsystem.IdlePayment.gameObject.SetActive(true);
            //gamemanager.paymentsystem.TryPayment.gameObject.SetActive(true);
            gamemanager.paymentsystem.ReadytoStart();
            gamemanager.GetComponent<ReadJsonFile>().WriteLog = false;
        }
        else if (SceneManager.GetActiveScene().name == "TelescopeMode")
        {
            Debug.LogError("PaymentSystem_On");
            gamemanager.checkplaytime.GuideFinish();
            gamemanager.checkplaytime.GuidePlaytime_t.text = gamemanager.paymentsystem.NoticePlaytime_Idle.text;
        }
    }

    public static void PaymentSystem_Off()
    {
        SetPayment = false;
        gamemanager.checkplaytime.enabled = false;
        gamemanager.PlayTime.SetActive(false);
        if (SceneManager.GetActiveScene().name == "WaitingMode")
        {
            gamemanager.paymentsystem.TryPayment.gameObject.SetActive(false);
            gamemanager.paymentsystem.IdlePayment.gameObject.SetActive(false);
            gamemanager.paymentsystem.TouchImg.SetActive(true);
            gamemanager.paymentsystem.enabled = false;
        }

    }*/
}
