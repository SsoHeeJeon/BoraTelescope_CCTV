using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WoosukLabel : MonoBehaviour
{
    public static List<string> Label_total;
    public static List<string> Label_Cate_1;
    public static List<string> Label_Cate_2;

    public static List<string> Label_Minimap;

    public static Sprite[] NaviLabel;
    public static Sprite[] MapLabel;

    public static Sprite[] DetailImage;
    public static Sprite[] MinimapLabel;
    public static Sprite CaptureMark;
    public static Sprite Tip_K;
    public static Sprite Tip_E;
    public static Sprite Tip_C;
    public static Sprite Tip_J;

    public static AudioClip[] Narration_K;
    public static AudioClip[] Narration_E;
    public static AudioClip[] Narration_C;
    public static AudioClip[] Narration_J;

    public static string[] WaitingVideo_path;

    public static bool[] ModeActive;

    public static void LoadLabelInfo()
    {
        Label_total = new List<string> { "BeanGoose", "Gari", "Swan", "WCrane", "Spoonbill",
                                         "Goshawk", "Eagle", "MallardDuck"};
        Label_Cate_1 = new List<string> {};
        Label_Cate_2 = new List<string> {"BeanGoose", "Gari", "Swan", "WCrane", "Spoonbill",
                                         "Goshawk", "Eagle", "MallardDuck"};

        Label_Minimap = new List<string> { "UIsland", "SongakM", "Odusan" };

        DisableLabel.HiddenLabelList = new List<string> { "River", "DogogaeM", "Old", "AmsilV", "Quarry", "New", "PromotionV", "Dora", "Gwansanpo", "Dolgoji", "HanR", "Paju" };
        DisableLabel.HiddenLabelPosition = new Vector3[12];

        TourismMode.TourList = new List<string> { "Street", "Noodle", "Platform", "GupoMarket", "CreateCenter",
                                                  "GupoCathedral", "EduPark", "ArtCenter", "Castle", "Young",
                                                  "ManduckT", "SuckbulT", "DuckTown", "RegoTown", "HwamyeoungPark", 
                                                  "Weather", "Sea", "Daechunchun", "ForestPark", "GaramStreet"};

        NaviLabel = new Sprite[Resources.LoadAll<Sprite>("Woosuk/Sprite/NavigationLabel").Length];
        MapLabel = new Sprite[Resources.LoadAll<Sprite>("Woosuk/Sprite/MapLabel").Length];

        DetailImage = new Sprite[Resources.LoadAll<Sprite>("Woosuk/Sprite/DetailImage").Length];
        MinimapLabel = new Sprite[Resources.LoadAll<Sprite>("Woosuk/Sprite/Hotspot").Length];

        Narration_K = new AudioClip[Resources.LoadAll<AudioClip>("Woosuk/Narration/Korea").Length];
        Narration_E = new AudioClip[Resources.LoadAll<AudioClip>("Woosuk/Narration/English").Length];
        Narration_C = new AudioClip[Resources.LoadAll<AudioClip>("Woosuk/Narration/Chinese").Length];
        Narration_J = new AudioClip[Resources.LoadAll<AudioClip>("Woosuk/Narration/Japanese").Length];

        NaviLabel = Resources.LoadAll<Sprite>("Woosuk/Sprite/NavigationLabel");
        MapLabel = Resources.LoadAll<Sprite>("Woosuk/Sprite/MapLabel");

        DetailImage = Resources.LoadAll<Sprite>("Woosuk/Sprite/DetailImage");
        MinimapLabel = Resources.LoadAll<Sprite>("Woosuk/Sprite/Hotspot");

        CaptureMark = Resources.Load<Sprite>("Woosuk/Sprite/CaptureMark");

        Tip_K = Resources.Load<Sprite>("Woosuk/Sprite/Tip_K");
        Tip_E = Resources.Load<Sprite>("Woosuk/Sprite/Tip_E");
        Tip_C = Resources.Load<Sprite>("Woosuk/Sprite/Tip_C");
        Tip_J = Resources.Load<Sprite>("Woosuk/Sprite/Tip_J");

        Narration_K = Resources.LoadAll<AudioClip>("Woosuk/Narration/Korea");
        Narration_E = Resources.LoadAll<AudioClip>("Woosuk/Narration/English");
        Narration_C = Resources.LoadAll<AudioClip>("Woosuk/Narration/Chinese");
        Narration_J = Resources.LoadAll<AudioClip>("Woosuk/Narration/Japanese");

        WaitingVideo_path = Directory.GetFiles(Application.dataPath + "/Resources/Video", "*.mp4");

        SettingManager.Password_Setting = "1215";

        GameManager.MainMode = "LiveMode";
        ClearMode.StartPosition = new Vector3(-1705, -2744, -1631);
        ClearMode.StartZoom = 200;
        ClearMode.MaxZoomIn = 1851;
        ClearMode.LabelMaxZoomIn = 1200;//970
        ClearMode.MaxZoomOut = -100;
        ClearMode.MinLabelSize = 1.0f;
        ClearMode.MaxLabelSize = 3;//5.5f;
        SunAPITest.CCTVControl.SwitchiingCCTV = true;

        ModeActive = new bool[3];
    }
}
