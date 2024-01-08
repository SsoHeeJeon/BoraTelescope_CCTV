using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class CameraClient : MonoBehaviour
{
    // 기존코드 => 스레드를 사용하지 않고 Update문으로만 돌렸는데 fps가 20아래로 나왔음
    // 스레드를 사용하고 rawImage에 받아온 byte를 뿌려주는 것은 Queue를 사용해서 했더니 150이상의 fps가 나옴
    private LogSendServer logsave;
    public GameObject CameraSet;
    public RawImage CameraWindow;
    Texture2D pickedImage;
    NetworkStream stream;
    public Thread ReadThread;
    public static Thread staticReadThread;
    private Queue<byte[]> queue_bytes = new Queue<byte[]>();

    //tcp
    const string serverIP = "127.0.0.1";
    const int port = 8002;
    const int orderport = 8003;

    TcpClient orderClient;
    TcpClient Client;
    byte[] recevBuffer;

    public static int camWidth = 0;
    public static int camHeight = 0;
    static int DataSize = 0;

    private bool alreadysend = false;
    public static bool EndThread = false;

    public CheckingView checkview;
    public static bool ConnectCamF = false;

    // Start is called before the first frame update
    void Start()
    {
        firstflag = true;
        logsave = GameObject.Find("GameManager").GetComponent<LogSendServer>();
        alreadysend = false;
        SpinCam();
        pickedImage = new Texture2D(camWidth, camHeight, TextureFormat.RGB24, false);
        DataSize = camWidth * camHeight * 3;    // 데이터 사이즈는 width x height x 3 (색상정보)
        recevBuffer = new byte[DataSize];
        staticReadThread = null;
        EndThread = false;
        if (staticReadThread is null)
        {
            staticReadThread = new Thread(new ThreadStart(ReadImage));
            staticReadThread.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (queue_bytes.Count > 0)
        {
            SeeCameraImage(queue_bytes.Dequeue());
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            DigitalZoom("Plus");
        } else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DigitalZoom("Minus");
        } else if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            DigitalZoom("ZoomOut");
        } else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            DigitalZoom("ZoomIn");
        }
    }    

    public void SpinCam()
    {
        var processList = System.Diagnostics.Process.GetProcessesByName("XRTeleSpinCam");
        if (processList.Length == 0)
        {
            System.Diagnostics.Process.Start(@"C:\XRTelesPinCam\XRTeleSpinCam.exe");
        }

        if (Client == null)
            Client = new TcpClient(serverIP, port);

        logsave.WriteLog(LogSendServer.NormalLogCode.Connect_Camera, "Connect_Camera_On", GetType().ToString());

        if (camHeight == 0 && camWidth == 0)
        {
            stream = Client.GetStream();
            recevBuffer = null;
            recevBuffer = new byte[9];
            stream.Read(recevBuffer, 0, recevBuffer.Length);    // 영상 사이즈 정보를 가져옴.
                                                                //Debug.Log(recevBuffer);

            camWidth = 1920;
            camHeight = 1080;
            string[] camsize = Encoding.UTF8.GetString(recevBuffer).Split('x');
            CameraSet.transform.position = new Vector3(0, 0, 530);
            //if (int.Parse(camsize[0]) == 2440 && int.Parse(camsize[1]) == 2048)
            //{
            //    CameraSet.transform.position = new Vector3(0, 0, 540);
            //} else if (int.Parse(camsize[0]) == 4096 && int.Parse(camsize[1]) == 3000)
            //{
            //    CameraSet.transform.position = new Vector3(0, 0, 540);
            //}
            //camWidth = int.Parse(camsize[0]);
            //camHeight = int.Parse(camsize[1]);
        }

        CameraWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(camWidth, camHeight);
    }

    bool firstflag = false;

    void ReadImage()
    {
        while (!EndThread)
        {
            if (Client != null)
            {
                stream = Client.GetStream();
                //recevBuffer = new byte[DataSize];
                stream.Read(recevBuffer, 0, recevBuffer.Length); // stream에 있던 바이트배열 내려서 새로 선언한 바이트배열에 넣기
                if (recevBuffer == null) return;

                queue_bytes.Enqueue(recevBuffer);       // recevBuffer의 크기를 할당해놓으면 stream.Read를 통해 자동으로 저장
                alreadysend = false;

                ConnectCamF = true;
            }
            else if (Client == null)
            {
                ConnectCamF = false;
                if (alreadysend == false)
                {
                    logsave.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_Connect_Camera, "Fail_Connect_Camera", GetType().ToString());
                    GameManager.AnyError = true;
                    SceneManager.LoadScene("Loading");
                    alreadysend = true;
                }
            }
        }
    }

    public void SeeCameraImage(byte[] cameradatas)
    {
        pickedImage.LoadRawTextureData(cameradatas);
        pickedImage.Apply();
        CameraWindow.texture = pickedImage;
    }

    public double zoomFactor = 1;
    
    public void DigitalZoom(string state)
    {
        if (state == "Plus")
        {
            zoomFactor = zoomFactor + 0.1;
        }
        else if (state == "Minus")
        {
            zoomFactor = zoomFactor - 0.1;
        }
        else if (state == "ZoomOut")
        {
            if (zoomFactor <=20)
            {
                //ChangeZoomOutFactor();
                Invoke("ChangeZoomOutFactor", 0.001f);

            }
        }
        else if (state == "ZoomIn")
        {
            if (zoomFactor >=1)
            {
                //ChangeZoomInFactor();
                Invoke("ChangeZoomInFactor", 0.001f);
            } else if(zoomFactor >= 20)
            {
                zoomFactor = 20;
            }
        } else if(state == "Origin")
        {
            zoomFactor = 1;
        }
        else
        {
            zoomFactor = (float.Parse(state));
        }
        zoomFactor = Mathf.Round((float)zoomFactor * 10) * 0.1f;
        GoZoomThread();
    }

    public static float purposezoom;
    public void ChangeZoomOutFactor()
    {
        zoomFactor = zoomFactor - 0.5;
        //if(zoomFactor <= 1)
        //{
        //    //print("zoomFactor1 = " + zoomFactor);
        //    zoomFactor = 1;
        //    CancelInvoke("ChangeZoomOutFactor");
        //    Debug.Log("changezoom 209");
        //}
        //else
        //{
        //    Invoke("ChangeZoomOutFactor", 0.02f);
        //    Debug.Log("changezoom 214");
        //}

        if(purposezoom >= zoomFactor)
        {
            zoomFactor = purposezoom;
            CancelInvoke("ChangeZoomOutFactor");
        } else if(purposezoom < zoomFactor)
        {
            Invoke("ChangeZoomOutFactor", 0.02f);
        }

        //if(zoomFactor!=1)
        //{
        //    zoomFactor = Mathf.Round((float)zoomFactor * 10) * 0.1f;
        //    Debug.Log("changezoom 220");
        //}

        if (zoomFactor != purposezoom)
        {
            zoomFactor = Mathf.Round((float)zoomFactor * 10) * 0.1f;
        }

        GoZoomThread();
        //print("zoomFactor = " + zoomFactor);
    }

    public void ChangeZoomInFactor()
    {
        zoomFactor = zoomFactor + 0.5;
        //if (zoomFactor >= 20)
        //{
        //    //print("zoomFactor1 = " + zoomFactor);
        //    zoomFactor = 20;
        //    CancelInvoke("ChangeZoomInFactor");
        //    Debug.Log("changezoom 234");
        //}
        //else
        //{
        //    Invoke("ChangeZoomInFactor", 0.02f);
        //    Debug.Log("changezoom 239");
        //}

        if(purposezoom <= zoomFactor)
        {
            zoomFactor = purposezoom;
            CancelInvoke("ChangeZoomInFactor");
        } else if(purposezoom > zoomFactor)
        {
            Invoke("ChangeZoomInFactor", 0.02f);
        }

        //if (zoomFactor != 20)
        //{
        //    zoomFactor = Mathf.Round((float)zoomFactor * 10) * 0.1f;
        //    Debug.Log("changezoom 245");
        //}

        if (zoomFactor != purposezoom)
        {
            zoomFactor = Mathf.Round((float)zoomFactor * 10) * 0.1f;
        }

        GoZoomThread();
    }

    public void GoZoomThread() {
        Thread thread = new Thread(
            () => {
                SendMessage((byte)(zoomFactor * 10));
            }
            );
        thread.Start();
        //print("zoomFactor = " + zoomFactor);
    }

    NetworkStream orderStream;
    private void SendMessage(byte factor)
    {
        if (orderClient == null)
        {
            orderClient = new TcpClient(serverIP, orderport);
            orderStream = orderClient.GetStream();
        }
        //Debug.Log("sd: " + factor.ToString());

        orderStream.WriteByte(factor);
        Thread.Sleep(10);
        byte[] bytes = new byte[10];
        int msg = orderStream.ReadByte();
        zoomFactor = (double)msg / 10;
    }
}