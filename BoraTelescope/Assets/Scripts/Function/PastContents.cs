using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PastContents : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject obj360;
    public GameObject obj360_Dis;
    public Material obj360_m;
    public GameObject currentbtn;

    public string currentmode;
    public VideoPlayer DissolveV;

    public static Sprite Day360;
    public static Sprite Night360;
    public static Sprite Past70;
    public static Sprite Past80;
    public static Sprite Current;

    public static string[] DisVideo;
    public static string Dis_Past7087;
    public static string Dis_Past8770;
    public static string Dis_Past70Cur;
    public static string Dis_Past87Cur;
    public static string Dis_CurPast70;
    public static string Dis_CurPast87;

    public GameObject All360Label;
    public GameObject Label70;
    public GameObject Label80;
    public GameObject LabelCur;

    bool PastModeLabelOn = false;
    float labelON;
    float labelOFF;
    Image[] OnLabel;
    Image[] OffLabel;

    private void Update()
    {
        if (obj360.transform.parent.gameObject.activeSelf)
        {
            if (Input.touchCount == 1)
            {
                gamemanager.setdragrange.ALLFuncDragRange();
            }
        }

        if(PastModeLabelOn == true)
        {
            if (OnLabel[0].color.a != 1)
            {
                for (int index = 0; index < OnLabel.Length; index++)
                {
                    OnLabel[index].color = new Color(1, 1, 1, OnLabel[index].color.a + 0.008f);
                }
            }

            if (OffLabel[0].color.a != 0)
            {
                for (int index = 0; index < OffLabel.Length; index++)
                {
                    OffLabel[index].color = new Color(1, 1, 1, OffLabel[index].color.a - 0.008f);
                }
            } else if(OffLabel[0].color.a == 0)
            {
                OffLabel[0].gameObject.transform.parent.gameObject.SetActive(false);

                if(OnLabel[0].color.a == 1)
                {
                    PastModeLabelOn = false;
                }
            }
        }

        Vector3 charAngle = obj360.transform.eulerAngles;
        charAngle.z = (charAngle.z > 180) ? charAngle.z - 360 : charAngle.z;
        charAngle.z = Mathf.Clamp(charAngle.z, -1.5f, 40f);
        charAngle.y = (charAngle.y > 180) ? charAngle.y - 360 : charAngle.y;
        charAngle.y = Mathf.Clamp(charAngle.y, -90, -80);

        obj360.transform.rotation = Quaternion.Euler(charAngle);
        obj360_Dis.transform.rotation = Quaternion.Euler(charAngle);
    }

    float bx;
    float by;
    float fx;
    float fy;
    string dir_360;
    Vector3 beforepos;
    private float dragmove_t;

    private Vector3 firsttouchmove;
    private Vector3 secondtouchmove;
    public static bool gosecond = false;
    public static bool dontclick = false;

    public void ChangeImage(string kind)
    {
        switch (kind)
        {
            case "Past70":
                if (obj360.transform.parent.gameObject.activeSelf)
                {
                    if (obj360_m.GetTexture("_MainTex") != Past70.texture)
                    {
                        obj360_m.SetTexture("_MainTex", Past70.texture);
                    }
                } else if (!obj360.transform.parent.gameObject.activeSelf)
                {
                    obj360_m.SetTexture("_MainTex", Past70.texture);
                }
                break;
            case "Past80":
                if (obj360.transform.parent.gameObject.activeSelf)
                {
                    if (obj360_m.GetTexture("_MainTex") != Past80.texture)
                    {
                        obj360_m.SetTexture("_MainTex", Past80.texture);
                    }
                }
                else if (!obj360.transform.parent.gameObject.activeSelf)
                {
                    obj360_m.SetTexture("_MainTex", Past80.texture);
                }
                break;
            case "Current":
                if (obj360.transform.parent.gameObject.activeSelf)
                {
                    if (obj360_m.GetTexture("_MainTex") != Current.texture)
                    {
                        obj360_m.SetTexture("_MainTex", Current.texture);
                    }
                }
                else if (!obj360.transform.parent.gameObject.activeSelf)
                {
                    obj360_m.SetTexture("_MainTex", Current.texture);
                }
                break;
        }
        currentmode = kind;
        obj360.GetComponent<MeshRenderer>().enabled = true;

        Readytofinish();
    }

    public void Readytofinish()
    {
        gamemanager.allbar.navi_t = 0;
        gamemanager.allbar.NaviOn = true;
        gamemanager.allbar.moveNavi = true;

        gamemanager.Arrow.SetActive(false);
        
        gamemanager.MiniMap_Background.transform.parent.parent.gameObject.SetActive(false);

        obj360.transform.parent.gameObject.SetActive(true);
    }

    public void SeeAllRound()
    {
        if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
        {
            if (Input.touches[0].phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    beforepos = Input.mousePosition;
                    bx = beforepos.x;
                    by = beforepos.y;
                }
                else
                {
                    beforepos = Input.GetTouch(0).position;
                    bx = beforepos.x;
                    by = beforepos.y;
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Moved || Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x != bx)
                {
                    if (obj360.GetComponent<MeshRenderer>().enabled == true)
                    {
                        obj360_Dis.SetActive(false);
                    }
                    fx = (bx - Input.mousePosition.x);
                    fy = (by - Input.mousePosition.y);

                    dragmove_t += Time.deltaTime;

                    if ((int)dragmove_t >= 0 && (int)dragmove_t < 1)
                    {
                        firstmoveposition();
                    }
                    exceptfilterWin();
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                fx = 0;
                fy = 0;
                obj360_Dis.SetActive(true);
                obj360.transform.rotation = Quaternion.Euler(0, obj360.transform.rotation.eulerAngles.y, obj360.transform.rotation.eulerAngles.z);
                obj360_Dis.transform.rotation = Quaternion.Euler(0, obj360_Dis.transform.rotation.eulerAngles.y, obj360_Dis.transform.rotation.eulerAngles.z);

                dontclick = false;
                gosecond = false;
                dragmove_t = 0;
                firsttouchmove = Vector3.zero;
                secondtouchmove = Vector3.zero;
            }
        } else if (Input.touchCount == 0)
        {
            obj360_Dis.SetActive(true);
            fx = 0;
            fy = 0;

            obj360.transform.rotation = Quaternion.Euler(0, obj360.transform.rotation.eulerAngles.y, obj360.transform.rotation.eulerAngles.z);
            obj360_Dis.transform.rotation = Quaternion.Euler(0, obj360_Dis.transform.rotation.eulerAngles.y, obj360_Dis.transform.rotation.eulerAngles.z);

            dontclick = false;
            gosecond = false;
            dragmove_t = 0;
            firsttouchmove = Vector3.zero;
            secondtouchmove = Vector3.zero;
        }
    }

    public void firstmoveposition()
    {
        gosecond = true;
        Invoke("secondmoveposition", 0.01f);
    }

    public void secondmoveposition()
    {
        if (gosecond == true)
        {
            beforepos = Input.mousePosition;

            bx = beforepos.x;
            by = beforepos.y;

            secondtouchmove = Input.mousePosition;

            dragmove_t = 0;
            firsttouchmove = Vector3.zero;
            secondtouchmove = Vector3.zero;
            if (Vector3.Distance(firsttouchmove, secondtouchmove) < 0.05f && dragmove_t >= 0.35f)
            {
                dontclick = true;
                gosecond = false;
            }
        }
    }

    public void exceptfilterWin()
    {
        if (!(Input.mousePosition.x <= 1385f && Input.mousePosition.x >= 550f && Input.mousePosition.y <= 1030f && Input.mousePosition.y >= 890f))
        {
            touchmovecamera();
        }
    }
    float LabelX;
    float LabelY;
    public void touchmovecamera()
    {
        if (obj360.transform.rotation.eulerAngles.y + -fx * 0.5f >= 269.45f && obj360.transform.rotation.eulerAngles.y + -fx * 0.5f <= 280)
        {
            obj360.transform.rotation = Quaternion.Euler(0, obj360.transform.rotation.eulerAngles.y + -fx * 0.5f, obj360.transform.rotation.eulerAngles.z + -fy * 0.1f);
        }
        else if (obj360.transform.rotation.eulerAngles.y + -fx * 0.5f < 269.45f)
        {
            obj360.transform.rotation = Quaternion.Euler(0, 269.45f, obj360.transform.rotation.eulerAngles.z + -fy * 0.1f);
        }
        else if (obj360.transform.rotation.eulerAngles.y + -fx * 0.5f > 280)
        {
            obj360.transform.rotation = Quaternion.Euler(0, 279.9f, obj360.transform.rotation.eulerAngles.z + -fy * 0.1f);
        }

        for (int index = 0; index < Label70.transform.childCount; index++)
        {
            Label70.transform.GetChild(index).gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        for (int index = 0; index < Label80.transform.childCount; index++)
        {
            Label80.transform.GetChild(index).gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        for (int index = 0; index < LabelCur.transform.childCount; index++)
        {
            LabelCur.transform.GetChild(index).gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (SceneManager.GetActiveScene().name == "ClearMode")
        {
            if (obj360.transform.rotation.eulerAngles.z < 330)
            {
                LabelY = obj360.transform.rotation.eulerAngles.z * 10f;
            }
            else if (obj360.transform.rotation.eulerAngles.z >= 330)
            {
                LabelY = (obj360.transform.rotation.eulerAngles.z - 360) * 10f;
            }

            LabelX = (obj360.transform.localEulerAngles.y - 270) / 10 * 170;
            All360Label.transform.localPosition = new Vector3(LabelX, LabelY, 0);
            All360Label.transform.parent.transform.localRotation = Quaternion.Euler(-obj360.transform.rotation.eulerAngles.z, -90, 0);
        }
        else if (SceneManager.GetActiveScene().name == "XRMode")
        {
            if (obj360.transform.rotation.eulerAngles.y <= 270)
            {
                All360Label.transform.parent.transform.localRotation = Quaternion.Euler(-obj360.transform.rotation.eulerAngles.z, -90, 0);
            }
            else if (obj360.transform.rotation.eulerAngles.y > 270)
            {
                All360Label.transform.parent.transform.localRotation = Quaternion.Euler(-obj360.transform.rotation.eulerAngles.z, obj360.transform.rotation.eulerAngles.y, 0);
            }

            if (obj360.transform.rotation.eulerAngles.z < 330)
            {
                LabelY = obj360.transform.rotation.eulerAngles.z * 10f;
            }
            else if (obj360.transform.rotation.eulerAngles.z >= 330)
            {
                LabelY = (obj360.transform.rotation.eulerAngles.z - 360) * 10f;
            }

            LabelX = (obj360.transform.localEulerAngles.y - 270) / 10 * 170;
            All360Label.transform.localPosition = new Vector3(LabelX, LabelY, Mathf.Abs(All360Label.transform.localPosition.y));

            //All360Label.transform.localPosition = new Vector3(All360Label.transform.localPosition.x, All360Label.transform.localPosition.y, Mathf.Abs((int)All360Label.transform.localPosition.y / 10));
        }
        obj360_Dis.transform.rotation = obj360.transform.rotation;
    }

    float laby;
    public GameObject moveTime;
    public void SelectLoc(GameObject btn)
    {
        //    for (int index = 1; index < 4; index++)
        //    {
        //        moveTime.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //        moveTime.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
        //    }
        //    btn.transform.GetChild(0).gameObject.SetActive(true);
        //    //ChangeImage(btn.name);

        if (currentmode != btn.name)
        {
            switch (btn.name)
            {
                case "Past70":
                    if (currentmode == "Current")
                    {
                        DissolveV.url = Dis_CurPast70;
                        OffLabel = new Image[LabelCur.transform.childCount];
                        for (int index = 0; index < OffLabel.Length; index++)
                        {
                            OffLabel[index] = LabelCur.transform.GetChild(index).gameObject.GetComponent<Image>();
                            OffLabel[index].color = new Color(1, 1, 1, 1);
                        }
                    }
                    else if (currentmode == "Past80")
                    {
                        DissolveV.url = Dis_Past8770;

                        OffLabel = new Image[Label80.transform.childCount];
                        for (int index = 0; index < OffLabel.Length; index++)
                        {
                            OffLabel[index] = Label80.transform.GetChild(index).gameObject.GetComponent<Image>();
                            OffLabel[index].color = new Color(1, 1, 1, 1);
                        }
                    }

                    Label70.SetActive(true);
                    OnLabel = new Image[Label70.transform.childCount];
                    for (int index = 0; index < OnLabel.Length; index++)
                    {
                        OnLabel[index] = Label70.transform.GetChild(index).gameObject.GetComponent<Image>();
                        OnLabel[index].color = new Color(1, 1, 1, 0);
                    }
                    break;
                case "Past80":
                    if (currentmode == "Current")
                    {
                        DissolveV.url = Dis_CurPast87;
                        OffLabel = new Image[LabelCur.transform.childCount];
                        for (int index = 0; index < OffLabel.Length; index++)
                        {
                            OffLabel[index] = LabelCur.transform.GetChild(index).gameObject.GetComponent<Image>();
                            OffLabel[index].color = new Color(1, 1, 1, 1);
                        }
                    }
                    else if (currentmode == "Past70")
                    {
                        DissolveV.url = Dis_Past7087;
                        OffLabel = new Image[Label70.transform.childCount];
                        for (int index = 0; index < OffLabel.Length; index++)
                        {
                            OffLabel[index] = Label70.transform.GetChild(index).gameObject.GetComponent<Image>();
                            OffLabel[index].color = new Color(1, 1, 1, 1);
                        }
                    }

                    Label80.SetActive(true);
                    OnLabel = new Image[Label80.transform.childCount];
                    for (int index = 0; index < OnLabel.Length; index++)
                    {
                        OnLabel[index] = Label80.transform.GetChild(index).gameObject.GetComponent<Image>();
                        OnLabel[index].color = new Color(1, 1, 1, 0);
                    }
                    break;
                case "Current":
                    if (currentmode == "Past70")
                    {
                        DissolveV.url = Dis_Past70Cur;
                        OffLabel = new Image[Label70.transform.childCount];
                        for (int index = 0; index < OffLabel.Length; index++)
                        {
                            OffLabel[index] = Label70.transform.GetChild(index).gameObject.GetComponent<Image>();
                            OffLabel[index].color = new Color(1, 1, 1, 1);
                        }
                    }
                    else if (currentmode == "Past80")
                    {
                        DissolveV.url = Dis_Past87Cur;
                        OffLabel = new Image[Label80.transform.childCount];
                        for (int index = 0; index < OffLabel.Length; index++)
                        {
                            OffLabel[index] = Label80.transform.GetChild(index).gameObject.GetComponent<Image>();
                            OffLabel[index].color = new Color(1, 1, 1, 1);
                        }
                    }

                    LabelCur.SetActive(true);
                    OnLabel = new Image[LabelCur.transform.childCount];
                    for (int index = 0; index < OnLabel.Length; index++)
                    {
                        OnLabel[index] = LabelCur.transform.GetChild(index).gameObject.GetComponent<Image>();
                        OnLabel[index].color = new Color(1, 1, 1, 0);
                    }
                    break;
            }
            currentmode = btn.name;

            DissolveV.time = 2f;
            DissolveV.Play();
            for (int index = 0; index < 3; index++)
            {
                moveTime.transform.GetChild(0).transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                moveTime.transform.GetChild(0).transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
            }
            btn.transform.GetChild(0).gameObject.SetActive(true);
            //ChangeImage(btn.name);

            Invoke("seedis", 0.1f);
        }
    }

    public void seedis()
    {
        obj360.GetComponent<MeshRenderer>().enabled = false;
        PastModeLabelOn = true;
        Invoke("ReturnImg", 2f);
    }

    public void ReturnImg()
    {
        if (obj360.transform.parent.gameObject.activeSelf)
        {
            for (int index = 0; index < 3; index++)
            {
                moveTime.transform.GetChild(0).transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
            }
            ChangeImage(currentmode);
        }
    }

    public void Close360()
    {
        PastModeLabelOn = false;
        gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "XRMode")
        {
            if (gamemanager.WantNoLabel == false)
            {
                gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gamemanager.xrmode.AllMapLabels.SetActive(true);

                gamemanager.allbar.navi_t = 0;
                gamemanager.allbar.NaviOn = false;
                gamemanager.allbar.moveNavi = true;
            } else if(gamemanager.WantNoLabel == true)
            {
                gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);

                gamemanager.allbar.navi_t = 0;
                gamemanager.allbar.NaviOn = true;
                gamemanager.allbar.moveNavi = true;

                gamemanager.xrmode.AllMapLabels.SetActive(false);
            }
            //gamemanager.xrmode.xrbtn.transform.parent.gameObject.SetActive(true);
        }
        else if(SceneManager.GetActiveScene().name == "ClearMode")
        {
            gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);

            gamemanager.allbar.navi_t = 0;
            gamemanager.allbar.NaviOn = false;
            gamemanager.allbar.moveNavi = true;

            gamemanager.clearmode.AllMapLabels.SetActive(true);
            ClearMode.changeZoom = new Vector3(0, 0, -3369);
            gamemanager.clearmode.CameraWindow.transform.parent.gameObject.transform.position = new Vector3(0, 0, -3369);
        }

        Label70.SetActive(false);
        Label80.SetActive(false);
        LabelCur.SetActive(true);
        for (int index = 0; index<LabelCur.transform.childCount; index++)
        {
            LabelCur.transform.GetChild(index).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        for(int index =0; index < moveTime.transform.GetChild(0).childCount; index++)
        {
            moveTime.transform.GetChild(0).transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
        }
        obj360.transform.parent.parent = gamemanager.gameObject.transform;
        obj360.transform.parent.gameObject.transform.localPosition = Vector3.zero;
        All360Label.transform.parent.transform.localRotation = Quaternion.Euler(0, -90, 0);
        obj360.transform.parent.gameObject.SetActive(false);
        gamemanager.UI_All.gameObject.SetActive(true);
        for (int index = 0; index < gamemanager.UI_All.transform.childCount; index++)
        {
            gamemanager.UI_All.transform.GetChild(index).gameObject.SetActive(true);
        }
        gamemanager.MiniMap_Background.transform.parent.gameObject.SetActive(true);
        gamemanager.Arrow.SetActive(false);
        gamemanager.Tip_Obj.SetActive(false);
        //gamemanager.Setting_background.SetActive(false);
        //gamemanager.LanguageBar.SetActive(false);

        for (int index = 0; index < gamemanager.label.LabelsParent.transform.childCount; index++)
        {
            gamemanager.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = true;
        }
       
        //gamemanager.AutoSelectImg.gameObject.SetActive(false);
    }
}
