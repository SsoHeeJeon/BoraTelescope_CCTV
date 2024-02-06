using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Emgu.CV;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using LibVLCSharp;
using System;
using System.Net.Sockets;
using System.Text;

public static class Extensions
{
    public static void Swap<T>(this List<T> list, int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
public class CCTVViewer : MonoBehaviour
{
    NetworkStream stream;
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

    Texture2D playertexture;
    public RawImage CCTVMonitor;
    byte[] matData;
    public static Thread staticReadThread;
    public static bool EndThread = false;
    Queue<byte[]> queue_bytes = new Queue<byte[]>();
    Queue<IntPtr> queue_IntPtr = new Queue<IntPtr>();
    VideoCapture capture = new VideoCapture();
    Media media;

    // Start is called before the first frame update
    public void ReadytoStart()
    {
        SpinCam();

        playertexture = new Texture2D(camWidth, camHeight, TextureFormat.RGB24, false);

        DataSize = camWidth * camHeight * 3;    // 데이터 사이즈는 width x height x 3 (색상정보)
        recevBuffer = new byte[DataSize];
        BGRData = new byte[DataSize];
        //BGRData = new List<byte>();

        //playertexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        //capture = new VideoCapture("rtsp://admin:Bora7178@" + SunAPITest.CCTVControl.url + "/00/profile2/media.smp");
        //capture = new VideoCapture("rtsp://admin:Bora7178@172.30.1.126:554/0/onvif/profile2/media.smp");
        //capture = new VideoCapture("rtsp://admin:Bora7178@172.30.1.126:554/0/onvif/profile2/media.smp");
        //capture = new VideoCapture("rtsp://admin:Bora7178@172.30.1.126/00/profile2/media.smp");
        //capture = new VideoCapture(@"C:\Users\sohee\Downloads\High1_insta_1.mp4");
        //PlayerRTSP();
        staticReadThread = null;
        EndThread = false;
        if (staticReadThread is null)
        {
            staticReadThread = new Thread(new ThreadStart(ReadImage));
            staticReadThread.Start();
        }
        //SeeCCTV();
    }

    // Update is called once per frame
    void Update()
    {
        if (queue_bytes.Count > 0)
        {
            SeeCameraImage(queue_bytes.Dequeue());
        }

        //if (queue_IntPtr.Count > 0)
        //{
        //    SeeCameraImage(queue_IntPtr.Dequeue());
        //}

        if (Input.GetKeyDown("s"))
        {
            EndThread = false;
        }
    }

    public void SpinCam()
    {
        //var processList = System.Diagnostics.Process.GetProcessesByName("SunAPITest");
        //if (processList.Length == 0)
        //{
        //    System.Diagnostics.Process.Start(@"C:\BoraCCTVCam\SunAPITest.exe");
        //}

        if (Client == null)
            Client = new TcpClient(serverIP, port);
        //Debug.Log(port);
        //logsave.WriteLog(LogSendServer.NormalLogCode.Connect_Camera, "Connect_Camera_On", GetType().ToString());

        if (camHeight == 0 && camWidth == 0)
        {
            stream = Client.GetStream();
            matData = null;
            matData = new byte[9];
            stream.Read(matData, 0, matData.Length);    // 영상 사이즈 정보를 가져옴.
                                                        //Debug.Log(recevBuffer.Length);

            camWidth = 1920;
            camHeight = 1080;
            string[] camsize = Encoding.UTF8.GetString(matData).Split('x');
            //CameraSet.transform.position = new Vector3(0, 0, 530);
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

        //CameraWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(camWidth, camHeight);
    }

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
                //Debug.Log(recevBuffer.Length);
                ChangeRGB(recevBuffer);
                queue_bytes.Enqueue(BGRData);       // recevBuffer의 크기를 할당해놓으면 stream.Read를 통해 자동으로 저장
                //alreadysend = false;

                //ConnectCamF = true;
            }
            else if (Client == null)
            {
                //ConnectCamF = false;
                //if (alreadysend == false)
                //{
                //    logsave.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_Connect_Camera, "Fail_Connect_Camera", GetType().ToString());
                //    GameManager.AnyError = true;
                //    SceneManager.LoadScene("Loading");
                //    alreadysend = true;
                //}
            }
        }
    }

    byte[] BGRData;

    public void ChangeRGB(byte[] origindata)
    {
        //for (int index = 0; index < origindata.Length; index++)
        //{
        //    int a = index % 3;
        //    switch (a)
        //    {
        //        case 0:
        //            BGRData[index + 2] = origindata[index];
        //            break;
        //        case 1:
        //            BGRData[index] = origindata[index];
        //            break;
        //        case 2:
        //            BGRData[index - 2] = origindata[index];
        //            break;
        //    }
        //}

        for (int index = 0; index < origindata.Length; index += 3)
        {
            BGRData[index + 2] = origindata[index];
            BGRData[index + 1] = origindata[index + 1];
            BGRData[index] = origindata[index + 2];
        }
    }

    uint i_videoHeight = 0;
    uint i_videoWidth = 0;

    public void SeeCCTV()
    {
        while (true)
        {
            if (i_videoWidth == 0 && i_videoHeight == 0)
            {
                //uint i_videoHeight = 0;
                //uint i_videoWidth = 0;

                mediaPlayer.Size(0, ref i_videoWidth, ref i_videoHeight);
                var texptr = mediaPlayer.GetTexture(i_videoWidth, i_videoHeight, out bool updated);
                //gamemanager.WriteLog(LogSendServer.NormalLogCode.ChangeMode, "LiveStreaming Ready", GetType().ToString());
                if (i_videoWidth != 0 && i_videoHeight != 0 && updated && texptr != IntPtr.Zero)
                {
                    Debug.Log("Creating texture with height " + i_videoHeight + " and width " + i_videoWidth);
                    queue_IntPtr.Enqueue(texptr);
                    //playertexture = Texture2D.CreateExternalTexture((int)i_videoWidth,
                    //    (int)i_videoHeight,
                    //    TextureFormat.RGB24,
                    //    false,
                    //    true,
                    //    texptr);
                    //CCTVMonitor.texture = playertexture;
                    //GetComponent<Renderer>().material.mainTexture = tex;
                    //this.gameObject.GetComponent<AutoStreaming>().rawim.GetComponent<RawImage>().texture = tex;
                }
            }
            else
            {
                var texptr = mediaPlayer.GetTexture(i_videoWidth, i_videoHeight, out bool updated);
                if (updated)
                {
                    queue_IntPtr.Enqueue(texptr);
                    //playertexture.UpdateExternalTexture(texptr);
                    //CCTVMonitor.texture = playertexture;
                }
            }
        }
        //Invoke("SeeCCTV", 0.001f);
    }

    MediaPlayer mediaPlayer;

    public void PlayerRTSP()
    {/*
        var libVLC = new LibVLC();
        
        mediaPlayer = new MediaPlayer(libVLC);

        media = new Media(libVLC, "rtsp://admin:Bora7178@172.30.1.126/00/profile2/media.smp", FromType.FromLocation);

        mediaPlayer.Play(media);
        SeeCCTV();*/
        /*
        //if (!capture.IsOpened)
        //{
        //    Debug.Log(capture.IsOpened);
        //}
        using (Mat image = new Mat()) // Frame image buffer
        {
            while (true)
            {
                //if (!capture.Read(image))
                //{
                //    Cv2.WaitKey();
                //}

                //if (!capture.IsOpened)
                //{
                //    Debug.Log(capture.IsOpened);
                //    return;
                //}

                capture.Read(image);
                if (image == null)
                {
                    //capture.Read(image);

                    int imgHeight = image.Rows;
                    int imgWidth = image.Cols;

                    matData = new byte[imgHeight * imgWidth * 3];

                    //image.GetRawData().CopyTo(matData, 0);
                }
                else
                {
                    //capture.Read(image);

                    if (image.Cols > 0 && image.Rows > 0)
                    {
                        //Get the height and width of the Mat 
                        //int imgHeight = image.Height;
                        //int imgWidth = image.Width;

                        //byte[] matData = new byte[imgHeight * imgWidth * 3];

                        //image.GetRawData().CopyTo(matData, 0);


                        //window.ShowImage(image);
                        //Bitmap bitmap = BitmapExtension.ToBitmap(image);
                        queue_bytes.Enqueue(matData);
                        
                        
                    }

                    //if (Cv2.WaitKey(1) >= 0)
                    //    break;
                    //Cv2.WaitKey(sleepTime);
                }
            }
        }*/
    }

    //public void SeeCameraImage(IntPtr img)
    //{
    //    if (playertexture == null)
    //    {
    //        playertexture = Texture2D.CreateExternalTexture((int)i_videoWidth, (int)i_videoHeight,
    //                TextureFormat.RGB24,
    //                false,
    //                true,
    //                img);
    //    }
    //    else if (playertexture != null)
    //    {
    //        playertexture.UpdateExternalTexture(img);
    //        CCTVMonitor.texture = playertexture;
    //    }
    //}

    public void SeeCameraImage(byte[] matData)
    {
        playertexture.LoadRawTextureData(matData);
        playertexture.Apply();
        CCTVMonitor.texture = playertexture;
    }

    //void MatToTexture(Mat sourceMat)
    //{
    //    //Get the height and width of the Mat 
    //    int imgHeight = sourceMat.Height;
    //    int imgWidth = sourceMat.Width;

    //    byte[] matData = new byte[imgHeight * imgWidth];

    //    //Get the byte array and store in matData
    //    //sourceMat.GetArray(0, 0, matData);
    //    sourceMat.GetData().CopyTo(matData, 0);
    //    //Create the Color array that will hold the pixels 
    //    Color32[] c = new Color32[imgHeight * imgWidth];

    //    //Get the pixel data from parallel loop
    //    Parallel.For(0, imgHeight, i => {
    //        for (var j = 0; j < imgWidth; j++)
    //        {
    //            byte vec = matData[j + i * imgWidth];
    //            var color32 = new Color32
    //            {
    //                r = vec,
    //                g = vec,
    //                b = vec,
    //                a = 0
    //            };
    //            c[j + i * imgWidth] = color32;
    //        }
    //    });

    //    //Create Texture from the result
    //    Texture2D tex = new Texture2D(imgWidth, imgHeight, TextureFormat.RGBA32, true, true);
    //    tex.SetPixels32(c);
    //    tex.Apply();
    //}
}
