using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentsInfo : LogSendServer
{
    public static string ContentsName;

    public GameObject GM;
    public GameManager gamemanager;
    public FunctionOrigin functionorigin;
    private Label label_open;
    //public GameObject SetLang;

    public string[] WaitingVideo_path;
    public static bool[] ModeActive = new bool[3];

    public static bool AwakeOnce = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (AwakeOnce == false)     // �ѹ��� ����
        {
            // ������ ���� ����
            //ContentsName = "Basic";
            ContentsName = "GoldSunset";
            //ContentsName = "High1";
            //ContentsName = "OceanCafe";
            //ContentsName = "Seongsan";

            FunctionCustom.gamemanager = gamemanager;
            FunctionCustom.functionorigin = functionorigin;
            label_open = gamemanager.label;

            //���õ� ������ ������ ���� �ش� ��ũ��Ʈ���� �ҷ��� �̹��� �� ���ҽ� ���� �ҷ�����
            TelescopeInfo();
            WriteLog(NormalLogCode.StartContents, "StartContents", GetType().ToString());       // ������ ���� �α� ����

            Connect_Button();       // �ý��� ��Ʈ�ѷ� ���α׷��� �����Ͽ� �������÷��� �ҷ�����
            WriteLog(NormalLogCode.Connect_SystemControl, "Connect_SystemControl_On", GetType().ToString());        // �ҷ��� ������ �÷��� �α׷� ǥ��
            gamemanager.GetComponent<GameManager>().UISetting();       // UI ����
            AwakeOnce = true;
        }
    }

    /// <summary>
    /// �޾ƿ� ������ ������ ���� �ش� ��ũ��Ʈ�� �̿��ؼ� ���� ���ҽ� �ҷ�����
    /// </summary>
    /// <param name="Telescope"></param>
    public void TelescopeInfo()
    {
        //label_open.Label_Position = new Vector3[label_open.Label_total.Count];
        /*
        gamemanager.GetComponent<ReadJson>().Readfile();

        label_open.Title_K = ReadJson.Title_K.ToArray();
        label_open.Title_E = ReadJson.Title_E.ToArray();
        label_open.Title_C = ReadJson.Title_C.ToArray();
        label_open.Title_J = ReadJson.Title_J.ToArray();

        label_open.DetailTexts_K = ReadJson.DetailText_K.ToArray();
        label_open.DetailTexts_E = ReadJson.DetailText_E.ToArray();
        label_open.DetailTexts_C = ReadJson.DetailText_C.ToArray();
        label_open.DetailTexts_J = ReadJson.DetailText_J.ToArray();
        */
        switch (ContentsName)
        {
            case "GoldSunset":
                GoldSunsetLabel.LoadLabelInfo();

                WriteLog(LogSendServer.NormalLogCode.Load_ResourceFile, "Load_ResourceFile", GetType().ToString());
                label_open.Label_total = GoldSunsetLabel.Label_total;
                label_open.Label_Cate_1 = GoldSunsetLabel.Label_Cate_1;
                label_open.Label_Cate_2 = GoldSunsetLabel.Label_Cate_2;

                label_open.NaviLabel = (Sprite[])GoldSunsetLabel.NaviLabel.Clone();
                label_open.MapLabel = (Sprite[])GoldSunsetLabel.MapLabel.Clone();
                //label_open.NaviLabel_C = (Sprite[])GoldSunsetLabel.NaviLabel_C.Clone();
                //label_open.NaviLabel_J = (Sprite[])GoldSunsetLabel.NaviLabel_J.Clone();
                label_open.DetailImage = (Sprite[])GoldSunsetLabel.DetailImage.Clone();
                label_open.Narration_K = (AudioClip[])GoldSunsetLabel.Narration_K.Clone();
                label_open.Narration_E = (AudioClip[])GoldSunsetLabel.Narration_E.Clone();
                label_open.Narration_C = (AudioClip[])GoldSunsetLabel.Narration_C.Clone();
                label_open.Narration_J = (AudioClip[])GoldSunsetLabel.Narration_J.Clone();

                gamemanager.minimap.Hotspot = GoldSunsetLabel.Label_Minimap;
                gamemanager.minimap.hotspot_img = (Sprite[])GoldSunsetLabel.MinimapLabel.Clone();
                ScreenCapture.MarkImg = GoldSunsetLabel.CaptureMark;

                label_open.Tip_K = GoldSunsetLabel.Tip_K;
                label_open.Tip_E = GoldSunsetLabel.Tip_E;
                label_open.Tip_C = GoldSunsetLabel.Tip_C;
                label_open.Tip_J = GoldSunsetLabel.Tip_J;

                WaitingVideo_path = (string[])GoldSunsetLabel.WaitingVideo_path.Clone();

                ModeActive = new bool[GoldSunsetLabel.ModeActive.Length];
                break;
            
        }

        gamemanager.GetComponent<ReadJson>().Readfile();

        label_open.Title_K = ReadJson.Title_K.ToArray();
        label_open.Title_E = ReadJson.Title_E.ToArray();
        label_open.Title_C = ReadJson.Title_C.ToArray();
        label_open.Title_J = ReadJson.Title_J.ToArray();

        label_open.DetailTexts_K = ReadJson.DetailText_K.ToArray();
        label_open.DetailTexts_E = ReadJson.DetailText_E.ToArray();
        label_open.DetailTexts_C = ReadJson.DetailText_C.ToArray();
        label_open.DetailTexts_J = ReadJson.DetailText_J.ToArray();

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            for (int index = 0; index < ModeActive.Length; index++)
            {
                ModeActive[index] = true;
            }
        }

        GameManager.currentLang = GameManager.Language_enum.Korea;
        Category.curcate = Category.CurrentCategory.Total;
        gamemanager.Tip_Obj.GetComponent<Image>().sprite = label_open.Tip_K;
        //label_open.SelectCategortButton(label_open.CategoryContent.transform.GetChild(0).gameObject);
    }
}
