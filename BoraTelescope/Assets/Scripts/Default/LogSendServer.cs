using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;
using System.Net;
using System.Linq;

public class LogData
{
    public string Timestamp;
    public string Type;
    public int LogCode;
    public string LogInformation;
    public string ScriptName;
    public string ContentsVersion;

    public LogData(string timestamp, string type, int codenum, string loginfo, string scriptname, string contentsversion)
    {
        Timestamp = timestamp;
        Type = type;
        LogCode = codenum;
        LogInformation = loginfo;
        ScriptName = scriptname;
        ContentsVersion = contentsversion;
    }
}

public class LogSendServer : bora_client_test
{
    // �Ϲݷα� : Timestamp [Normal] (�α��ڵ�) ������ ����/��ũ��Ʈ �̸�/������ ����
    // �����α� : Timestamp [Error] (�����ڵ�) ������ ����-����/��ũ��Ʈ �̸�/������ ����

    public enum NormalLogCode
    {
        StartContents = 1001,
        EndContents = 1002,
        Connect_SystemControl = 1003,
        Connect_Pantilt = 1004,
        Connect_Camera = 1005,
        ChangeMode = 1006,
        Load_ARModeLabelPosition = 1007,
        Load_ResourceFile = 1008,
        Load_PaymentFile = 1009,
        ChangeLanguage = 1010,
        ClickHomeBtn = 1011,

        AR_StartArrow = 2001,
        AR_FinishArrow = 2002,
        AR_SelectLabel = 2003,
        AR_SelectNavi = 2004,
        AR_Detail = 2005,
        AR_DetailMore = 2006,
        AR_DetailSound = 2007,
        AR_DetailClose = 2008,
        AR_Navigation = 2009,
        AR_CategorySelect = 2010,
        AR_Filter = 2011,
        AR_Capture = 2012,
        AR_ImageUpload = 2013,
        AR_ImageListConfirm = 2014,
        AR_QRCode = 2015,
        AR_StartMinimap = 2016,
        AR_FinishMinimap = 2017,
        AR_Joystick = 2018,
        AR_DragStart = 2019,
        AR_DragFinish = 2020,
        AR_Zoom = 2021,
        AR_PinchZoom = 2022,
        AR_Hotspot = 2023,

        Clear_StartArrow = 3001,
        Clear_FinishArrow = 3002,
        Clear_StartDrag = 3003,
        Clear_FinishDrag = 3004,
        Clear_StartMinimap = 3005,
        Clear_FinishMinimap = 3006,
        Clear_Zoom = 3007,
        Clear_PinchZoom = 3008,
        Clear_SelectLabel = 3009,
        Clear_SelectNavi = 3010,
        Clear_Detail = 3011,
        Clear_DetailMore = 3012,
        Clear_DetailSound = 3013,
        Clear_DetailClose = 3014,
        Clear_Navigation = 3015,
        Clear_CategorySelect = 3016,
        Clear_Filter = 3017,
        Clear_Capture = 3018,
        Clear_ImageUpload = 3019,
        Clear_ImageListConfirm = 3020,
        Clear_QRCode = 3021,
        Clear_Joystick = 3022,
        Clear_Season = 3023,
        Clear_Hotspot = 3024,

        Live_StartArrow = 4001,
        LIve_FinishArrow = 4002,
        Live_DragStart = 4003,
        LIve_DragFinish = 4004,

        Etc_SelectLabel = 5001,
        Etc_Detail = 5002,
        Etc_DetailMore = 5003,
        Etc_DetailSound = 5004,
        Etc_DetailClose = 5005,
        Etc_PantiltOrigin = 5006,
        Etc_PantiltStartPosition = 5007,
        Etc_YoutubeLive = 5008,
        Etc_Chromakey = 5009,
        ETC_Movie360 = 5010,
        Visit_Start = 5011,
        Visit_See = 5012,
        Visit_Write = 5013,
        Visit_Out = 5014,
        Visit_Past = 5015,
        Visit_future = 5016,
        Visit_Save = 5017,
        Selfi_Start = 5018,
        Selfi_Photo = 5019,
        Selfi_Custom = 5020,
        Selfi_Download = 5021,
        Selfi_RePhoto = 5022,
        Selfi_Cancel = 5023,
        Selfi_QRCode = 5024,

        Record_Start = 6001,
        Record_End = 6002,
        Record_Upload = 6003,
        Record_Save = 6004,

        Payment_Start = 7001,
        Payment_Wait = 7002,
        Payment_Success = 7003,
        Payment_Fail = 7004,
        Payment_Reset = 7005,
    }
    public NormalLogCode lognum;

    public enum ErrorLogCode
    {
        DisConnect_SystemControl = 1001,
        Fail_Connect_Pantilt = 1002,
        Fail_Connect_Camera = 1003,
        UnLoad_ARModeLabelPosition = 1004,
        UnLoad_ResourceFile = 1005,
        Fail_ChangeMode = 1006,
        Fail_ImageUpload = 1007,
        Fail_ImageListConfirm = 1008,
        Fail_EnterMode = 1009,
        Fail_EtcPantilt = 1010,
        Fail_RecordUpload = 1011,
        UnLoad_PaymentFile = 1012,
        Fail_QRUpload = 1013,
        Fail_InternetConnect = 1014,

        UnityError = 2001,
        UnityException = 2002
    }
    public ErrorLogCode errornum;

    private string timestamp;
    private string logType;
    private int LogCode;
    public static string ContentsVersion;

    /// <summary>
    /// �α�����
    /// </summary>
    /// <param name="lognum"></param>
    /// <param name="loginfo"></param>
    /// <param name="scriptname"></param>
    public void WriteLog(NormalLogCode lognum, string loginfo, string scriptname)
    {
        logType = "[NORMAL]";
        //ContentsVersion = Application.version;

        LogCode = (int)lognum;

        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        LogData logdata = new LogData(timestamp, logType, LogCode, loginfo, scriptname, ContentsVersion);
        //string str = JsonUtility.ToJson(logdata);
        //saveLog(str);
        //savestringLog(timestamp + "`^" + logType + "`^" + LogCode + "`^" + loginfo + "`^" + scriptname + "`^" + ContentsVersion);
        Send_Log_Button(timestamp + "`^" + logType + "`^" + LogCode + "`^" + loginfo + "`^" + scriptname + "`^" + ContentsVersion);
    }

    /// <summary>
    /// �����α� ����
    /// </summary>
    /// <param name="errornum"></param>
    /// <param name="loginfo"></param>
    /// <param name="scriptname"></param>
    public void WriteErrorLog(ErrorLogCode errornum, string loginfo, string scriptname)
    {
        logType = "[ERROR]";
        //ContentsVersion = Application.version;

        LogCode = (int)errornum;

        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        LogData logdata = new LogData(timestamp, logType, LogCode, loginfo, scriptname, ContentsVersion);
        //string str = JsonUtility.ToJson(logdata);
        //saveLog(str);
        //savestringLog(timestamp + "`^" + logType + "`^" + LogCode +"`^" + loginfo + "`^" + scriptname + "`^" + ContentsVersion);
        Send_Error_Button(timestamp + "`^" + logType + "`^" + LogCode + "`^" + loginfo + "`^" + scriptname + "`^" + ContentsVersion);
    }

    List<string> Log_json = new List<string>();
    List<string> Log_Text = new List<string>();
    string allLog;
    int filenum;

    /// <summary>
    /// ���α� ���� ���Ͽ� ����
    /// </summary>
    /// <param name="str"></param>
    public void saveLog(string str)
    {
        allLog = "";
        Log_json.Add(str);

        for(int index =0; index < Log_json.Count; index++)
        {
            allLog += Log_json[index] + System.Environment.NewLine;
        }

        File.WriteAllText(Application.dataPath + ("/LogData_" + ContentsInfo.ContentsName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".json"), allLog);
        //Debug.Log(str);
    }

    /// <summary>
    /// ��� �α� ���� ���Ͽ� ����(Txt����)
    /// </summary>
    /// <param name="str"></param>
    public void savestringLog(string str)
    {
        allLog = "";
        Log_Text.Add(str);

        for (int index = 0; index < Log_Text.Count; index++)
        {
            allLog += Log_Text[index] + System.Environment.NewLine;
        }

        File.WriteAllText(Application.dataPath + ("/LogData_" + ContentsInfo.ContentsName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"), allLog);
    }

    public bool IsInternetConnected()
    {
        const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
        const string NCSI_TEST_RESULT = "Microsoft NCSI";
        const string NCSI_DNS = "dns.msftncsi.com";
        const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";

        try
        {
            // Check NCSI test link
            var webClient = new WebClient();
            //string result = webClient.DownloadString(NCSI_TEST_URL);
            string result = new TimedWebClient { Timeout = 500 }.DownloadString(NCSI_TEST_URL);
            if (result != NCSI_TEST_RESULT)
            {
                return false;
            }

            // Check NCSI DNS IP
            var dnsHost = Dns.GetHostEntry(NCSI_DNS);
            if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return false;
        }

        return true;
    }

    public class TimedWebClient : WebClient
    {
        // Timeout in milliseconds, default = 600,000 msec
        public int Timeout { get; set; }

        public TimedWebClient()
        {
            this.Timeout = 100;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = this.Timeout;
            return objWebRequest;
        }
    }
}
