using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourismMode : LabelName
{
    public GameManager gamemanager;

    public static List<string> TourList;
    public static GameObject[] TourLabel_list;
    public List<Text> TourLabel_Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UISetting()
    {
        switch (ContentsInfo.ContentsName)
        {
            case "EndIsland":
                //clearmode = GameObject.FindGameObjectWithTag("ClearMode").GetComponent<ClearMode>();
                //gamemanager.labeldetail = clearmode.GetComponent<LabelDetail>();
                gamemanager.UI_All.gameObject.SetActive(true);
                for (int index = 0; index < gamemanager.UI_All.transform.childCount; index++)
                {
                    gamemanager.UI_All.transform.GetChild(index).gameObject.SetActive(false);
                }
                gamemanager.MenuBar.SetActive(true);
                if (GameManager.AnyError == true)
                {
                    gamemanager.MenuBar.gameObject.GetComponent<Image>().enabled = true;
                    for (int index = 0; index < gamemanager.MenuBar.gameObject.transform.childCount; index++)
                    {
                        gamemanager.MenuBar.transform.GetChild(index).gameObject.SetActive(true);
                    }
                }
                gamemanager.Arrow.SetActive(false);
                gamemanager.NavigationBar.gameObject.SetActive(false);
                gamemanager.LanguageBar.gameObject.SetActive(true);
                gamemanager.NavigationBar.transform.GetChild(0).gameObject.SetActive(true);
                gamemanager.MiniMap_Background.transform.parent.gameObject.SetActive(false);
                gamemanager.MiniMap_Background.gameObject.SetActive(false);
                gamemanager.ZoomObj.gameObject.SetActive(false);
                gamemanager.ErrorMessage.transform.parent.gameObject.SetActive(true);

                for (int index = 0; index < gamemanager.MenuBar.transform.GetChild(0).transform.childCount; index++)
                {
                    if (gamemanager.MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                    {
                        gamemanager.MenuBar.transform.GetChild(0).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                for (int index = 0; index < gamemanager.MenuBar.transform.GetChild(1).transform.childCount; index++)
                {
                    if (gamemanager.MenuBar.transform.GetChild(1).GetChild(index).gameObject.activeSelf)
                    {
                        if (gamemanager.MenuBar.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.childCount != 0)
                        {
                            gamemanager.MenuBar.transform.GetChild(1).gameObject.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                }
                gamemanager.MenuBar.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //BackGround.transform.parent.gameObject.SetActive(true);
                gamemanager.allbar.NaviRect.sizeDelta = new Vector2(AllBarOnOff.barOpen, 1080);
                //Tip_Obj.SetActive(true);
                //label.SelectCategortButton(CategoryContent.transform.GetChild(0).gameObject);
                //minimap.SettingHotspot();
                //gamemanager.TipOpen();
                //Invoke("SeeNavibar", 0.3f);

                gamemanager.TourismModeBtn.transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
    }

    
    public void MapLabel()
    {
        if (gamemanager == null)
        {
            gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        TourLabel_Text.Clear();
        for (int index = 0; index < TourLabel_list.Length; index++)
        {
            if (TourLabel_list[index].activeSelf)
            {
                TourLabel_Text.Add(TourLabel_list[index].transform.GetChild(1).gameObject.GetComponent<Text>());
            }
        }

        for (int index = 0; index < TourLabel_Text.Count; index++)
        {
            for (int indexs = 0; indexs < TourList.Count; indexs++)
            {
                if (TourLabel_Text[index].gameObject.transform.parent.gameObject.name == TourList[indexs])
                {
                    TourLabel_Text[index].gameObject.transform.parent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(156, 40);
                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 40);
                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-36, 40);

                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            //TourLabelLanguage(TourList[indexs]);
                            TourLabel_Text[index].font = gamemanager.labelmake.Labelfont_KE;
                            TourLabel_Text[index].text = gamemanager.label.Title_K[index + gamemanager.label.Label_total.Count];

                            if (TourLabel_Text[index].text.Length <= 5)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_Label;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelSize_y);
                            }
                            else if (TourLabel_Text[index].text.Length <= 10 && TourLabel_Text[index].text.Length > 5)
                            {
                                if (TourList[indexs] == "PromotionV")
                                {
                                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-26, 40);
                                    TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong;
                                    TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(201, LabelMake.LabelSize_y);
                                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelSize_y);
                                }
                                else
                                {
                                    if (TourLabel_Text[index].text.Length <= 7)
                                    {
                                        TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong;
                                        TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelSize_y);
                                        TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelSize_y);
                                    }
                                    else
                                    {
                                        TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong;
                                        TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelMidSize_y);
                                        TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelMidSize_y);
                                    }
                                }
                            }
                            else if (TourLabel_Text[index].text.Length > 10)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongSize_x, LabelMake.LabelLongSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongTSize_x, LabelMake.LabelLongSize_y);
                            }
                            TourLabel_Text[index].lineSpacing = 0.8f;
                            TourLabel_Text[index].fontStyle = FontStyle.Normal;
                            break;
                        case GameManager.Language_enum.English:
                            //TourLabelLanguage(TourList[indexs]);
                            TourLabel_Text[index].font = gamemanager.labelmake.Labelfont_KE;
                            TourLabel_Text[index].text = gamemanager.label.Title_E[index + gamemanager.label.Label_total.Count];
                            if (TourList[indexs] == "PromotionV")
                            {
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector2(-26, 40);
                            }
                            if (TourLabel_Text[index].text.Length <= 8)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_Label;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelSize_y);
                            }
                            else if (TourLabel_Text[index].text.Length <= 16 && TourLabel_Text[index].text.Length > 8)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelMidSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelMidSize_y);
                            }
                            else if (TourLabel_Text[index].text.Length <= 22 && TourLabel_Text[index].text.Length > 16)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongSize_x, LabelMake.LabelLongSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongTSize_x + 10, LabelMake.LabelLongSize_y);
                            }
                            else if (TourLabel_Text[index].text.Length > 22)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongSize_x + (TourLabel_Text[index].text.Length * 2), LabelMake.LabelLongSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongTSize_x + (TourLabel_Text[index].text.Length * 2), LabelMake.LabelLongSize_y);
                            }
                            TourLabel_Text[index].lineSpacing = 0.8f;
                            TourLabel_Text[index].fontStyle = FontStyle.Normal;
                            break;
                        case GameManager.Language_enum.Chinese:
                            //TourLabelLanguage(TourList[indexs]);
                            TourLabel_Text[index].font = gamemanager.labelmake.Labelfont_CJ;
                            TourLabel_Text[index].text = gamemanager.label.Title_C[index + gamemanager.label.Label_total.Count];

                            if (TourLabel_Text[index].text.Length <= 5)
                            {
                                if (TourLabel_Text[index].text.Length < 5)
                                {
                                    TourLabel_Text[index].fontSize = LabelMake.fontSize_Label;
                                    TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelSize_y);
                                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelSize_y);
                                }
                                else
                                {
                                    TourLabel_Text[index].fontSize = LabelMake.fontSize_Label - 2;
                                    TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelSize_y);
                                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelSize_y);
                                }
                            }
                            else if (TourLabel_Text[index].text.Length <= 9 && TourLabel_Text[index].text.Length > 5)
                            {
                                if (TourLabel_Text[index].text.Length <= 7)
                                {
                                    TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                    TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelMidSize_y);
                                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelMidSize_y);
                                }
                                else
                                {
                                    TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                    TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongSize_x, LabelMake.LabelMidSize_y);
                                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongTSize_x, LabelMake.LabelMidSize_y);
                                }
                            }
                            else if (TourLabel_Text[index].text.Length <= 16 && TourLabel_Text[index].text.Length > 9)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongSize_x, LabelMake.LabelLongSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongTSize_x + 10, LabelMake.LabelLongSize_y);
                            }
                            else if (TourLabel_Text[index].text.Length > 16)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongSize_x + (TourLabel_Text[index].text.Length * 2), LabelMake.LabelLongSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongTSize_x + (TourLabel_Text[index].text.Length * 2), LabelMake.LabelLongSize_y);
                            }
                            TourLabel_Text[index].lineSpacing = 0.7f;
                            TourLabel_Text[index].fontStyle = FontStyle.Bold;
                            break;
                        case GameManager.Language_enum.Japanese:
                            //TourLabelLanguage(TourList[indexs]);
                            TourLabel_Text[index].font = gamemanager.labelmake.Labelfont_CJ;
                            TourLabel_Text[index].text = gamemanager.label.Title_J[index + gamemanager.label.Label_total.Count];

                            if (TourLabel_Text[index].text.Length <= 5)
                            {
                                if (TourLabel_Text[index].text.Length < 5)
                                {
                                    TourLabel_Text[index].fontSize = LabelMake.fontSize_Label;
                                    TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelSize_y);
                                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelSize_y);
                                }
                                else
                                {
                                    TourLabel_Text[index].fontSize = LabelMake.fontSize_Label - 2;
                                    TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelSize_y);
                                    TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelSize_y);
                                }
                            }
                            else if (TourLabel_Text[index].text.Length <= 8 && TourLabel_Text[index].text.Length > 5)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelSize_x, LabelMake.LabelMidSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelTSize_x, LabelMake.LabelMidSize_y);
                            }
                            else if (TourLabel_Text[index].text.Length <= 12 && TourLabel_Text[index].text.Length > 8)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongSize_x, LabelMake.LabelLongSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongTSize_x + 10, LabelMake.LabelLongSize_y);
                            }
                            else if (TourLabel_Text[index].text.Length > 12)
                            {
                                TourLabel_Text[index].fontSize = LabelMake.fontSize_LabelLong - 2;
                                TourLabel_Text[index].gameObject.transform.parent.gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongSize_x + (TourLabel_Text[index].text.Length * 2), LabelMake.LabelLongSize_y);
                                TourLabel_Text[index].gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(LabelMake.LabelLongTSize_x + (TourLabel_Text[index].text.Length * 2), LabelMake.LabelLongSize_y);
                            }
                            TourLabel_Text[index].lineSpacing = 0.7f;
                            TourLabel_Text[index].fontStyle = FontStyle.Bold;
                            break;
                    }
                    TourLabel_Text[index].alignment = TextAnchor.MiddleCenter;
                }
            }
        }
    }
}
