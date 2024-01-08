using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllBarOnOff : MonoBehaviour
{
    public GameManager gamemanager;
    public Image NaviBackground;
    public Image LangChildImg;
    public Image EtcChildImg;

    // 네비게이션창, 언어선택창 On/Off
    public float navi_t;
    public float langnavi_t;
    public float filternavi_t;
    public float ETCnavi_t;
    private bool touchfinish = false;
    public static float touchCount;

    public RectTransform NaviRect;
    public RectTransform ETCRect;
    public RectTransform LangRect;
    public Scrollbar naviscroll;

    public static float barOpen = 472f;
    public static float barClose = 60f;

    public bool NaviOn = false;
    public bool langNaviOn = false;
    public bool filterNaviOn = false;
    public bool ETCNaviOn = false;
    public bool moveNavi = false;
    public bool movelangNavi = false;
    public bool movefilterNavi = false;
    public bool moveETCNavi = false;

    bool alreadynaviLog = false;

    // Start is called before the first frame update
    void Start()
    {
        navi_t = 0;
        langnavi_t = 0;

        //NaviRect = NavigationBar.GetComponent<RectTransform>();
        //LangRect = LanguageBar.GetComponent<RectTransform>();
        //LangChildImg = LanguageBar.transform.GetChild(0).gameObject.GetComponent<Image>();

        // 언어선택창 닫아놓기(로딩화면에서 안보임.)
        LangRect.sizeDelta = new Vector2(barClose, 1080);
        LangChildImg.fillAmount = 0;
        gamemanager.LanguageBar.transform.GetChild(0).gameObject.SetActive(false);
        langNaviOn = false;
        movelangNavi = false;

        // ETC창 닫아놓기(로딩화면에서 안보임.)
        ETCRect.sizeDelta = new Vector2(barClose, 1080);
        gamemanager.ETCBar.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = 0;
        gamemanager.ETCBar.transform.GetChild(0).gameObject.SetActive(false);
        ETCNaviOn = false;
        moveETCNavi = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 네비게이션 창, 상세설명창 속도 조절 
        navi_t += Time.deltaTime * 0.1f;
        langnavi_t += Time.deltaTime * 0.1f;
        ETCnavi_t += Time.deltaTime * 0.1f;

        // 네비게이션 창 라벨선택부 스크롤바 사이즈
        naviscroll.value = Mathf.Clamp(naviscroll.value, 0, 1);
        naviscroll.size = 0.0f;

        if (moveNavi == true)
        {
            NaviArr_set();
        }

        if (movelangNavi == true)
        {
            SelectLanguageChange();
        }
    }

    /// <summary>
    /// 네비게이션 창 활성화/비활성화
    /// </summary>
    public void NaviArr_set()
    {
        //Image NaviBackground = gamemanager.NavigationBar.transform.GetChild(0).gameObject.GetComponent<Image>();

        // 네비게이션 비활성화
        if (NaviOn == true)
        {
            // 기존 기획 : 메뉴바가 길어져서 네비게이션 창이 생성
            // 현재 기획 : 네비게이션 창이 따로 있어서 크기가 커짐

            // 네비게이션창 로그
            if (alreadynaviLog == false)
            {
                if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Navigation, "XR_NavigationOff", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Navigation, "Clear_NavigationOff", GetType().ToString());
                }
                alreadynaviLog = true;
            }

            // 네비게이션창이 비활성화 되어있기 때문에 언어선택 창이 활성화/비활성홛되어 있는 상태에 따라 화살표 UI 활성화/비활성화
            if (LangRect.sizeDelta.x < barOpen)
            {
                if (ETCRect.sizeDelta.x < barOpen)
                {
                    if (FunctionCustom.GuideMode == false && FunctionCustom.PastMode == false && FunctionCustom.View360 == false)
                    {
                        if(!SceneManager.GetActiveScene().name.Contains("VisitMode"))
                        {
                            gamemanager.Arrow.gameObject.SetActive(true);
                            gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                        }
                    }
                    else
                    {
                        if (FunctionCustom.GuideMode == true && FunctionCustom.functionorigin.guidemode.GuideObj.activeSelf)
                        {
                            gamemanager.Arrow.gameObject.SetActive(false);
                        }
                        else if (FunctionCustom.PastMode == true && FunctionCustom.functionorigin.pastcontents.obj360.activeSelf)
                        {
                            gamemanager.Arrow.gameObject.SetActive(false);
                        }
                        else if (FunctionCustom.View360 == true && FunctionCustom.functionorigin.view360.obj360.activeSelf)
                        {
                            gamemanager.Arrow.gameObject.SetActive(false);
                        }
                        else
                        {
                            gamemanager.Arrow.gameObject.SetActive(true);
                            gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                        }
                    }
                }
                else if (ETCRect.sizeDelta.x > barClose)
                {
                    gamemanager.Arrow.gameObject.SetActive(false);
                }
            }
            else if (LangRect.sizeDelta.x > barClose)
            {
                gamemanager.Arrow.gameObject.SetActive(false);
            }

            gamemanager.NavigationBar.gameObject.SetActive(true);   // 네비게이션 창 활성화

            // 네비게이션 창이 비활성화되어 있기 때문에 네비게이션 라벨 버튼 비활성화
            //GameObject NaviLabel = NavigationBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
            {
                gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
            }
            if (NaviRect.sizeDelta.x > barClose)
            {
                NaviRect.sizeDelta = Vector2.Lerp(NaviRect.sizeDelta, new Vector2(barClose - 5f, 1080), navi_t);
                NaviBackground.fillAmount -= 0.5f * navi_t;
            }
            else if (NaviRect.sizeDelta.x <= barClose)
            {
                NaviRect.sizeDelta = new Vector2(barClose, 1080);
                NaviBackground.fillAmount = 0;
                gamemanager.NavigationBar.transform.GetChild(0).gameObject.SetActive(false);
                NaviOn = false;
                moveNavi = false;
                alreadynaviLog = false;
            }
        }
        else if (NaviOn == false)       // 네비게이션 활성화
        {
            // 네비게이션창 활성화 로그
            if (alreadynaviLog == false)
            {
                if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Navigation, "XR_NavigationOn", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Navigation, "Clear_NavigationOn", GetType().ToString());
                }
                alreadynaviLog = true;
            }

            gamemanager.NavigationBar.transform.GetChild(0).gameObject.SetActive(true);     // 네비게이션창 활성화
            gamemanager.Arrow.gameObject.SetActive(false);      // 네비게이션창이 활성화 되어있기 때문에 화살표는 비활성화

            // 네비게이션 창 천천히 활성화(열리는 효과)
            if (NaviRect.sizeDelta.x < barOpen)
            {
                NaviRect.sizeDelta = Vector2.Lerp(NaviRect.sizeDelta, new Vector2(barOpen + 5f, 1080), navi_t);
                NaviBackground.fillAmount += 0.5f * navi_t;
            }
            else if (NaviRect.sizeDelta.x >= barOpen)
            {
                gamemanager.NavigationBar.gameObject.SetActive(true);
                gamemanager.NavigationBar.transform.GetChild(0).gameObject.SetActive(true);

                gamemanager.NavigationBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<ScrollRect>().enabled = true;
                NaviRect.sizeDelta = new Vector2(barOpen, 1080);
                NaviBackground.fillAmount = 1;

                // 네비게이션 창이 모두 펴지면 네비게이션 라벨 버튼 활성화
                //GameObject NaviLabel = NavigationBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
                {
                    gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
                }
                NaviOn = true;
                moveNavi = false;
                alreadynaviLog = false;
            }
        }
    }

    /// <summary>
    /// 메뉴바에서 언어선택 아이콘 선택하면 언어선택창 활성화/비활성화
    /// </summary>
    public void SelectLanguageChange()
    {
        if (langNaviOn == true)     // 언어선택 창 비활성화
        {
            if (!SceneManager.GetActiveScene().name.Contains("CultureMode"))        // 역사모드가 아니면 네비게이션 창의 상태에 따라(활성화/비활성화) 화살표UI 활성화 비활성화
            {
                if (NaviRect.sizeDelta.x < barOpen)     // 네비게이션 창이 비활성화 상태이므로 화살표 UI 활성화
                {
                    if (ETCRect.sizeDelta.x < barOpen)     // 네비게이션 창이 비활성화 상태이므로 화살표 UI 활성화
                    {
                        if (FunctionCustom.GuideMode == false && FunctionCustom.PastMode == false && FunctionCustom.View360 == false)
                        {
                            if (!SceneManager.GetActiveScene().name.Contains("VisitMode"))
                            {
                                gamemanager.Arrow.gameObject.SetActive(true);
                                gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                            }
                        }
                        else
                        {
                            if (FunctionCustom.GuideMode == true && FunctionCustom.functionorigin.guidemode.GuideObj.activeSelf)
                            {
                                gamemanager.Arrow.gameObject.SetActive(false);
                            }
                            else if (FunctionCustom.PastMode == true && FunctionCustom.functionorigin.pastcontents.obj360.activeSelf)
                            {
                                gamemanager.Arrow.gameObject.SetActive(false);
                            }
                            else if (FunctionCustom.View360 == true && FunctionCustom.functionorigin.view360.obj360.activeSelf)
                            {
                                gamemanager.Arrow.gameObject.SetActive(false);
                            }
                            else
                            {
                                gamemanager.Arrow.gameObject.SetActive(true);
                                gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                            }
                        }
                    }
                    else if (ETCRect.sizeDelta.x > barClose)     // 네비게이션 창이 비활성화 상태이므로 화살표 UI 활성화
                    {
                        gamemanager.Arrow.gameObject.SetActive(false);
                    }
                }
                else if (NaviRect.sizeDelta.x > barClose)     // 네비게이션 창이 활성화상태이므로 화살표 UI 비활성화
                {
                    gamemanager.Arrow.gameObject.SetActive(false);
                }
            }
            else if (SceneManager.GetActiveScene().name.Contains("CultureMode"))      // 역사모드에서는 네비게이션 창에 상관없이 화살표 UI 비활성화
            {
                gamemanager.Arrow.gameObject.SetActive(false);
            }

            // 언어선택 창 비활성화 진행
            if (LangRect.sizeDelta.x > barClose)
            {
                LangRect.sizeDelta = Vector2.Lerp(LangRect.sizeDelta, new Vector2(barClose - 5f, 1080), langnavi_t);
                LangChildImg.fillAmount -= 0.5f * langnavi_t;
            }
            else if (LangRect.sizeDelta.x <= barClose)
            {
                LangRect.sizeDelta = new Vector2(barClose, 1080);
                LangChildImg.fillAmount = 0;
                gamemanager.LanguageBar.transform.GetChild(0).gameObject.SetActive(false);
                gamemanager.LanguageBtn.transform.GetChild(0).gameObject.SetActive(false);
                langNaviOn = false;
                movelangNavi = false;
            }
        }
        else if (langNaviOn == false)       // 언어선택 활성화
        {
            gamemanager.LanguageBar.transform.GetChild(0).gameObject.SetActive(true);       // 언어선택 창 활성화
            gamemanager.Arrow.gameObject.SetActive(false);      // 언어선택창이 활성화 되어있기 때문에 화살표 UI 비활성화
            if (LangRect.sizeDelta.x < barOpen)
            {
                LangRect.sizeDelta = Vector2.Lerp(LangRect.sizeDelta, new Vector2(barOpen + 5f, 1080), langnavi_t);
                LangChildImg.fillAmount += 0.5f * langnavi_t;
            }
            else if (LangRect.sizeDelta.x >= barOpen)
            {
                //LanguageBar.gameObject.SetActive(true);
                gamemanager.LanguageBar.transform.GetChild(0).gameObject.SetActive(true);
                //gamemanager.LanguageBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<ScrollRect>().enabled = true;
                LangRect.sizeDelta = new Vector2(barOpen, 1080);
                LangChildImg.fillAmount = 1;
                langNaviOn = true;
                movelangNavi = false;
            }
        }
    }

    public void SelectETCChange()
    {
        if (ETCNaviOn == true)     // ETC 창 비활성화
        {
            if (SceneManager.GetActiveScene().name.Contains("XRMode") || SceneManager.GetActiveScene().name.Contains("ClearMode"))        // 역사모드가 아니면 네비게이션 창의 상태에 따라(활성화/비활성화) 화살표UI 활성화 비활성화
            {
                if (NaviRect.sizeDelta.x < barOpen)     // 네비게이션 창이 비활성화 상태이므로 화살표 UI 활성화
                {
                    if (LangRect.sizeDelta.x < barOpen)     // 네비게이션 창이 비활성화 상태이므로 화살표 UI 활성화
                    {
                        if (FunctionCustom.GuideMode == false && FunctionCustom.PastMode == false && FunctionCustom.View360 == false)
                        {
                            gamemanager.Arrow.gameObject.SetActive(true);
                            gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                        }
                        else
                        {
                            if (FunctionCustom.GuideMode == true && FunctionCustom.functionorigin.guidemode.GuideObj.activeSelf)
                            {
                                gamemanager.Arrow.gameObject.SetActive(false);
                            }
                            else if (FunctionCustom.PastMode == true && FunctionCustom.functionorigin.pastcontents.obj360.activeSelf)
                            {
                                gamemanager.Arrow.gameObject.SetActive(false);
                            }
                            else if (FunctionCustom.View360 == true && FunctionCustom.functionorigin.view360.obj360.activeSelf)
                            {
                                gamemanager.Arrow.gameObject.SetActive(false);
                            }
                            else
                            {
                                gamemanager.Arrow.gameObject.SetActive(true);
                                gamemanager.Arrow.transform.position = gamemanager.Arrowpos_extend;
                            }
                        }
                    }
                    else if (NaviRect.sizeDelta.x > barClose)     // 네비게이션 창이 활성화상태이므로 화살표 UI 비활성화
                    {
                        gamemanager.Arrow.gameObject.SetActive(false);
                    }
                }
                else if (NaviRect.sizeDelta.x > barClose)     // 네비게이션 창이 활성화상태이므로 화살표 UI 비활성화
                {
                    gamemanager.Arrow.gameObject.SetActive(false);
                }
            }
            else       // 역사모드에서는 네비게이션 창에 상관없이 화살표 UI 비활성화
            {
                gamemanager.Arrow.gameObject.SetActive(false);
            }

            // ETC 창 비활성화 진행
            if (ETCRect.sizeDelta.x > barClose)
            {
                ETCRect.sizeDelta = Vector2.Lerp(ETCRect.sizeDelta, new Vector2(barClose - 5f, 1080), ETCnavi_t);
                EtcChildImg.fillAmount -= 0.5f * ETCnavi_t;
            }
            else if (ETCRect.sizeDelta.x <= barClose)
            {
                ETCRect.sizeDelta = new Vector2(barClose, 1080);
                EtcChildImg.fillAmount = 0;
                gamemanager.ETCBar.transform.GetChild(0).gameObject.SetActive(false);
                gamemanager.MenuBar.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                ETCNaviOn = false;
                moveETCNavi = false;
            }
        }
        else if (ETCNaviOn == false)       // ETC 활성화
        {
            gamemanager.ETCBar.transform.GetChild(0).gameObject.SetActive(true);       // ETC 창 활성화
            gamemanager.Arrow.gameObject.SetActive(false);      // ETC창이 활성화 되어있기 때문에 화살표 UI 비활성화
            if (ETCRect.sizeDelta.x < barOpen)
            {
                ETCRect.sizeDelta = Vector2.Lerp(ETCRect.sizeDelta, new Vector2(barOpen + 5f, 1080), ETCnavi_t);
                EtcChildImg.fillAmount += 0.5f * ETCnavi_t;
            }
            else if (ETCRect.sizeDelta.x >= barOpen)
            {
                //ETCBar.gameObject.SetActive(true);
                gamemanager.ETCBar.transform.GetChild(0).gameObject.SetActive(true);
                gamemanager.ETCBar.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<ScrollRect>().enabled = true;
                ETCRect.sizeDelta = new Vector2(barOpen, 1080);
                EtcChildImg.fillAmount = 1;
                ETCNaviOn = true;
                moveETCNavi = false;
            }
        }
    }
}
