using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LabelMake : MonoBehaviour
{
    public GameManager gamemanager;

    public List<Image> MapLabel_Icon = new List<Image>();
    public List<Text> Navilabel_Text = new List<Text>();
    public List<Image> Navilabel_Icon = new List<Image>();
    public List<Text> XR_MapLabel = new List<Text>();
    public List<Text> Clear_MapLabel = new List<Text>();

    public static int fontSize_Quick = 24;
    public static int fontSize_QuickLong = 22;
    public static int fontSize_Label = 22;
    public static int fontSize_LabelLong = 20;

    public static int LabelSize_x = 156;
    public static int LabelLongSize_x = 188;
    public static int LabelTSize_x = 108;
    public static int LabelLongTSize_x = 124;
    public static int LabelSize_y = 40;
    public static int LabelMidSize_y = 44;
    public static int LabelLongSize_y = 48;
    public static int ClearLabelSize_y = 100;
    public static int ClearLabelMidSize_y = 104;
    public static int ClearLabelLongSize_y = 108;
    public static int Icon_x = 0;
    public static int IconMid_x = 2;
    public static int IconLong_x = 4;

    public Font font_KE;
    public Font font_CJ;
    public Font Labelfont_KE;
    public Font Labelfont_CJ;
    
    // Start is called before the first frame update
    public void ReadytoStart()
    {
        if (Navilabel_Text.Count != gamemanager.label.LabelsParent.transform.childCount)
        {
            Navilabel_Text.Clear();
            Navilabel_Icon.Clear();
            for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
            {
                Navilabel_Text.Add(gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>());
                Navilabel_Icon.Add(gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>());
            }
        }

        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (XR_MapLabel.Count == 0 || XR_MapLabel[0] == null)
            {
                XR_MapLabel.Clear();
                MapLabel_Icon.Clear();
                for (int index = 0; index < gamemanager.xrmode.AllMapLabels.transform.childCount; index++)
                {
                    XR_MapLabel.Add(gamemanager.xrmode.AllMapLabels.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>());
                    MapLabel_Icon.Add(gamemanager.xrmode.AllMapLabels.transform.GetChild(index).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>());
                }
            }
        } else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            if (Clear_MapLabel.Count == 0 || Clear_MapLabel[0] == null)
            {
                Clear_MapLabel.Clear();
                MapLabel_Icon.Clear();
                for (int index = 0; index < gamemanager.clearmode.AllMapLabels.transform.childCount; index++)
                {
                    Clear_MapLabel.Add(gamemanager.clearmode.AllMapLabels.transform.GetChild(index).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>());
                    MapLabel_Icon.Add(gamemanager.clearmode.AllMapLabels.transform.GetChild(index).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>());
                }
            }
        }

        //MapLabel();
    }

    public void MapLabel()
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            for(int index = 0; index < gamemanager.xrmode.AllMapLabels.transform.childCount; index++)
            {
                for (int indexs = 0; indexs < gamemanager.label.Label_total.Count; indexs++)
                {
                    if (gamemanager.xrmode.AllMapLabels.transform.GetChild(index).gameObject.name == gamemanager.label.Label_total[indexs])
                    {
                        MapLabel_Icon[index].sprite = gamemanager.label.MapLabel[index];
                        switch (GameManager.currentLang)
                        {
                            case GameManager.Language_enum.Korea:
                                XR_MapLabel[index].font = Labelfont_KE;
                                XR_MapLabel[index].text = gamemanager.label.Title_K[indexs];
                                Debug.Log("today " + XR_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta + " / " + XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta);
                                Debug.Log(gamemanager.label.Title_K[indexs] + " / " + gamemanager.label.Title_K[indexs].Length);
                                XR_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 40);
                                XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);
                                if (gamemanager.label.Title_K[indexs].Length <= 5)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_Label;
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 40 - Icon_x);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                }
                                else if (gamemanager.label.Title_K[indexs].Length <= 10 && gamemanager.label.Title_K[indexs].Length > 5)
                                {
                                    if (gamemanager.label.Title_K[indexs].Length <= 7)
                                    {
                                        XR_MapLabel[index].fontSize = fontSize_LabelLong;
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 40 - Icon_x);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                        XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                    else
                                    {
                                        XR_MapLabel[index].fontSize = fontSize_LabelLong;
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 40 - IconMid_x);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                        XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelMidSize_y);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelMidSize_y);
                                    }
                                }
                                else if (gamemanager.label.Title_K[indexs].Length > 10)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong;
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 40 - IconLong_x);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 40);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelLongSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x, LabelLongSize_y);
                                }
                                XR_MapLabel[index].lineSpacing = 0.8f;
                                XR_MapLabel[index].fontStyle = FontStyle.Normal;
                                break;
                            case GameManager.Language_enum.English:
                                XR_MapLabel[index].font = Labelfont_KE;
                                XR_MapLabel[index].text = gamemanager.label.Title_E[indexs];
                                Debug.Log(gamemanager.label.Title_E[indexs] + " / " + gamemanager.label.Title_E[indexs].Length);

                                XR_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 40);
                                XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);

                                if (gamemanager.label.Title_E[indexs].Length <= 8)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_Label;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 40 - Icon_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                }
                                else if (gamemanager.label.Title_E[indexs].Length <= 16 && gamemanager.label.Title_E[indexs].Length > 8)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 40 - IconMid_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelMidSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelMidSize_y);
                                }
                                else if (gamemanager.label.Title_E[indexs].Length <= 22 && gamemanager.label.Title_E[indexs].Length > 16)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 40 - IconLong_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelLongSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + 10, LabelLongSize_y);
                                }
                                else if (gamemanager.label.Title_E[indexs].Length > 22)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 40 - IconLong_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + ((gamemanager.label.Title_E[indexs].Length+1) * 2), LabelLongSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + ((gamemanager.label.Title_E[indexs].Length+1) * 2), LabelLongSize_y);

                                    if (ContentsInfo.ContentsName == "Aegibong" && XR_MapLabel[index].gameObject.transform.parent.gameObject.name == "Daesungdong")
                                    {
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(193, LabelLongSize_y);
                                    }
                                }
                                XR_MapLabel[index].lineSpacing = 0.8f;
                                XR_MapLabel[index].fontStyle = FontStyle.Normal;
                                break;
                            case GameManager.Language_enum.Chinese:
                                XR_MapLabel[index].font = Labelfont_CJ;
                                XR_MapLabel[index].text = gamemanager.label.Title_C[indexs];

                                Debug.Log(gamemanager.label.Title_C[indexs] + " / " + gamemanager.label.Title_C[indexs].Length);

                                XR_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 40);
                                XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);

                                if (gamemanager.label.Title_C[indexs].Length <= 5)
                                {
                                    if (gamemanager.label.Title_C[indexs].Length < 5)
                                    {
                                        XR_MapLabel[index].fontSize = fontSize_Label;
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 40 - Icon_x);
                                        XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                    else
                                    {
                                        XR_MapLabel[index].fontSize = fontSize_Label - 2;
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 40 - Icon_x);
                                        XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                }
                                else if (gamemanager.label.Title_C[indexs].Length <= 9 && gamemanager.label.Title_C[indexs].Length > 5)
                                {
                                    if (gamemanager.label.Title_C[indexs].Length <= 7)
                                    {
                                        XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 40 - IconMid_x);
                                        XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelMidSize_y);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelMidSize_y);
                                    }
                                    else
                                    {
                                        XR_MapLabel[index].fontSize = fontSize_LabelLong-2;
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconMid_x, 40);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 40 - IconMid_x);
                                        XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelMidSize_y);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x, LabelMidSize_y);
                                    }
                                }
                                else if (gamemanager.label.Title_C[indexs].Length <= 16 && gamemanager.label.Title_C[indexs].Length > 9)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 40 - IconLong_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelLongSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + 10, LabelLongSize_y);
                                }
                                else if (gamemanager.label.Title_C[indexs].Length > 16)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 40 - IconLong_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + (gamemanager.label.Title_C[indexs].Length * 2), LabelLongSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + (gamemanager.label.Title_C[indexs].Length * 2), LabelLongSize_y);
                                }
                                XR_MapLabel[index].lineSpacing = 0.7f;
                                XR_MapLabel[index].fontStyle = FontStyle.Bold;
                                break;
                            case GameManager.Language_enum.Japanese:
                                XR_MapLabel[index].font = Labelfont_CJ;
                                XR_MapLabel[index].text = gamemanager.label.Title_J[indexs];

                                Debug.Log(gamemanager.label.Title_J[indexs] + " / " + gamemanager.label.Title_J[indexs].Length);

                                XR_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 40);
                                XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);

                                if (gamemanager.label.Title_J[indexs].Length <= 5)
                                {
                                    if (gamemanager.label.Title_J[indexs].Length < 5)
                                    {
                                        XR_MapLabel[index].fontSize = fontSize_Label;
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 40 - Icon_x);
                                        XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                    else
                                    {
                                        XR_MapLabel[index].fontSize = fontSize_Label - 2;
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 40 - Icon_x);
                                        XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                }
                                else if (gamemanager.label.Title_J[indexs].Length <= 8 && gamemanager.label.Title_J[indexs].Length > 5)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 40 - IconMid_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelMidSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelMidSize_y);
                                }
                                else if (gamemanager.label.Title_J[indexs].Length <= 12 && gamemanager.label.Title_J[indexs].Length > 8)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 40 - IconLong_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelLongSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + 10, LabelLongSize_y);
                                }
                                else if (gamemanager.label.Title_J[indexs].Length > 12)
                                {
                                    XR_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 40);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 40 - IconLong_x);
                                    XR_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + (gamemanager.label.Title_J[indexs].Length * 2), LabelLongSize_y);
                                    XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + (gamemanager.label.Title_J[indexs].Length * 2), LabelLongSize_y);

                                    if (ContentsInfo.ContentsName == "Aegibong" && XR_MapLabel[index].gameObject.transform.parent.gameObject.name == "Gijungdong")
                                    {
                                        XR_MapLabel[index].fontSize = 16;
                                    }
                                    else if (ContentsInfo.ContentsName == "Aegibong" && XR_MapLabel[index].gameObject.transform.parent.gameObject.name == "Daesungdong")
                                    {
                                        XR_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(164, LabelLongSize_y);
                                    }
                                }
                                XR_MapLabel[index].lineSpacing = 0.7f;
                                XR_MapLabel[index].fontStyle = FontStyle.Bold;
                                break;
                        }
                        XR_MapLabel[index].alignment = TextAnchor.MiddleCenter;
                    }
                }
            }
            if (ContentsInfo.ContentsName == "Aegibong")
            {
                //gamemanager.xrmode.gameObject.GetComponent<Aegibong_Eco>().SettingLabel();
            }
            else if(ContentsInfo.ContentsName == "Typhoon")
            {
                gamemanager.xrmode.AllMapLabels.transform.parent.gameObject.GetComponent<DisableLabel>().MapLabel();
            } else if(ContentsInfo.ContentsName == "EndIsland")
            {
                gamemanager.xrmode.AllMapLabels.transform.parent.gameObject.GetComponent<DisableLabel>().MapLabel();
            }
        } else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            for (int index = 0; index < gamemanager.clearmode.AllMapLabels.transform.childCount; index++)
            {
                for (int indexs = 0; indexs < gamemanager.label.Label_total.Count; indexs++)
                {
                    if (gamemanager.clearmode.AllMapLabels.transform.GetChild(index).gameObject.name == gamemanager.label.Label_total[indexs])
                    {
                        MapLabel_Icon[index].sprite = gamemanager.label.MapLabel[index];
                        switch (GameManager.currentLang)
                        {
                            case GameManager.Language_enum.Korea:
                                Clear_MapLabel[index].font = Labelfont_KE;
                                Clear_MapLabel[index].text = gamemanager.label.Title_K[indexs];

                                Clear_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 100);
                                Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);

                                if (gamemanager.label.Title_K[indexs].Length <= 5)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_Label;
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 100 - Icon_x);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                }
                                else if (gamemanager.label.Title_K[indexs].Length <= 10 && gamemanager.label.Title_K[indexs].Length > 5)
                                {
                                    if (gamemanager.label.Title_K[indexs].Length <= 7)
                                    {
                                        Clear_MapLabel[index].fontSize = fontSize_LabelLong;
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 100 - Icon_x);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                        Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                    else
                                    {
                                        Clear_MapLabel[index].fontSize = fontSize_LabelLong;
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 100 - IconMid_x);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                        Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelMidSize_y);
                                        Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelMidSize_y);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelMidSize_y);
                                    }
                                }
                                else if (gamemanager.label.Title_K[indexs].Length > 10)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong;
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 100 - IconLong_x);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 100);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, ClearLabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x, LabelLongSize_y);
                                }
                                Clear_MapLabel[index].fontStyle = FontStyle.Normal;
                                Clear_MapLabel[index].lineSpacing = 0.8f;
                                break;
                            case GameManager.Language_enum.English:
                                Clear_MapLabel[index].font = Labelfont_KE;
                                Clear_MapLabel[index].text = gamemanager.label.Title_E[indexs];
                                Debug.Log(gamemanager.label.Title_E[indexs] + " / " + gamemanager.label.Title_E[indexs].Length);

                                Clear_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 100);
                                Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);

                                if (gamemanager.label.Title_E[indexs].Length <= 10)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_Label - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 100 - Icon_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                }
                                else if (gamemanager.label.Title_E[indexs].Length <= 16 && gamemanager.label.Title_E[indexs].Length > 10)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 100 - IconMid_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelMidSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelMidSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelMidSize_y);
                                }
                                else if (gamemanager.label.Title_E[indexs].Length <= 22 && gamemanager.label.Title_E[indexs].Length > 16)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 100 - IconLong_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, ClearLabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + 10, LabelLongSize_y);
                                }
                                else if (gamemanager.label.Title_E[indexs].Length > 22)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 100 - IconLong_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + ((gamemanager.label.Title_E[indexs].Length+1) * 2), ClearLabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + ((gamemanager.label.Title_E[indexs].Length+1) * 2), LabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + ((gamemanager.label.Title_E[indexs].Length+1) * 2), LabelLongSize_y);

                                    if(ContentsInfo.ContentsName == "Aegibong" && Clear_MapLabel[index].gameObject.transform.parent.gameObject.name == "Daesungdong")
                                    {
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(193, LabelLongSize_y);
                                    }
                                }
                                Clear_MapLabel[index].fontStyle = FontStyle.Normal;
                                Clear_MapLabel[index].lineSpacing = 0.8f;
                                break;
                            case GameManager.Language_enum.Chinese:
                                Clear_MapLabel[index].font = Labelfont_CJ;
                                Clear_MapLabel[index].text = gamemanager.label.Title_C[indexs];

                                Debug.Log(gamemanager.label.Title_C[indexs] + " / " + gamemanager.label.Title_C[indexs].Length);

                                Clear_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 100);
                                Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);

                                if (gamemanager.label.Title_C[indexs].Length <= 5)
                                {
                                    if (gamemanager.label.Title_C[indexs].Length < 5)
                                    {
                                        Clear_MapLabel[index].fontSize = fontSize_Label;
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 100 - Icon_x);
                                        Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                    else
                                    {
                                        Clear_MapLabel[index].fontSize = fontSize_Label-2;
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 100 - Icon_x);
                                        Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                }
                                else if (gamemanager.label.Title_C[indexs].Length <= 9 && gamemanager.label.Title_C[indexs].Length > 5)
                                {
                                    if (gamemanager.label.Title_C[indexs].Length <= 7)
                                    {
                                        Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 100 - IconMid_x);
                                        Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelMidSize_y);
                                        Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelMidSize_y);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelMidSize_y);
                                    }
                                    else
                                    {
                                        Clear_MapLabel[index].fontSize = fontSize_LabelLong;
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconMid_x, 100);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 100 - IconMid_x);
                                        Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, ClearLabelMidSize_y);
                                        Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelMidSize_y);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x, LabelMidSize_y);

                                        if (ContentsInfo.ContentsName == "Aegibong" && (Clear_MapLabel[index].gameObject.transform.parent.gameObject.name == "Daesungdong" || Clear_MapLabel[index].gameObject.transform.parent.gameObject.name == "Gijungdong"))
                                        {
                                            Clear_MapLabel[index].fontSize = fontSize_LabelLong-2;
                                        }
                                    }
                                }
                                else if (gamemanager.label.Title_C[indexs].Length <= 16 && gamemanager.label.Title_C[indexs].Length > 9)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 100 - IconLong_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, ClearLabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + 10, LabelLongSize_y);
                                }
                                else if (gamemanager.label.Title_C[indexs].Length > 16)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 100 - IconLong_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + (gamemanager.label.Title_C[indexs].Length * 2), ClearLabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + (gamemanager.label.Title_C[indexs].Length * 2), LabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + (gamemanager.label.Title_C[indexs].Length * 2), LabelLongSize_y);
                                }
                                Clear_MapLabel[index].fontStyle = FontStyle.Bold;
                                Clear_MapLabel[index].lineSpacing = 0.7f;
                                break;
                            case GameManager.Language_enum.Japanese:
                                Clear_MapLabel[index].font = Labelfont_CJ;
                                Clear_MapLabel[index].text = gamemanager.label.Title_J[indexs];

                                Debug.Log(gamemanager.label.Title_J[indexs] + " / " + gamemanager.label.Title_J[indexs].Length);

                                Clear_MapLabel[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 100);
                                Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);

                                if (gamemanager.label.Title_J[indexs].Length <= 5)
                                {
                                    if (gamemanager.label.Title_J[indexs].Length < 5)
                                    {
                                        Clear_MapLabel[index].fontSize = fontSize_Label;
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 100 - Icon_x);
                                        Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                    else
                                    {
                                        Clear_MapLabel[index].fontSize = fontSize_Label - 2;
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                        MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + Icon_x, 100 - Icon_x);
                                        Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelSize_y);
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelSize_y);
                                    }
                                }
                                else if (gamemanager.label.Title_J[indexs].Length <= 8 && gamemanager.label.Title_J[indexs].Length > 5)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + Icon_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconMid_x, 100 - IconMid_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, ClearLabelMidSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelSize_x, LabelMidSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelTSize_x, LabelMidSize_y);
                                }
                                else if (gamemanager.label.Title_J[indexs].Length <= 12 && gamemanager.label.Title_J[indexs].Length > 8)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 100 - IconLong_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, ClearLabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x, LabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + 10, LabelLongSize_y);
                                }
                                else if (gamemanager.label.Title_J[indexs].Length > 12)
                                {
                                    Clear_MapLabel[index].fontSize = fontSize_LabelLong - 2;
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36 + IconLong_x, 100);
                                    MapLabel_Icon[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-78 + IconLong_x, 100 - IconLong_x);
                                    Clear_MapLabel[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + (gamemanager.label.Title_J[indexs].Length * 2), ClearLabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.parent.GetChild(0).gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongSize_x + (gamemanager.label.Title_J[indexs].Length * 2), LabelLongSize_y);
                                    Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelLongTSize_x + (gamemanager.label.Title_J[indexs].Length * 2), LabelLongSize_y);
                                    if (ContentsInfo.ContentsName == "Aegibong" && Clear_MapLabel[index].gameObject.transform.parent.gameObject.name == "Gijungdong")
                                    {
                                        Clear_MapLabel[index].fontSize = 16;
                                    } else if (ContentsInfo.ContentsName == "Aegibong" && Clear_MapLabel[index].gameObject.transform.parent.gameObject.name == "Daesungdong"){
                                        Clear_MapLabel[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(164, LabelLongSize_y);
                                    }
                                }
                                Clear_MapLabel[index].fontStyle = FontStyle.Bold;
                                Clear_MapLabel[index].lineSpacing = 0.7f;
                                break;
                        }
                        Clear_MapLabel[index].alignment = TextAnchor.MiddleCenter;
                    }
                }
            }
            if (ContentsInfo.ContentsName == "Aegibong")
            {
                //gamemanager.clearmode.gameObject.GetComponent<Aegibong_Eco>().SettingLabel();
            }
        }
    }

    public void NavigationText()
    {
        for (int index = 0; index < Navilabel_Text.Count; index++)
        {
            for (int indexs = 0; indexs < gamemanager.label.Label_total.Count; indexs++)
            {
                if (Navilabel_Text[index].gameObject.transform.parent.gameObject.name == gamemanager.label.Label_total[indexs])
                {
                    Navilabel_Icon[index].sprite = gamemanager.label.NaviLabel[indexs];
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Navilabel_Text[index].text = gamemanager.label.Title_K[indexs];
                            Navilabel_Text[index].font = font_KE;
                            Navilabel_Text[index].lineSpacing = 1;
                            if (gamemanager.label.Title_K[indexs].Length <= 18)
                            {
                                Navilabel_Text[index].fontSize = fontSize_Quick;
                            }
                            else if (gamemanager.label.Title_K[indexs].Length > 18)
                            {
                                Navilabel_Text[index].fontSize = fontSize_LabelLong;
                            }
                            break;
                        case GameManager.Language_enum.English:
                            Navilabel_Text[index].text = gamemanager.label.Title_E[indexs];
                            Navilabel_Text[index].font = font_KE;
                            Navilabel_Text[index].lineSpacing = 1;
                            if (gamemanager.label.Title_E[indexs].Length <= 18)
                            {
                                Navilabel_Text[index].fontSize = fontSize_Quick;
                            }
                            else if (gamemanager.label.Title_E[indexs].Length > 18)
                            {
                                Navilabel_Text[index].fontSize = fontSize_Quick;
                            }
                            break;
                        case GameManager.Language_enum.Chinese:
                            Navilabel_Text[index].text = gamemanager.label.Title_C[indexs];
                            Navilabel_Text[index].font = font_CJ;
                            Navilabel_Text[index].lineSpacing = 1;
                            if (gamemanager.label.Title_C[indexs].Length <= 20)
                            {
                                Navilabel_Text[index].fontSize = fontSize_Quick;
                            }
                            else if (gamemanager.label.Title_C[indexs].Length > 20)
                            {
                                Navilabel_Text[index].fontSize = fontSize_Quick;
                            }
                            break;
                        case GameManager.Language_enum.Japanese:
                            Navilabel_Text[index].text = gamemanager.label.Title_J[indexs];
                            Navilabel_Text[index].font = font_CJ;

                            Navilabel_Text[index].lineSpacing = 0.7f;
                            if (gamemanager.label.Title_J[indexs].Length <= 20)
                            {
                                Navilabel_Text[index].fontSize = fontSize_Quick;
                            }
                            else if (gamemanager.label.Title_J[indexs].Length > 20)
                            {
                                Navilabel_Text[index].fontSize = fontSize_QuickLong;
                            }
                            break;
                    }
                }
            }
        }
    }
}
