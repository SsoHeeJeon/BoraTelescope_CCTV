using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Loading : MonoBehaviour
{
    public GameManager gamemanager;
    public Slider progressBar;
    public static string nextScene;

    public Image CustmMode;
    public Sprite Tourism;
    public Sprite ClearMode;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gamemanager.UISetting();
        GameManager.SettingLabelPosition = false;
        //ConnectCamera();
        //gamemanager.pantiltorigin.ReadytoStart();

        if(nextScene == null)
        {
            nextScene = "Loading";
            if (File.Exists(Application.dataPath + ("/XRModeLabelPosition_" + ContentsInfo.ContentsName + ".json")))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Load_ARModeLabelPosition, "Load_XRModeLabelPosition", GetType().ToString());
            }
            else
            {
                gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.UnLoad_ARModeLabelPosition, "UnLoad_XRModeLabelPosition", GetType().ToString());
                GameManager.AnyError = true;
                nextScene = "ClearMode";
            }
            progressBar.value = 0;
        } else
        {/*
            GameManager.AnyError = false;
            ConnectCamera();
            if(PanTiltControl_v2.PanTiltControl.IsConnected == false)
            {
                GameManager.AnyError = true;
            } else if(PanTiltControl_v2.PanTiltControl.IsConnected == true)
            {
                GameManager.AnyError = false;
            }
            Debug.Log(PanTiltControl_v2.PanTiltControl.NowPanPulse + " / " + PanTiltControl_v2.PanTiltControl.NowTiltPulse);*/
        }

        //Debug.Log("today " + PanTiltControl_v2.PanTiltControl.IsConnected + GameManager.AnyError);
        LoadingScene();
    }

    float touchCount;
    /*
    private void Update()
    {
        touchCount += Time.deltaTime;
        Debug.Log((int)touchCount);
        if((int)touchCount >= 20)
        {
            GameManager.AnyError = true;
            if(GameManager.ModeActive[2] == true)
            {
                MoveClearMode();
            } else if(GameManager.ModeActive[2] == false)
            {
                MoveWaitingMode();
            }
            touchCount = 0;
        }
    }*/

    //tcp
    public const string serverIP = "127.0.0.1";
    public const int port = 8002;
    public TcpClient Client;
    NetworkStream stream;
    byte[] recevBuffer;

    public void ConnectCamera()
    {
        var processList = System.Diagnostics.Process.GetProcessesByName("SunAPITest");
        if (processList.Length == 0)
        {
            if (File.Exists(@"C:\BoraCCTVCam\SunAPITest.exe"))
            {
                System.Diagnostics.Process.Start(@"C:\BoraCCTVCam\SunAPITest.exe");
            } else
            {
                GameManager.AnyError = true;
                gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_Connect_Camera, "Fail_File_Camera", GetType().ToString());
            }
        }
        //CheckClient();
        Invoke("CheckClient", 0.1f);
    }

    public void CheckClient()
    {
        if (Client == null && GameManager.AnyError == false)
        {
            Client = new TcpClient(serverIP, port);
            stream = Client.GetStream();
            recevBuffer = null;
            recevBuffer = new byte[9];
            stream.Read(recevBuffer, 0, recevBuffer.Length);    // 영상 사이즈 정보를 가져옴.
            string[] camsize = System.Text.Encoding.UTF8.GetString(recevBuffer).Split('x');
            if (int.Parse(camsize[0]) == 0 || int.Parse(camsize[0]) == 0)
            {
                gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_Connect_Camera, "Fail_Connect_Camera", GetType().ToString());
                GameManager.AnyError = true;
            }
        }

        if (Client == null)
        {
            gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_Connect_Camera, "Fail_Connect_Camera", GetType().ToString());
            GameManager.AnyError = true;
        }
        else if (Client != null)
        {
            if (GameManager.AnyError == false)
            {
                GameManager.AnyError = false;
            }
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Connect_Camera, "Connect_Camera:On", GetType().ToString());
            Client.Close();
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Connect_Camera, "Connect_Camera:Off", GetType().ToString());
        }
    }

    public void LoadingScene()
    {
        switch (nextScene)
        {
            case "Loading":
                if (GameManager.AnyError == false)
                {
                    if (GameManager.ModeActive[0] == false && GameManager.ModeActive[1] == false && GameManager.ModeActive[2] == false)
                    {
                        NoticeWindow.NoticeWindowOpen("ErrorMessage");
                        //gamemanager.ErrorMessage.gameObject.SetActive(true);
                        if (GameManager.ModeActive.Length == 3)
                        {
                            MoveWaitingMode_Basic();
                        }else if(GameManager.ModeActive.Length > 3)
                        {

                        }
                    }
                    else if (GameManager.ModeActive[0] == false && GameManager.ModeActive[1] == true && GameManager.ModeActive[2] == false)
                    {
                        GameManager.MainMode = "XRMode";
                        MoveLoading();
                    }
                    else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[1] == false && GameManager.ModeActive[2] == false)
                    {
                        GameManager.MainMode = "LiveMode";
                        MoveLoading();
                    }
                    else if (GameManager.ModeActive[0] == false && GameManager.ModeActive[1] == false && GameManager.ModeActive[2] == true)
                    {
                        GameManager.MainMode = "ClearMode";
                        MoveLoading();
                    }
                    else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[1] == true && GameManager.ModeActive[2] == false)
                    {
                        GameManager.MainMode = "XRMode";
                        MoveLoading();
                    }
                    else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[1] == false && GameManager.ModeActive[2] == true)
                    {
                        GameManager.MainMode = "LiveMode";
                        MoveLoading();
                    }
                    else if (GameManager.ModeActive[0] == false && GameManager.ModeActive[1] == true && GameManager.ModeActive[2] == true)
                    {
                        GameManager.MainMode = "XRMode";
                        MoveLoading();
                    }
                    else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[1] == true && GameManager.ModeActive[2] == true)
                    {
                        MoveLoading();
                    }
                }
                else if (GameManager.AnyError == true)
                {
                    if (GameManager.ModeActive[2] == true)
                    {
                        GameManager.MainMode = "ClearMode";
                        MoveLoading();
                    }
                    else if (GameManager.ModeActive[2] == false)
                    {
                        NoticeWindow.NoticeWindowOpen("ErrorMessage");
                        //gamemanager.ErrorMessage.gameObject.SetActive(true);
                        if (GameManager.ModeActive.Length == 3)
                        {
                            MoveWaitingMode_Basic();
                        }
                        else if (GameManager.ModeActive.Length > 3)
                        {

                        }
                    }
                }
                break;
            case "LiveMode":
                if (GameManager.AnyError == false)
                {
                    if (GameManager.ModeActive[0] == true)
                    {
                        MoveXRMode();
                    }
                    else if (GameManager.ModeActive[0] == false)
                    {
                        NoticeWindow.NoticeWindowOpen("ErrorMessage");
                        //gamemanager.ErrorMessage.gameObject.SetActive(true);
                        if(GameManager.PrevMode == "XRMode")
                        {
                            if(GameManager.ModeActive[1] == true && GameManager.ModeActive[2] == true)
                            {
                                if(GameManager.AnyError == false)
                                {
                                    nextScene = "XRMode";
                                    MoveXRMode();
                                } else if(GameManager.AnyError == true)
                                {
                                    MoveClearMode();
                                }
                            } else if(GameManager.ModeActive[1] == true && GameManager.ModeActive[2] == false)
                            {
                                if(GameManager.AnyError == false)
                                {
                                    nextScene = "XRMode";
                                    MoveXRMode();
                                } else if(GameManager.AnyError == true)
                                {
                                    if (GameManager.ModeActive.Length == 3)
                                    {
                                        MoveWaitingMode_Basic();
                                    }
                                    else if (GameManager.ModeActive.Length > 3)
                                    {

                                    }
                                }
                            } else if(GameManager.ModeActive[1] == false && GameManager.ModeActive[2] == true)
                            {
                                MoveClearMode();
                            } else if(GameManager.ModeActive[1] == false && GameManager.ModeActive[2] == false)
                            {
                                if (GameManager.ModeActive.Length == 3)
                                {
                                    MoveWaitingMode_Basic();
                                }
                                else if (GameManager.ModeActive.Length > 3)
                                {

                                }
                            }
                        } else if(GameManager.PrevMode == "ClearMode")
                        {
                            if (GameManager.ModeActive[2] == true)
                            {
                                MoveClearMode();
                            }
                            else if (GameManager.ModeActive[2] == false)
                            {
                                NoticeWindow.NoticeWindowOpen("ErrorMessage");
                                //gamemanager.ErrorMessage.SetActive(true);
                                if (GameManager.AnyError == false)
                                {
                                    if(GameManager.ModeActive[1] == true)
                                    {
                                        nextScene = "XRMode";
                                        MoveXRMode();
                                    } else if(GameManager.ModeActive[1] == false)
                                    {
                                        if (GameManager.ModeActive.Length == 3)
                                        {
                                            MoveWaitingMode();
                                        }
                                        else if (GameManager.ModeActive.Length > 3)
                                        {

                                        }
                                    }
                                } else if(GameManager.AnyError == true)
                                {
                                    if (GameManager.ModeActive.Length == 3)
                                    {
                                        MoveWaitingMode_Basic();
                                    }
                                    else if (GameManager.ModeActive.Length > 3)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
                else if (GameManager.AnyError == true)
                {
                    NoticeWindow.NoticeWindowOpen("ErrorMessage");
                    //gamemanager.ErrorMessage.gameObject.SetActive(true);
                    if (GameManager.ModeActive[2] == true)
                    {
                        MoveClearMode();
                    }
                    else if (GameManager.ModeActive[2] == false)
                    {
                        if (GameManager.ModeActive.Length == 3)
                        {
                            MoveWaitingMode_Basic();
                        }
                        else if (GameManager.ModeActive.Length > 3)
                        {

                        }
                    }
                }
                break;
            case "XRMode":
                if(GameManager.AnyError == false)
                {
                    if (GameManager.ModeActive[1] == true)
                    {
                        MoveXRMode();
                    }
                    else if (GameManager.ModeActive[1] == false)
                    {
                        NoticeWindow.NoticeWindowOpen("ErrorMessage");
                        //gamemanager.ErrorMessage.gameObject.SetActive(true);
                        if(GameManager.PrevMode == "LiveMode")
                        {
                            if (GameManager.ModeActive[0] == false && GameManager.ModeActive[2] == false)
                            {
                                if (GameManager.ModeActive.Length == 3)
                                {
                                    MoveWaitingMode();
                                }
                                else if (GameManager.ModeActive.Length > 3)
                                {

                                }
                            }
                            else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[2] == false)
                            {
                                if (GameManager.AnyError == false)
                                {
                                    nextScene = "LiveMode";
                                    MoveXRMode();
                                } else if(GameManager.AnyError == true)
                                {
                                    if (GameManager.ModeActive.Length == 3)
                                    {
                                        MoveWaitingMode_Basic();
                                    }
                                    else if (GameManager.ModeActive.Length > 3)
                                    {

                                    }
                                }
                            }
                            else if (GameManager.ModeActive[0] == false && GameManager.ModeActive[2] == true)
                            {
                                MoveClearMode();
                            }
                            else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[2] == true)
                            {
                                if (GameManager.AnyError == false)
                                {
                                    nextScene = "LiveMode";
                                    MoveXRMode();
                                } else if(GameManager.AnyError == true)
                                {
                                    MoveClearMode();
                                }
                            }
                        } else if(GameManager.PrevMode == "ClearMode")
                        {
                            if (GameManager.ModeActive[0] == false && GameManager.ModeActive[2] == false)
                            {
                                if (GameManager.ModeActive.Length == 3)
                                {
                                    MoveWaitingMode();
                                }
                                else if (GameManager.ModeActive.Length > 3)
                                {

                                }
                            }
                            else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[2] == false)
                            {
                                if (GameManager.AnyError == false)
                                {
                                    nextScene = "LiveMode";
                                    MoveXRMode();
                                } else if(GameManager.AnyError == true)
                                {
                                    if (GameManager.ModeActive.Length == 3)
                                    {
                                        MoveWaitingMode_Basic();
                                    }
                                    else if (GameManager.ModeActive.Length > 3)
                                    {

                                    }
                                }
                            }
                            else if (GameManager.ModeActive[0] == false && GameManager.ModeActive[2] == true)
                            {
                                MoveClearMode();
                            }
                            else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[2] == true)
                            {
                                MoveClearMode();
                            }
                        }
                    }
                } else if(GameManager.AnyError == true)
                {
                    NoticeWindow.NoticeWindowOpen("ErrorMessage");
                    //gamemanager.ErrorMessage.gameObject.SetActive(true);
                    if (GameManager.ModeActive[2] == true)
                    {
                        MoveClearMode();
                    }
                    else if (GameManager.ModeActive[2] == false)
                    {
                        if (GameManager.ModeActive.Length == 3)
                        {
                            MoveWaitingMode_Basic();
                        }
                        else if (GameManager.ModeActive.Length > 3)
                        {

                        }
                    }

                }
                break;
            case "ClearMode":
                if (GameManager.ModeActive[2] == true)
                {
                    MoveClearMode();
                }
                else if (GameManager.ModeActive[2] == false)
                {
                    NoticeWindow.NoticeWindowOpen("ErrorMessage");
                    //gamemanager.ErrorMessage.gameObject.SetActive(true);
                    if (GameManager.PrevMode == "LiveMode")
                    {
                        if(GameManager.AnyError == false)
                        {
                            if(GameManager.ModeActive[0] == true && GameManager.ModeActive[1] == true)
                            {
                                nextScene = "LiveMode";
                                MoveXRMode();
                            } else if(GameManager.ModeActive[0] == true && GameManager.ModeActive[1] == false)
                            {
                                nextScene = "LiveMode";
                                MoveXRMode();
                            } else if(GameManager.ModeActive[0] == false && GameManager.ModeActive[1] == true)
                            {
                                nextScene = "XRMode";
                                MoveXRMode();
                            } else if(GameManager.ModeActive[0] == false && GameManager.ModeActive[1] == false)
                            {
                                if (GameManager.ModeActive.Length == 3)
                                {
                                    MoveWaitingMode();
                                }
                                else if (GameManager.ModeActive.Length > 3)
                                {

                                }
                            }
                        } else if(GameManager.AnyError == true)
                        {
                            if (GameManager.ModeActive.Length == 3)
                            {
                                MoveWaitingMode_Basic();
                            }
                            else if (GameManager.ModeActive.Length > 3)
                            {

                            }
                        }
                    } else if(GameManager.PrevMode == "XRMode")
                    {
                        if (GameManager.AnyError == false)
                        {
                            if (GameManager.ModeActive[0] == true && GameManager.ModeActive[1] == true)
                            {
                                nextScene = "XRMode";
                                MoveXRMode();
                            }
                            else if (GameManager.ModeActive[0] == true && GameManager.ModeActive[1] == false)
                            {
                                nextScene = "LiveMode";
                                MoveXRMode();
                            }
                            else if (GameManager.ModeActive[0] == false && GameManager.ModeActive[1] == true)
                            {
                                nextScene = "XRMode";
                                MoveXRMode();
                            }
                            else if (GameManager.ModeActive[0] == false && GameManager.ModeActive[1] == false)
                            {
                                if (GameManager.ModeActive.Length == 3)
                                {
                                    MoveWaitingMode();
                                }
                                else if (GameManager.ModeActive.Length > 3)
                                {

                                }
                            }
                        }
                        else if (GameManager.AnyError == true)
                        {
                            if (GameManager.ModeActive.Length == 3)
                            {
                                MoveWaitingMode_Basic();
                            }
                            else if (GameManager.ModeActive.Length > 3)
                            {

                            }
                        }
                    }
                }
                break;
            case "TourismMode":
                MoveETCMode("TourismMode");
                break;
            case "WaitingMode":
                MoveWaitingMode();
                break;
            case "VisitMode":
                StartCoroutine(LoadScene());
                break;
        }
    }

    public void MoveLoading()
    {
        nextScene = GameManager.MainMode;
        if (nextScene == "XRMode" || nextScene == "LiveMode")
        {
            //PantiltOrigin.State = PantiltOrigin.OriginState.SetOrigin;
            //gamemanager.pantiltorigin.StartOrigin = false;
            //gamemanager.pantiltorigin.FinishOrigin = false;
            Debug.Log("today MoveLoading");
            if (nextScene == "XRMode")
            {
                gamemanager.WantNoLabel = false;
                nextScene = "XRMode_" + ContentsInfo.ContentsName;
                MoveNextMode();
            }
            else if (nextScene == "LiveMode")
            {
                gamemanager.WantNoLabel = true;
                nextScene = "XRMode_" + ContentsInfo.ContentsName;
                MoveNextMode();
            }
        } else if(nextScene == "ClearMode")
        {
            MoveClearMode();
        }
    }

    public void MoveXRMode()
    {
        if (nextScene == "XRMode")
        {
            gamemanager.WantNoLabel = false;
            nextScene = "XRMode_" + ContentsInfo.ContentsName;
        } else if(nextScene == "LiveMode")
        {
            gamemanager.WantNoLabel = true;
            nextScene = "XRMode_" + ContentsInfo.ContentsName;
        }

        if (GameManager.PrevMode != "WaitingMode")
        {
            StartCoroutine(LoadScene());
        }
        else if(GameManager.PrevMode == "WaitingMode")
        {
            if(PantiltOrigin.State == PantiltOrigin.OriginState.Wait)
            {
                //gamemanager.pantiltorigin.StartOrigin = true;
                //gamemanager.pantiltorigin.FinishOrigin = true;
                Debug.Log("today MoveXRMode");
                //PantiltOrigin.State = PantiltOrigin.OriginState.SetOrigin;
                StartCoroutine(LoadScene());
            }
        }
    }

    public void MoveClearMode()
    {
        if (ContentsInfo.ContentsName != "GoldSunset")
        {
            nextScene = "ClearMode_" + ContentsInfo.ContentsName;
            StartCoroutine(LoadScene());
        } else if(ContentsInfo.ContentsName == "GoldSunset")
        {
            MoveETCMode("TourismMode");
        }
    }

    public void MoveETCMode(string modename)
    {
        nextScene = modename + "_" + ContentsInfo.ContentsName;
        StartCoroutine(LoadScene());
    }

    public void MoveWaitingMode()
    {
        //PantiltOrigin.State = PantiltOrigin.OriginState.SetOrigin;
        //gamemanager.pantiltorigin.StartOrigin = false;
        //gamemanager.pantiltorigin.FinishOrigin = false;
        Debug.Log("today MoveWaitingMode");
        nextScene = "WaitingMode";
        StartCoroutine(LoadScene());
    }

    public void MoveWaitingMode_Basic()
    {
        nextScene = "WaitingMode";
        StartCoroutine(LoadScene());
    }

    public void MoveNextMode()
    {
        //Debug.Log(progressBar.gameObject.name);
        //progressBar.value = 0;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        print(3123123);
        yield return new WaitForSeconds(1f);
        Debug.Log(nextScene);

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);

        if (op != null)
        {
            op.allowSceneActivation = false;

            float timer = 0.0f;
            while (!op.isDone)
            {
                yield return null;

                timer += Time.deltaTime;

                if (op.progress >= 0.9f)
                {
                    progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);

                    if (progressBar.value == 1.0f)
                        op.allowSceneActivation = true;

                }
                else
                {
                    progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                    if (progressBar.value >= op.progress)
                    {
                        timer = 0f;
                    }
                }
            }
            touchCount = 0;
        }
        else if (op == null)
        {
            gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_ChangeMode, "Fail_ChangeMode:" + nextScene, GetType().ToString());
        }
    }
}
