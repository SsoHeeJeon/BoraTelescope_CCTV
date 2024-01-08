using Amazon.S3.Model;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Amazon.S3;
using Amazon;
using System;
using UnityEngine.SceneManagement;
using System.Net.Http;

public class UploadImage : QRMaker
{
    public string IdentityPoolId = "";
    public static string CognitoIdentityRegion = RegionEndpoint.APNortheast2.SystemName;
    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }
    
    private IAmazonS3 _s3Client;
    private string fileName;

    public static bool awsputImage = false;

    private void Update()
    {
        gamemanager.CaptueObject.gameObject.GetComponent<ScreenCapture>().playFlashEffect();

        if (ScreenCapture.counttime == true)
        {
            if (QRCodeImage.transform.parent.gameObject.transform.GetChild(1).gameObject.activeSelf)
            {
                QRCodeImage.transform.parent.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;
            }
        }

        if (awsputImage == true)
        {
            GetListbucketObjects();
            awsputImage = false;
        }
    }

    private void makeS3Client()
    {
        string accessKey = "AKIA6GOREEPHZOD6LEZ7";       // AWS IAM AccessKey ����
        string secretKey = "AwQ38XC3LRfsRMVm7EYx/wgaMa9JtI4ZrD98jl4m";  //AWS IAM SecretKey ����

        _s3Client = new AmazonS3Client(accessKey, secretKey, _CognitoIdentityRegion);
    }
    
    public void GetListbucketObjects()
    {
        makeS3Client();

        var request = new ListObjectsRequest()
        {
            BucketName = "borabucket"
        };

        using (_s3Client) 
        {
            try
            {
                var listObjectsResponse = _s3Client.ListObjects(request);

                for (int index = 0; index < listObjectsResponse.S3Objects.Count; index++)
                {
                    if(listObjectsResponse.S3Objects[index].Key == fileName)
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_ImageListConfirm, "ImageListConfirm", GetType().ToString());
                    }
                }
            }
            catch(Exception e)
            {
                Debug.Log(e);
                gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_ImageListConfirm, "Fail_ImageListConfirm : " + e, GetType().ToString());
                QRCodeImage.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                //�����޼��� �ʿ�
            }
        }
    }
    
    public void PutImageObject(string filepath, string filename)
    {
        HttpClient httpClient = new HttpClient();
        MultipartFormDataContent form = new MultipartFormDataContent();

        byte[] imagebytearraystring = ImageFileToByteArray(filepath); // ���� ��� �ֱ�
        form.Add(new ByteArrayContent(imagebytearraystring, 0, imagebytearraystring.Length), "boraphotofile", filename); // key �̸�, ���ε� �� �� �̸�
        
        try
        {
            HttpResponseMessage response = httpClient.PostAsync("https://bora.web.awesomeserver.kr/info/BoraUploadForPhotoToS3", form).Result; // ��û�� ������ �ּ� (�ݵ�� http �� https �� �����ؾ���)
            httpClient.Dispose();
            //string sd = response.Content.ReadAsStringAsync().Result; // ���������� �Ϸ� �� �� ���� �������� �亯 ��
            url = response.Content.ReadAsStringAsync().Result; // ���������� �Ϸ� �� �� ���� �������� �亯 ��
            Console.WriteLine(url);

            if (url == "Fail Upload")
            {
                return;
            }
            else if (url.Contains("error") || url.Contains("Error"))
            {
                Debug.Log("no");
                return;
            }
            if (GameManager.internetCon == true)
            {
                MakeQRCode();
            } else if(GameManager.internetCon == false)
            {
                FunctionCustom.functionorigin.capturemode.CaptureEndCamera();
                NoticeWindow.NoticeWindowOpen("ErrorInternet");
                //gamemanager.ErrorMessage.SetActive(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            FunctionCustom.functionorigin.capturemode.CaptureEndCamera();
            NoticeWindow.NoticeWindowOpen("ErrorInternet");
            //gamemanager.ErrorMessage.SetActive(true);
        }
    }

    byte[] ImageFileToByteArray(string fullFilePath)
    {
        FileStream fs = File.OpenRead(fullFilePath);
        byte[] bytes = new byte[fs.Length];
        fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
        fs.Close();
        return bytes;
    }
    
    string savepath;
    string savefilename;

    public void UploadVideo()
    {
        GetBoranum();
        //savepath = FFmpegEncoder.originsavepath;
        savepath = "C:/ScreenShot/" + System.DateTime.Now.ToString("yyyy-MM") + "/" + CheckingView.filename;
        savefilename = "private/" + boranum + "/" + CheckingView.filename;
        //UploadReal();
        Invoke("WaitUpload", 5f);
    }

    public void WaitUpload()
    {
        makeS3Client();

        fileName = savefilename; // hi/1/asdf.mp4
        
        var stream = new FileStream(savepath, FileMode.Open);

        var request = new PutObjectRequest()
        {
            BucketName = "borabucket",
            Key = fileName,
            InputStream = stream
        };

        try
        {
            _s3Client.PutObject(request);
            //Debug.Log("WaitUpload");
            awsputImage = true;
        }
        catch(Exception e)
        {
            Debug.Log(e);
            gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_ImageUpload, "Fail_ImageUpload : " + e, GetType().ToString());
            QRCodeImage.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //�����޼��� �ʿ�
        }
    }
}
