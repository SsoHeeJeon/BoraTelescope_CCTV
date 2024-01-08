using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Emgu.CV;
using System.IO;
using System.Threading;

public class CheckingView : MonoBehaviour
{
    public Camera Objcamera;       //보여지는 카메라.

    private int resWidth;
    private int resHeight;
    string path;

    public Thread ReadThread;
    public static Thread staticCannyThread;

    public List<string> ImagePath = new List<string>();
    int Imanum;
    float Waittime;
    public static string filename;
    public static bool StartChecking = false;
    public static bool CheckingHour = false;
    
    // Start is called before the first frame update
    void Start()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;

        path = "C:/ScreenShot/" + System.DateTime.Now.ToString("yyyy-MM");

        staticCannyThread = null;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(CheckingHour == true)
        {
            Waittime += Time.deltaTime;
            
            if((int)Waittime >= 3600)
            {
                Waittime = 0;
                CheckingHour = false;
            }
        }

        if(StartChecking == true && CameraClient.ConnectCamF == true)
        {
            CaptureView();
        }
    }
    
    public void CaptureView()
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(path);
        }
        string name;
        name = path + "/" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";
        filename = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        Objcamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
        Objcamera.Render();
        RenderTexture.active = rt;

        screenShot.ReadPixels(rec, 0, 0);

        byte[] bytes = screenShot.EncodeToJPG();
        File.WriteAllBytes(name, bytes);
        
        Objcamera.targetTexture = null;
        ImagePath.Add(name);
        Destroy(screenShot);

        CheckingHour = true;
        
        FunctionCustom.functionorigin.capturemode.UploadVideo();
        if (staticCannyThread is null)
        {
            staticCannyThread = new Thread(new ThreadStart(EdgeChecking));
            staticCannyThread.Start();
        }
        ResultCheckView();

        //FunctionCustom.functionorigin.capturemode.UploadVideo();
        //EdgeChecking();
    }

    public void EdgeChecking()
    {
        StartChecking = false;
        Imanum = 0;

        Mat InputImg = new Mat(ImagePath[ImagePath.Count - 1]);
        Mat output = new Mat();
        Mat hisimg = new Mat();

        CvInvoke.Resize(InputImg, output, new System.Drawing.Size(InputImg.Cols / 5, InputImg.Rows / 5));
        CvInvoke.Canny(output, hisimg, 50, 250);

        for (int index_C = 0; index_C < hisimg.Cols; index_C++)
        {
            for (int index_R = 0; index_R < hisimg.Rows; index_R++)
            {
                if (hisimg.GetData().GetValue(index_R, index_C).ToString() == "255")
                {
                    Imanum++;
                }
            }
        }

        resultcan = (float)Imanum / (float)(hisimg.Cols * hisimg.Rows) * 100;
        //hisimg.ToBitmap().Save(Application.dataPath + "/Image/" + index + "_Canny_" + resultcan.ToString("0.00") + "%.jpg", ImageFormat.Jpeg);
        //hisimg.ToBitmap().Save(Application.dataPath + "/Image/hisimg.jpg", ImageFormat.Jpeg);
    }

    float resultcan;

    public void ResultCheckView()
    {
        staticCannyThread = null;
        if (resultcan < 1.5f)
        {
            if (SceneManager.GetActiveScene().name.Contains("XRMode"))
            {
                NoticeWindow.NoticeWindowOpen("GoClearMode");
            }
        }
        else if (resultcan >= 1.5f)
        {
            if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                NoticeWindow.NoticeWindowOpen("GoXRMode");
            }
        }
    }
}
