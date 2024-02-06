using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TourismLite : MonoBehaviour
{
    private GameManager gamemanager;
    public ConnectAPI seemap;
    public GameObject AllRoute;
    public GameObject AnnounceRoute;

    public Image RouteName;
    public Image RouteState;
    public GameObject CarEffect;
    public GameObject CurrentPos;
    public GameObject[] PosName;
    public Text PosAddress;

    float AnimationTime;
    float RouteState_amount;
    float CurPos_X;
    float CurPosCar_X;

    bool StartAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gamemanager.tourLite = this;
        UISetting();

        AllRoute.SetActive(true);
        AnnounceRoute.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartAnimation == true)
        {
            AnimationTime += Time.deltaTime;

            if (RouteState.fillAmount != RouteState_amount)
            {
                if (RouteState_amount > RouteState.fillAmount)
                {
                    RouteState.fillAmount += 0.0015f;
                }
                else if (RouteState_amount < RouteState.fillAmount)
                {
                    RouteState.fillAmount -= 0.0015f;
                }
            }

            if(Mathf.Abs(CarEffect.transform.localPosition.x-((RouteState.fillAmount * 1120) - 495))>10)
            {
                CarEffect.transform.localPosition = new Vector3((RouteState.fillAmount*1120)-495, 54, 0);
            }
            else
            {
                if (Mathf.Abs(RouteState_amount- RouteState.fillAmount)<0.05f)
                {
                    if(Mathf.Abs(RouteState_amount - RouteState.fillAmount) * 20 >= 0.2f)
                    {
                        CarEffect.GetComponent<Image>().fillAmount = Mathf.Abs(RouteState_amount - RouteState.fillAmount) * 20;
                    }
                    else
                    {
                        CarEffect.GetComponent<Image>().fillAmount = 0;
                    }
                }
            }
            //if ((CarEffect.transform.position.x - CurPosCar_X) > 0.1f)
            //{
            //    CarEffect.transform.position = new Vector2(Mathf.Lerp(CarEffect.transform.position.x, CurPosCar_X, AnimationTime), CarEffect.transform.position.y);
            //}
            //else if ((CarEffect.transform.position.x - CurPosCar_X) <= 0.1f)
            //{
            //    CarEffect.transform.position = new Vector2(CurPosCar_X, CarEffect.transform.position.y);
            //}
        }
    }

    public void UISetting()
    {
        switch (ContentsInfo.ContentsName)
        {
            case "GoldSunset":
                //clearmode = GameObject.FindGameObjectWithTag("ClearMode").GetComponent<ClearMode>();
                gamemanager.labeldetail = GameObject.Find("TourismMode").GetComponent<LabelDetail>();
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

    Sprite[] RSprite;
    int num;
    public string clicknaviobj;

    public void ChangeLang()
    {
        switch (GameManager.currentLang)
        {
            case GameManager.Language_enum.Korea:
                if (PosName[0].transform.GetChild(0).gameObject.GetComponent<Text>().text != gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num])
                {
                    for (int index = 0; index < PosName.Length; index++)
                    {
                        PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                        PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text = "";

                        //PosName[index].transform.GetChild(0).gameObject.transform.localPosition = new Vector2(0, PosName[index].transform.GetChild(0).gameObject.transform.position.y);
                        //PosName[index].transform.GetChild(1).gameObject.transform.localPosition = new Vector2(0, PosName[index].transform.GetChild(1).gameObject.transform.position.y);
                    }

                    for (int index = 0; index < PosName.Length; index++)
                    {
                        if (PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text == "")
                        {
                            PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num + index];
                            //PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text = gamemanager.label.Title_E[GoldSunsetLabel.Label_total.Count + PosName.Length * num + index];

                            PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);
                            //PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);

                            PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().fontSize = 26;
                        }
                    }
                }
                break;
            case GameManager.Language_enum.English:
                if (PosName[0].transform.GetChild(0).gameObject.GetComponent<Text>().text != gamemanager.label.Title_E[GoldSunsetLabel.Label_total.Count + PosName.Length * num])
                {
                    for (int index = 0; index < PosName.Length; index++)
                    {
                        PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                        PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text = "";

                        //PosName[index].transform.GetChild(0).gameObject.transform.localPosition = new Vector2(0, PosName[index].transform.GetChild(0).gameObject.transform.position.y);
                        //PosName[index].transform.GetChild(1).gameObject.transform.localPosition = new Vector2(0, PosName[index].transform.GetChild(1).gameObject.transform.position.y);
                    }

                    for (int index = 0; index < PosName.Length; index++)
                    {
                        if (PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text == "")
                        {
                            PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = gamemanager.label.Title_E[GoldSunsetLabel.Label_total.Count + PosName.Length * num + index];
                            //PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text = gamemanager.label.Title_E[GoldSunsetLabel.Label_total.Count + PosName.Length * num + index];

                            PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);
                            //PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);

                            PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().fontSize = 20;
                        }
                    }
                }
                break;
        }

        if(CurrentPos.transform.position.x == 544)
        {
            NPostion("first");
        } else if(CurrentPos.transform.position.x == 743)
        {
            NPostion("second");
        }
        else if (CurrentPos.transform.position.x == 940)
        {
            NPostion("third");
        }
        else if (CurrentPos.transform.position.x == 1138)
        {
            NPostion("fourth");
        }
        else if (CurrentPos.transform.position.x == 1336)
        {
            NPostion("fifth");
        }
        else if (CurrentPos.transform.position.x == 1534)
        {
            NPostion("sixth");
        }
    }
    
    public void SelectRoute(GameObject btn)
    {
        AllRoute.SetActive(false);
        AnnounceRoute.SetActive(true);
        switch (btn.name)
        {
            case "Route1":
                if (ContentsInfo.ContentsName == "GoldSunset")
                {
                    RSprite = GoldSunset_Tour.Route1;
                    num = 0;
                }
                break;
            case "Route2":
                if (ContentsInfo.ContentsName == "GoldSunset")
                {
                    RSprite = GoldSunset_Tour.Route2;
                    num = 1;
                }
                break;
            case "Route3":
                if (ContentsInfo.ContentsName == "GoldSunset")
                {
                    RSprite = GoldSunset_Tour.Route3;
                    num = 2;
                }
                break;
        }

        for (int index = 0; index < PosName.Length; index++)
        {
            PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
            PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text = "";

            //PosName[index].transform.GetChild(0).gameObject.transform.localPosition = new Vector2(0, PosName[index].transform.GetChild(0).gameObject.transform.position.y);
            //PosName[index].transform.GetChild(1).gameObject.transform.localPosition = new Vector2(0, PosName[index].transform.GetChild(1).gameObject.transform.position.y);
        }

        if (ContentsInfo.ContentsName == "GoldSunset")
        {
            RouteState.sprite = RSprite[2];
            CarEffect.GetComponent<Image>().sprite = RSprite[3];
            CurrentPos.GetComponent<Image>().sprite = RSprite[4];
            for (int index = 0; index < PosName.Length; index++)
            {
                PosName[index].GetComponent<Image>().sprite = RSprite[5 + index];
            }

            if (GameManager.currentLang == GameManager.Language_enum.Korea)
            {
                RouteName.sprite = RSprite[0];
                for (int index = 0; index < PosName.Length; index++)
                {
                    if (PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text == "")
                    {
                        PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num + index];
                        //PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text = gamemanager.label.Title_E[GoldSunsetLabel.Label_total.Count + PosName.Length * num + index];

                        PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);
                        //PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);

                        PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().fontSize = 26;

                        //PosName[index].transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text.Length * 22, PosName[index].transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.y);

                        //if (PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text.Length * 18 <= 160) {
                        //    PosName[index].transform.GetChild(1).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text.Length * 18, 30);
                        //} else if(PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text.Length * 18 > 160 && PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text.Length * 18 < 190 )
                        //{
                        //    PosName[index].transform.GetChild(1).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(160,48);
                        //}
                        //else if (PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text.Length * 18 >= 190)
                        //{
                        //    PosName[index].transform.GetChild(1).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(190, 48);
                        //}
                    }
                }
            }
            else if (GameManager.currentLang == GameManager.Language_enum.English)
            {
                RouteName.sprite = RSprite[1];
                for (int index = 0; index < PosName.Length; index++)
                {
                    if (PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text == "")
                    {
                        PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text = gamemanager.label.Title_E[GoldSunsetLabel.Label_total.Count + PosName.Length * num + index];
                        //PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().text = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num + index];

                        PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);
                        //PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);

                        PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().fontSize = 20;

                        //PosName[index].transform.GetChild(1).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text.Length * 18, 30);

                        //if (PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text.Length * 22 <= 160)
                        //{
                        //    PosName[index].transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text.Length * 22, 30);
                        //}
                        //else if (PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text.Length * 22 > 160 && PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text.Length * 18 < 190)
                        //{
                        //    PosName[index].transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 48);
                        //}
                        //else if (PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().text.Length * 18 >= 190)
                        //{
                        //    PosName[index].transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(190, 48);
                        //}
                    }
                }
            }
        }

        NPostion("first");
    }

    public void NPostion(string NP)
    {
        CarEffect.GetComponent<Image>().fillAmount = 1;
        for (int index = 0; index < PosName.Length; index++)
        {
            PosName[index].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);
            PosName[index].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.6196079f, 0.6196079f, 0.6196079f);
        }

        switch (NP)
        {
            case "first":
                PosName[0].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);
                PosName[0].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);

                RouteState_amount = 0.07f;
                CurrentPos.transform.position = new Vector3(544,957,0);
                CurPosCar_X = 569;

                clicknaviobj = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num];

                if (GameManager.currentLang == GameManager.Language_enum.Korea)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_K[PosName.Length * num];
                } else if (GameManager.currentLang == GameManager.Language_enum.English)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_E[PosName.Length * num];
                }

                CarEffect.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case "second":
                PosName[1].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);
                PosName[1].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);

                RouteState_amount = 0.24f;
                CurrentPos.transform.position = new Vector3(743, 957, 0);
                CurPosCar_X = 763;

                clicknaviobj = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count+PosName.Length * num + 1];

                if (GameManager.currentLang == GameManager.Language_enum.Korea)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_K[PosName.Length * num + 1];
                }
                else if (GameManager.currentLang == GameManager.Language_enum.English)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_E[PosName.Length * num + 1];
                }

                if (CarEffect.GetComponent<RectTransform>().anchoredPosition.x < -230)
                {
                    CarEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    CarEffect.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                    break;
            case "third":
                PosName[2].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);
                PosName[2].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);

                RouteState_amount = 0.4f;
                CurrentPos.transform.position = new Vector3(940, 957, 0);
                CurPosCar_X = 959;

                clicknaviobj = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num + 2];

                if (GameManager.currentLang == GameManager.Language_enum.Korea)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_K[PosName.Length * num + 2];
                }
                else if (GameManager.currentLang == GameManager.Language_enum.English)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_E[PosName.Length * num + 2];
                }

                if (CarEffect.GetComponent<RectTransform>().anchoredPosition.x < -52)
                {
                    CarEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    CarEffect.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case "fourth":
                PosName[3].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);
                PosName[3].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);

                RouteState_amount = 0.58f;
                CurrentPos.transform.position = new Vector3(1138, 957, 0);
                CurPosCar_X = 1154;

                clicknaviobj = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num + 3];

                if (GameManager.currentLang == GameManager.Language_enum.Korea)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_K[PosName.Length * num + 3];
                }
                else if (GameManager.currentLang == GameManager.Language_enum.English)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_E[PosName.Length * num + 3];
                }


                if (CarEffect.GetComponent<RectTransform>().anchoredPosition.x < 150)
                {
                    CarEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    CarEffect.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case "fifth":
                PosName[4].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);
                PosName[4].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);

                RouteState_amount = 0.76f;
                CurrentPos.transform.position = new Vector3(1336, 957, 0);
                CurPosCar_X = 1354;

                clicknaviobj = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num + 4];

                if (GameManager.currentLang == GameManager.Language_enum.Korea)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_K[PosName.Length * num + 4];
                }
                else if (GameManager.currentLang == GameManager.Language_enum.English)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_E[PosName.Length * num + 4];
                }

                if (CarEffect.GetComponent<RectTransform>().anchoredPosition.x < 360)
                {
                    CarEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    CarEffect.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                break;
            case "sixth":
                PosName[5].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);
                PosName[5].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.2588235f, 0.2588235f, 0.2588235f);

                RouteState_amount = 0.94f;
                CurrentPos.transform.position = new Vector3(1534, 957, 0);
                CurPosCar_X = 1551;

                clicknaviobj = gamemanager.label.Title_K[GoldSunsetLabel.Label_total.Count + PosName.Length * num + 5];

                if (GameManager.currentLang == GameManager.Language_enum.Korea)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_K[PosName.Length * num + 5];
                }
                else if (GameManager.currentLang == GameManager.Language_enum.English)
                {
                    PosAddress.text = GoldSunset_Tour.Posadd_E[PosName.Length * num + 5];
                }

                CarEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
        seemap.SelectPos(clicknaviobj);
        SelectLabel(clicknaviobj);
        StartAnimation = true;
    }

    /// <summary>
    /// 라벨 선택하면 상세설명창, 설명 셋팅
    /// </summary>
    /// <param name="Label"></param>
    public void SelectLabel(string Label)
    {
        for (int index = gamemanager.label.Label_total.Count; index < gamemanager.label.Title_K.Length; index++)
        {
            if (gamemanager.label.Title_K[index] == Label)
            {
                gamemanager.labeldetail.Detail_LabelImage.sprite = gamemanager.label.DetailImage[index];
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        gamemanager.labeldetail.TitleDetail.text = gamemanager.label.Title_K[index];
                        gamemanager.labeldetail.TitleDetail.font = gamemanager.label.Titlefont_KE;
                        gamemanager.labeldetail.TitleDetail.fontSize = 30;
                        gamemanager.labeldetail.SubTitleDetail.text = gamemanager.label.Title_E[index];
                        gamemanager.labeldetail.SubTitleDetail.font = gamemanager.label.Titlefont_KE;
                        if (gamemanager.label.Title_E[index].Length < 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                        }
                        else if (gamemanager.label.Title_E[index].Length >= 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                        }
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = gamemanager.label.DetailTexts_K[index];
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = gamemanager.label.Detailfont_KE;
                        gamemanager.label.Narration.clip = gamemanager.label.Narration_K[index];
                        break;
                    case GameManager.Language_enum.English:
                        gamemanager.labeldetail.TitleDetail.text = gamemanager.label.Title_E[index];
                        gamemanager.labeldetail.TitleDetail.font = gamemanager.label.Titlefont_KE;
                        if (gamemanager.label.Title_E[index].Length < 25)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 30;
                        }
                        else if (gamemanager.label.Title_E[index].Length >= 25)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 26;
                        }

                        gamemanager.labeldetail.SubTitleDetail.text = gamemanager.label.Title_K[index];
                        gamemanager.labeldetail.SubTitleDetail.font = gamemanager.label.Titlefont_KE;
                        gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = gamemanager.label.DetailTexts_E[index];
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = gamemanager.label.Detailfont_KE;
                        gamemanager.label.Narration.clip = gamemanager.label.Narration_E[index];
                        break;
                    case GameManager.Language_enum.Chinese:
                        gamemanager.labeldetail.TitleDetail.text = gamemanager.label.Title_C[index];
                        gamemanager.labeldetail.TitleDetail.font = gamemanager.label.Titlefont_CJ;
                        gamemanager.labeldetail.TitleDetail.fontSize = 30;
                        gamemanager.labeldetail.SubTitleDetail.text = gamemanager.label.Title_E[index];
                        gamemanager.labeldetail.SubTitleDetail.font = gamemanager.label.Titlefont_KE;
                        if (gamemanager.label.Title_E[index].Length < 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                        }
                        else if (gamemanager.label.Title_E[index].Length >= 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                        }
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = gamemanager.label.DetailTexts_C[index];
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = gamemanager.label.Detailfont_CJ;
                        gamemanager.label.Narration.clip = gamemanager.label.Narration_C[index];
                        break;
                    case GameManager.Language_enum.Japanese:
                        gamemanager.labeldetail.TitleDetail.text = gamemanager.label.Title_J[index];
                        gamemanager.labeldetail.TitleDetail.font = gamemanager.label.Titlefont_CJ;
                        if (gamemanager.label.Title_J[index].Length < 15)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 30;
                        }
                        else if (gamemanager.label.Title_J[index].Length >= 15)
                        {
                            gamemanager.labeldetail.TitleDetail.fontSize = 22;
                        }
                        gamemanager.labeldetail.SubTitleDetail.text = gamemanager.label.Title_E[index];
                        gamemanager.labeldetail.SubTitleDetail.font = gamemanager.label.Titlefont_KE;
                        if (gamemanager.label.Title_E[index].Length < 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 24;
                        }
                        else if (gamemanager.label.Title_E[index].Length >= 25)
                        {
                            gamemanager.labeldetail.SubTitleDetail.fontSize = 20;
                        }
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().text = gamemanager.label.DetailTexts_J[index];
                        gamemanager.labeldetail.InfoHeight.GetComponent<Text>().font = gamemanager.label.Detailfont_CJ;
                        gamemanager.label.Narration.clip = gamemanager.label.Narration_J[index];
                        break;
                }
                gamemanager.label.Narration.Play();
            }
        }

        Canvas.ForceUpdateCanvases();
        gamemanager.labeldetail.ClickLabel();
    }

    public void DPSoundClick()
    {
        if (gamemanager.label.Narration.isPlaying)
        {
            gamemanager.label.Narration.Stop();
        }
        else
        {
            gamemanager.label.Narration.Play();
        }
    }
}
