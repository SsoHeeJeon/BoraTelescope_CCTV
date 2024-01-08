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

    // �׺���̼�â, ����â On/Off
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

        // ����â �ݾƳ���(�ε�ȭ�鿡�� �Ⱥ���.)
        LangRect.sizeDelta = new Vector2(barClose, 1080);
        LangChildImg.fillAmount = 0;
        gamemanager.LanguageBar.transform.GetChild(0).gameObject.SetActive(false);
        langNaviOn = false;
        movelangNavi = false;

        // ETCâ �ݾƳ���(�ε�ȭ�鿡�� �Ⱥ���.)
        ETCRect.sizeDelta = new Vector2(barClose, 1080);
        gamemanager.ETCBar.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = 0;
        gamemanager.ETCBar.transform.GetChild(0).gameObject.SetActive(false);
        ETCNaviOn = false;
        moveETCNavi = false;
    }

    // Update is called once per frame
    void Update()
    {
        // �׺���̼� â, �󼼼���â �ӵ� ���� 
        navi_t += Time.deltaTime * 0.1f;
        langnavi_t += Time.deltaTime * 0.1f;
        ETCnavi_t += Time.deltaTime * 0.1f;

        // �׺���̼� â �󺧼��ú� ��ũ�ѹ� ������
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
    /// �׺���̼� â Ȱ��ȭ/��Ȱ��ȭ
    /// </summary>
    public void NaviArr_set()
    {
        //Image NaviBackground = gamemanager.NavigationBar.transform.GetChild(0).gameObject.GetComponent<Image>();

        // �׺���̼� ��Ȱ��ȭ
        if (NaviOn == true)
        {
            // ���� ��ȹ : �޴��ٰ� ������� �׺���̼� â�� ����
            // ���� ��ȹ : �׺���̼� â�� ���� �־ ũ�Ⱑ Ŀ��

            // �׺���̼�â �α�
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

            // �׺���̼�â�� ��Ȱ��ȭ �Ǿ��ֱ� ������ ���� â�� Ȱ��ȭ/��Ȱ���U�Ǿ� �ִ� ���¿� ���� ȭ��ǥ UI Ȱ��ȭ/��Ȱ��ȭ
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

            gamemanager.NavigationBar.gameObject.SetActive(true);   // �׺���̼� â Ȱ��ȭ

            // �׺���̼� â�� ��Ȱ��ȭ�Ǿ� �ֱ� ������ �׺���̼� �� ��ư ��Ȱ��ȭ
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
        else if (NaviOn == false)       // �׺���̼� Ȱ��ȭ
        {
            // �׺���̼�â Ȱ��ȭ �α�
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

            gamemanager.NavigationBar.transform.GetChild(0).gameObject.SetActive(true);     // �׺���̼�â Ȱ��ȭ
            gamemanager.Arrow.gameObject.SetActive(false);      // �׺���̼�â�� Ȱ��ȭ �Ǿ��ֱ� ������ ȭ��ǥ�� ��Ȱ��ȭ

            // �׺���̼� â õõ�� Ȱ��ȭ(������ ȿ��)
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

                // �׺���̼� â�� ��� ������ �׺���̼� �� ��ư Ȱ��ȭ
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
    /// �޴��ٿ��� ���� ������ �����ϸ� ����â Ȱ��ȭ/��Ȱ��ȭ
    /// </summary>
    public void SelectLanguageChange()
    {
        if (langNaviOn == true)     // ���� â ��Ȱ��ȭ
        {
            if (!SceneManager.GetActiveScene().name.Contains("CultureMode"))        // �����尡 �ƴϸ� �׺���̼� â�� ���¿� ����(Ȱ��ȭ/��Ȱ��ȭ) ȭ��ǥUI Ȱ��ȭ ��Ȱ��ȭ
            {
                if (NaviRect.sizeDelta.x < barOpen)     // �׺���̼� â�� ��Ȱ��ȭ �����̹Ƿ� ȭ��ǥ UI Ȱ��ȭ
                {
                    if (ETCRect.sizeDelta.x < barOpen)     // �׺���̼� â�� ��Ȱ��ȭ �����̹Ƿ� ȭ��ǥ UI Ȱ��ȭ
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
                    else if (ETCRect.sizeDelta.x > barClose)     // �׺���̼� â�� ��Ȱ��ȭ �����̹Ƿ� ȭ��ǥ UI Ȱ��ȭ
                    {
                        gamemanager.Arrow.gameObject.SetActive(false);
                    }
                }
                else if (NaviRect.sizeDelta.x > barClose)     // �׺���̼� â�� Ȱ��ȭ�����̹Ƿ� ȭ��ǥ UI ��Ȱ��ȭ
                {
                    gamemanager.Arrow.gameObject.SetActive(false);
                }
            }
            else if (SceneManager.GetActiveScene().name.Contains("CultureMode"))      // �����忡���� �׺���̼� â�� ������� ȭ��ǥ UI ��Ȱ��ȭ
            {
                gamemanager.Arrow.gameObject.SetActive(false);
            }

            // ���� â ��Ȱ��ȭ ����
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
        else if (langNaviOn == false)       // ���� Ȱ��ȭ
        {
            gamemanager.LanguageBar.transform.GetChild(0).gameObject.SetActive(true);       // ���� â Ȱ��ȭ
            gamemanager.Arrow.gameObject.SetActive(false);      // ����â�� Ȱ��ȭ �Ǿ��ֱ� ������ ȭ��ǥ UI ��Ȱ��ȭ
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
        if (ETCNaviOn == true)     // ETC â ��Ȱ��ȭ
        {
            if (SceneManager.GetActiveScene().name.Contains("XRMode") || SceneManager.GetActiveScene().name.Contains("ClearMode"))        // �����尡 �ƴϸ� �׺���̼� â�� ���¿� ����(Ȱ��ȭ/��Ȱ��ȭ) ȭ��ǥUI Ȱ��ȭ ��Ȱ��ȭ
            {
                if (NaviRect.sizeDelta.x < barOpen)     // �׺���̼� â�� ��Ȱ��ȭ �����̹Ƿ� ȭ��ǥ UI Ȱ��ȭ
                {
                    if (LangRect.sizeDelta.x < barOpen)     // �׺���̼� â�� ��Ȱ��ȭ �����̹Ƿ� ȭ��ǥ UI Ȱ��ȭ
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
                    else if (NaviRect.sizeDelta.x > barClose)     // �׺���̼� â�� Ȱ��ȭ�����̹Ƿ� ȭ��ǥ UI ��Ȱ��ȭ
                    {
                        gamemanager.Arrow.gameObject.SetActive(false);
                    }
                }
                else if (NaviRect.sizeDelta.x > barClose)     // �׺���̼� â�� Ȱ��ȭ�����̹Ƿ� ȭ��ǥ UI ��Ȱ��ȭ
                {
                    gamemanager.Arrow.gameObject.SetActive(false);
                }
            }
            else       // �����忡���� �׺���̼� â�� ������� ȭ��ǥ UI ��Ȱ��ȭ
            {
                gamemanager.Arrow.gameObject.SetActive(false);
            }

            // ETC â ��Ȱ��ȭ ����
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
        else if (ETCNaviOn == false)       // ETC Ȱ��ȭ
        {
            gamemanager.ETCBar.transform.GetChild(0).gameObject.SetActive(true);       // ETC â Ȱ��ȭ
            gamemanager.Arrow.gameObject.SetActive(false);      // ETCâ�� Ȱ��ȭ �Ǿ��ֱ� ������ ȭ��ǥ UI ��Ȱ��ȭ
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
