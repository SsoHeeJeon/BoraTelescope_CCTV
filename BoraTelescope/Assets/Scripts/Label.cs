using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Label : Category
{
    public GameObject Label_origin;
    public GameObject LabelsParent;

    GameObject SetObject;

    public List<string> Label_total;
    public List<string> Label_Cate_1;
    public List<string> Label_Cate_2;
    public List<string> Label_navi = new List<string>();

    public Sprite[] NaviLabel;
    public Sprite[] MapLabel;
    public Font Titlefont_KE;
    public Font Titlefont_CJ;
    public Font Detailfont_KE;
    public Font Detailfont_CJ;

    public Sprite[] DetailImage;
    public Sprite Tip_K;
    public Sprite Tip_E;
    public Sprite Tip_C;
    public Sprite Tip_J;

    public string[] Title_K;
    public string[] Title_E;
    public string[] Title_C;
    public string[] Title_J;

    public string[] DetailTexts_K;
    public string[] DetailTexts_E;
    public string[] DetailTexts_C;
    public string[] DetailTexts_J;

    public Vector3[] Label_Position;
    public AudioClip[] Narration_K;
    public AudioClip[] Narration_E;
    public AudioClip[] Narration_C;
    public AudioClip[] Narration_J;

    public AudioSource Narration;
    public Sprite Narr_On;
    public Sprite Narr_Off;
    public bool PlayNarr = false;
    public Vector3 StartPosition;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        Label_navi.Clear();
        for (int index = 0; index < Label_total.Count; index++)
        {
            Label_navi.Add(Label_total[index]);
        }

        //Label_Position[5] = new Vector3(9595, 50,0);

        Narration.clip = null;
        PlayNarr = false;
        Narration.Stop();
    }

    /// <summary>
    /// 상세설명이 켜져있는 상태에서 언어변경
    /// 언어별 네비게이션 라벨 버튼 언어 변경
    /// </summary>
    public void SelectLanguageButton()
    {
        switch (GameManager.currentLang)
        {
            case GameManager.Language_enum.Korea:
                //for (int index = 0; index < LabelsParent.transform.childCount; index++)
                //{
                //    for (int sindex = 0; sindex < NaviLabel_K.Length; sindex++)
                //    {
                //        if (LabelsParent.transform.GetChild(index).gameObject.name == Label_total[sindex])
                //        {
                //            //LabelsParent.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = NaviLabel_K[sindex];
                //            LabelsParent.transform.GetChild(index).gameObject.GetComponent<NaviLabel>().labelname.text = Title_K[sindex];
                //        }
                //    }
                //}

                CategorySelect_korea();
                break;
            case GameManager.Language_enum.English:
                //for (int index = 0; index < LabelsParent.transform.childCount; index++)
                //{
                //    for (int sindex = 0; sindex < NaviLabel_K.Length; sindex++)
                //    {
                //        if (LabelsParent.transform.GetChild(index).gameObject.name == Label_total[sindex])
                //        {
                //            //LabelsParent.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = NaviLabel_E[sindex];
                //            LabelsParent.transform.GetChild(index).gameObject.GetComponent<NaviLabel>().labelname.text = Title_E[sindex];
                //        }
                //    }
                //}

                CategorySelect_English();
                break;
            case GameManager.Language_enum.Chinese:
                //for (int index = 0; index < LabelsParent.transform.childCount; index++)
                //{
                //    for (int sindex = 0; sindex < NaviLabel_C.Length; sindex++)
                //    {
                //        if (LabelsParent.transform.GetChild(index).gameObject.name == Label_total[sindex])
                //        {
                //            //LabelsParent.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = NaviLabel_C[sindex];
                //            LabelsParent.transform.GetChild(index).gameObject.GetComponent<NaviLabel>().labelname.text = Title_C[sindex];
                //        }
                //    }
                //}

                CategorySelect_Chinese();
                break;
            case GameManager.Language_enum.Japanese:
                //for (int index = 0; index < LabelsParent.transform.childCount; index++)
                //{
                //    for (int sindex = 0; sindex < NaviLabel_J.Length; sindex++)
                //    {
                //        if (LabelsParent.transform.GetChild(index).gameObject.name == Label_total[sindex])
                //        {
                //            //LabelsParent.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = NaviLabel_J[sindex];
                //            LabelsParent.transform.GetChild(index).gameObject.GetComponent<NaviLabel>().labelname.text = Title_J[sindex];
                //        }
                //    }
                //}

                CategorySelect_Japanese();
                break;
        }
        gamemanager.labelmake.NavigationText();
        SetCategory();

        //gamemanager.labeldetail.ChangeDetailLanguage();
        if(!SceneManager.GetActiveScene().name.Contains("VisitMode"))
        {
            if (gamemanager.labeldetail.Detail_Background.transform.localPosition.x != LabelDetail.Detail_Close_x)
            {
                if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    SetObject = gamemanager.xrmode.SelectLabel;
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    SetObject = gamemanager.clearmode.clicknaviobj;
                }

                if (SceneManager.GetActiveScene().name.Contains("XRMode") || SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    for (int index = 0; index < Label_total.Count; index++)
                    {
                        if (SetObject != null)
                        {
                            if (Label_total[index] == SetObject.name)
                            {
                                gamemanager.labeldetail.Detail_LabelImage.sprite = DetailImage[index];
                                switch (GameManager.currentLang)
                                {
                                    case GameManager.Language_enum.Korea:
                                        gamemanager.labeldetail.TitleDetail.text = Title_K[index];
                                        gamemanager.labeldetail.TitleDetail.font = Detailfont_KE;
                                        gamemanager.labeldetail.TitleDetail.fontSize = 30;
                                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                                        gamemanager.labeldetail.SubTitleDetail.font = Detailfont_KE;
                                        if (Title_E[index].Length < 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                                        }
                                        else if (Title_E[index].Length >= 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                                        }
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_K[index];
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_KE;
                                        Narration.clip = Narration_K[index];
                                        break;
                                    case GameManager.Language_enum.English:
                                        gamemanager.labeldetail.TitleDetail.text = Title_E[index];
                                        gamemanager.labeldetail.TitleDetail.font = Detailfont_KE;
                                        if (Title_E[index].Length < 25)
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 30;
                                        }
                                        else if (Title_E[index].Length >= 25)
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 26;
                                        }

                                        if (ContentsInfo.ContentsName == "Aegibong" && Title_E[index].Contains("Gijeongdong"))
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 24;
                                        }

                                        gamemanager.labeldetail.SubTitleDetail.text = Title_K[index];
                                        gamemanager.labeldetail.SubTitleDetail.font = Detailfont_KE;
                                        gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_E[index];
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_KE;
                                        Narration.clip = Narration_E[index];
                                        break;
                                    case GameManager.Language_enum.Chinese:
                                        gamemanager.labeldetail.TitleDetail.text = Title_C[index];
                                        gamemanager.labeldetail.TitleDetail.font = Detailfont_CJ;
                                        gamemanager.labeldetail.TitleDetail.fontSize = 30;
                                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                                        gamemanager.labeldetail.SubTitleDetail.font = Detailfont_KE;
                                        if (Title_E[index].Length < 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                                        }
                                        else if (Title_E[index].Length >= 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                                        }
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_C[index];
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_CJ;
                                        Narration.clip = Narration_C[index];
                                        break;
                                    case GameManager.Language_enum.Japanese:
                                        gamemanager.labeldetail.TitleDetail.text = Title_J[index];
                                        gamemanager.labeldetail.TitleDetail.font = Detailfont_CJ;
                                        if (Title_J[index].Length < 15)
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 30;
                                        }
                                        else if (Title_J[index].Length >= 15)
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 22;
                                        }
                                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                                        gamemanager.labeldetail.SubTitleDetail.font = Detailfont_KE;
                                        if (Title_E[index].Length < 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                                        }
                                        else if (Title_E[index].Length >= 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                                        }
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_J[index];
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_CJ;
                                        Narration.clip = Narration_J[index];
                                        break;
                                }
                            }
                        }
                    }
                } else if (SceneManager.GetActiveScene().name.Contains("Tourism"))
                {
                    for (int index = Label_total.Count; index < Title_K.Length; index++)
                    {
                        if (SetObject != null)
                        {
                            if (Title_K[index] == gamemanager.tourLite.clicknaviobj)
                            {
                                gamemanager.labeldetail.Detail_LabelImage.sprite = DetailImage[index];
                                switch (GameManager.currentLang)
                                {
                                    case GameManager.Language_enum.Korea:
                                        gamemanager.labeldetail.TitleDetail.text = Title_K[index];
                                        gamemanager.labeldetail.TitleDetail.font = Detailfont_KE;
                                        gamemanager.labeldetail.TitleDetail.fontSize = 30;
                                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                                        gamemanager.labeldetail.SubTitleDetail.font = Detailfont_KE;
                                        if (Title_E[index].Length < 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                                        }
                                        else if (Title_E[index].Length >= 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                                        }
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_K[index];
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_KE;
                                        Narration.clip = Narration_K[index];
                                        break;
                                    case GameManager.Language_enum.English:
                                        gamemanager.labeldetail.TitleDetail.text = Title_E[index];
                                        gamemanager.labeldetail.TitleDetail.font = Detailfont_KE;
                                        if (Title_E[index].Length < 25)
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 30;
                                        }
                                        else if (Title_E[index].Length >= 25)
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 26;
                                        }

                                        gamemanager.labeldetail.SubTitleDetail.text = Title_K[index];
                                        gamemanager.labeldetail.SubTitleDetail.font = Detailfont_KE;
                                        gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_E[index];
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_KE;
                                        Narration.clip = Narration_E[index];
                                        break;
                                    case GameManager.Language_enum.Chinese:
                                        gamemanager.labeldetail.TitleDetail.text = Title_C[index];
                                        gamemanager.labeldetail.TitleDetail.font = Detailfont_CJ;
                                        gamemanager.labeldetail.TitleDetail.fontSize = 30;
                                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                                        gamemanager.labeldetail.SubTitleDetail.font = Detailfont_KE;
                                        if (Title_E[index].Length < 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                                        }
                                        else if (Title_E[index].Length >= 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                                        }
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_C[index];
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_CJ;
                                        Narration.clip = Narration_C[index];
                                        break;
                                    case GameManager.Language_enum.Japanese:
                                        gamemanager.labeldetail.TitleDetail.text = Title_J[index];
                                        gamemanager.labeldetail.TitleDetail.font = Detailfont_CJ;
                                        if (Title_J[index].Length < 15)
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 30;
                                        }
                                        else if (Title_J[index].Length >= 15)
                                        {
                                            gamemanager.labeldetail.TitleDetail.fontSize = 22;
                                        }
                                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                                        gamemanager.labeldetail.SubTitleDetail.font = Detailfont_KE;
                                        if (Title_E[index].Length < 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                                        }
                                        else if (Title_E[index].Length >= 25)
                                        {
                                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                                        }
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_J[index];
                                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_CJ;
                                        Narration.clip = Narration_J[index];
                                        break;
                                }
                                gamemanager.labeldetail.Detail_ScrollView.GetComponent<ScrollRect>().content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, gamemanager.labeldetail.SubTitleDetail.GetComponent<RectTransform>().sizeDelta.y); 
                            }
                        }
                    }
                }
                gamemanager.labeldetail.ChangeDetailLanguage();
            }
            //else if (gamemanager.labeldetail.Detail_Background.transform.localPosition.x == LabelDetail.Detail_Close_x)
            //{
            //    Invoke("SelectLanguageButton", 0.05f);
            //}

            //gamemanager.labeldetail.ChangeDetailLanguage();
        }
    }

    /// <summary>
    /// 라벨 선택하면 상세설명창, 설명 셋팅
    /// </summary>
    /// <param name="Label"></param>
    public void SelectLabel(string Label)
    {
        for (int index = 0; index < Label_total.Count; index++)
        {
            if (Label_total[index] == Label)
            {
                gamemanager.labeldetail.Detail_LabelImage.sprite = DetailImage[index];
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        gamemanager.labeldetail.TitleDetail.text = Title_K[index];
                        gamemanager.labeldetail.TitleDetail.font = Titlefont_KE;
                        gamemanager.labeldetail.TitleDetail.fontSize = 30;
                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                        gamemanager.labeldetail.SubTitleDetail.font = Titlefont_KE;
                        if (Title_E[index].Length < 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                        }
                        else if (Title_E[index].Length >= 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                        }
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_K[index];
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_KE;
                        Narration.clip = Narration_K[index];
                        break;
                    case GameManager.Language_enum.English:
                        gamemanager.labeldetail.TitleDetail.text = Title_E[index];
                        gamemanager.labeldetail.TitleDetail.font = Titlefont_KE;
                        if (Title_E[index].Length < 25)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 30;
                        }
                        else if (Title_E[index].Length >= 25)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 26;
                        }

                        if (ContentsInfo.ContentsName == "Aegibong" && Title_E[index].Contains("Gijeongdong") == true)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 24;
                        }
                        gamemanager.labeldetail.SubTitleDetail.text = Title_K[index];
                        gamemanager.labeldetail.SubTitleDetail.font = Titlefont_KE;
                        gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_E[index];
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_KE;
                        Narration.clip = Narration_E[index];
                        break;
                    case GameManager.Language_enum.Chinese:
                        gamemanager.labeldetail.TitleDetail.text = Title_C[index];
                        gamemanager.labeldetail.TitleDetail.font = Titlefont_CJ;
                        gamemanager.labeldetail.TitleDetail.fontSize = 30;
                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                        gamemanager.labeldetail.SubTitleDetail.font = Titlefont_KE;
                        if (Title_E[index].Length < 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                        }
                        else if (Title_E[index].Length >= 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                        }
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_C[index];
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_CJ;
                        Narration.clip = Narration_C[index];
                        break;
                    case GameManager.Language_enum.Japanese:
                        gamemanager.labeldetail.TitleDetail.text = Title_J[index];
                        gamemanager.labeldetail.TitleDetail.font = Titlefont_CJ;
                        if (Title_J[index].Length < 15)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 30;
                        }
                        else if (Title_J[index].Length >= 15)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 22;
                        }
                        gamemanager.labeldetail.SubTitleDetail.text = Title_E[index];
                        gamemanager.labeldetail.SubTitleDetail.font = Titlefont_KE;
                        if (Title_E[index].Length < 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                        }
                        else if (Title_E[index].Length >= 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                        }
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = DetailTexts_J[index];
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = Detailfont_CJ;
                        Narration.clip = Narration_J[index];
                        break;
                }
            }
        }

        Canvas.ForceUpdateCanvases();
        gamemanager.labeldetail.ClickLabel();
    }

    /// <summary>
    /// 카테고리 선택시 선택 안한 카테고리 비활성화
    /// </summary>
    /// <param name="cate"></param>
    public void SelectCategortButton(GameObject cate)
    {
        Label_navi.Clear();

        int cnt = LabelsParent.gameObject.transform.childCount;


        if (cnt > 0)
        {
            for (int index = cnt - 1; index >= 0; index--)
            {
                Destroy(LabelsParent.gameObject.transform.GetChild(index).gameObject);
            }
        }
        
        switch (cate.name)
        {
            case "Total":
                curcate = CurrentCategory.Total;
                break;
            case "Eco":
                curcate = CurrentCategory.Eco;
                break;
            case "Building":
                curcate = CurrentCategory.Building;
                break;
        }

        switch (GameManager.currentLang)
        {
            case GameManager.Language_enum.Korea:
                CategorySelect_korea();
                break;
            case GameManager.Language_enum.English:
                CategorySelect_English();
                break;
            case GameManager.Language_enum.Chinese:
                CategorySelect_Chinese();
                break;
            case GameManager.Language_enum.Japanese:
                CategorySelect_Japanese();
                break;
        }

        SelectCategory(cate.name);
        SetCategory();
    }

    // 카테고리 리스트 바꾸기
    // 선택한 카테고리에 따라 네비게이션 리스트 변경 및 사이즈 변경
    public void SelectCategory(string category)
    {
        if (category != "Total")
        {
            for (int index = 0; index < Label_total.Count; index++)
            {
                Label_navi.Add(Label_total[index]);
            }

            if (!category.Contains("Eco"))
            {
                for (int index = Label_Cate_1.Count - 1; index >= 0; index--)
                {
                    Label_navi.Remove(Label_Cate_1[index]);
                }
            }
            if (!category.Contains("Building"))
            {
                for (int index = Label_Cate_2.Count - 1; index >= 0; index--)
                {
                    Label_navi.Remove(Label_Cate_2[index]);
                }
            }
        }
        else if (category == "Total")
        {
            if (Label_navi.Count != Label_total.Count)
            {
                for (int index = 0; index < Label_total.Count; index++)
                {
                    Label_navi.Add(Label_total[index]);
                }
            }
        }

        // 각 모드에서 선택한 카테고리만 활성화하고 나머지 라벨은 비활성화
        if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            LabelsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -12 + Label_navi.Count * Label_origin.GetComponent<RectTransform>().rect.height + 27 * Label_navi.Count);

            // 네비게이션 라벨 생성
            for (int index = 0; index < Label_navi.Count; index++)
            {
                GameObject obj = Instantiate(Label_origin);
                obj.transform.SetParent(LabelsParent.transform);
                obj.name = Label_navi[index];
                obj.transform.localPosition = new Vector3(0, -12 - Label_origin.GetComponent<RectTransform>().rect.height * index - 27 * index, 0);
                //obj.gameObject.GetComponent<NaviLabel>().LabelIcon.sprite = NaviLabel[index];

                //for (int Sindex = 0; Sindex < Label_total.Count; Sindex++)
                //{
                //    NaviLabelLanguage(obj, Sindex);
                //}
            }

            for (int index = 0; index < gamemanager.clearmode.AllMapLabels.transform.childCount; index++)
            {
                gamemanager.clearmode.AllMapLabels.transform.GetChild(index).gameObject.SetActive(false);
            }

            for (int index_n = 0; index_n < Label_navi.Count; index_n++)
            {
                for (int index = 0; index < gamemanager.clearmode.AllMapLabels.transform.childCount; index++)
                {
                    if (Label_navi[index_n] == gamemanager.clearmode.AllMapLabels.transform.GetChild(index).gameObject.name)
                    {
                        gamemanager.clearmode.AllMapLabels.transform.GetChild(index).gameObject.SetActive(true);
                    }
                }
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (FunctionCustom.OtherList == true)
            {
                CustomList();
            }
            LabelsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, -12 + Label_navi.Count * Label_origin.GetComponent<RectTransform>().rect.height + 27 * Label_navi.Count);

            // 네비게이션 라벨 생성
            for (int index = 0; index < Label_navi.Count; index++)
            {
                GameObject obj = Instantiate(Label_origin);
                obj.transform.SetParent(LabelsParent.transform);
                obj.name = Label_navi[index];
                obj.transform.localPosition = new Vector3(0, -12 - Label_origin.GetComponent<RectTransform>().rect.height * index - 27 * index, 0);
                //obj.gameObject.GetComponent<NaviLabel>().LabelIcon.sprite = NaviLabel[index];

                //gamemanager.labelmake.NavigationText();
                //for (int Sindex = 0; Sindex < Label_total.Count; Sindex++)
                //{
                //    NaviLabelLanguage(obj, Sindex);
                //}
            }

            if (GameManager.SettingLabelPosition == false)
            {
                for (int index = 0; index < gamemanager.xrmode.AllMapLabels.transform.childCount; index++)
                {
                    gamemanager.xrmode.AllMapLabels.transform.GetChild(index).gameObject.SetActive(false);
                }

                for (int index_n = 0; index_n < Label_navi.Count; index_n++)
                {
                    for (int index = 0; index < gamemanager.xrmode.AllMapLabels.transform.childCount; index++)
                    {
                        if (Label_navi[index_n] == gamemanager.xrmode.AllMapLabels.transform.GetChild(index).gameObject.name)
                        {
                            gamemanager.xrmode.AllMapLabels.transform.GetChild(index).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        gamemanager.labelmake.ReadytoStart();
        //gamemanager.labelmake.NavigationText();
    }

    public void NaviLabelLanguage(GameObject obj, int totalnum)
    {
        if (obj.name == Label_total[totalnum])
        {
            if (GameManager.currentLang == GameManager.Language_enum.Korea)
            {
                //obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = Title_K[totalnum];
                obj.GetComponent<NaviLabel>().labelname.text = Title_K[totalnum];
                //obj.gameObject.GetComponent<Image>().sprite = NaviLabel_K[totalnum];
            }
            else if (GameManager.currentLang == GameManager.Language_enum.English)
            {
                //obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = Title_E[totalnum];
                obj.GetComponent<NaviLabel>().labelname.text = Title_E[totalnum];
                //obj.gameObject.GetComponent<Image>().sprite = NaviLabel_E[totalnum];
            }
            else if (GameManager.currentLang == GameManager.Language_enum.Chinese)
            {
                //obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = Title_C[totalnum];
                obj.GetComponent<NaviLabel>().labelname.text = Title_C[totalnum];
                //obj.gameObject.GetComponent<Image>().sprite = NaviLabel_C[totalnum];
            }
            else if (GameManager.currentLang == GameManager.Language_enum.Japanese)
            {
                //obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = Title_J[totalnum];
                obj.GetComponent<NaviLabel>().labelname.text = Title_J[totalnum];
                //obj.gameObject.GetComponent<Image>().sprite = NaviLabel_J[totalnum];
            }
        }
    }

    public void CustomList()
    {
        switch (FunctionCustom.telescopestate)
        {
            case FunctionCustom.TelescopeState.Right:
                switch (ContentsInfo.ContentsName)
                {
                    case "Basic":
                        for (int index_o = ContentsOtherList.BasicList_Right.Count - 1; index_o >= 0; index_o--)
                        {
                            Label_navi.Remove(ContentsOtherList.BasicList_Right[index_o]);
                        }
                        break;
                    case "Apsan":
                        for (int index_o = ContentsOtherList.ApsanList_Right.Count - 1; index_o >= 0; index_o--)
                        {
                            Label_navi.Remove(ContentsOtherList.ApsanList_Right[index_o]);
                        }
                        break;
                    case "Aegibong":
                        for (int index_o = ContentsOtherList.Aegibong_Right.Count - 1; index_o >= 0; index_o--)
                        {
                            Label_navi.Remove(ContentsOtherList.Aegibong_Right[index_o]);
                        }
                        break;
                }
                break;
            case FunctionCustom.TelescopeState.Left:
                switch (ContentsInfo.ContentsName)
                {
                    case "Basic":
                        for (int index_o = ContentsOtherList.BasicList_Left.Count - 1; index_o >= 0; index_o--)
                        {
                            Label_navi.Remove(ContentsOtherList.BasicList_Left[index_o]);
                        }
                        break;
                    case "Apsan":
                        for (int index_o = ContentsOtherList.ApsanList_Left.Count - 1; index_o >= 0; index_o--)
                        {
                            Label_navi.Remove(ContentsOtherList.ApsanList_Left[index_o]);
                        }
                        break;
                    case "Aegibong":
                        for (int index_o = ContentsOtherList.Aegibong_Left.Count - 1; index_o >= 0; index_o--)
                        {
                            Label_navi.Remove(ContentsOtherList.Aegibong_Left[index_o]);
                        }
                        break;
                }
                break;
        }
    }
}
