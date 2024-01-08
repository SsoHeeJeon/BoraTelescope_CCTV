using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Clear_Pano : MonoBehaviour
{
    public GameManager gamemanager;

    public GameObject[] Season_Pano = new GameObject[4];
    public GameObject[] Season_Label = new GameObject[4];
    public GameObject[] Season_XR = new GameObject[4];
    public GameObject[] Season_Effect = new GameObject[4];

    float seasonnavi_t;
    float checkseasonopen;
    bool seasonBarMove = false;
    bool seasonBarMoveOn = false;
    bool CheckseasonTime = false;
    public GameObject seasonBar;
    public Image seasonBtn;
    public Sprite SeasonOn_K;
    public Sprite SeasonOn_E;
    public static GameObject SeeHereEffect;

    private enum Season
    {
        Spring = 1,
        Summer = 2,
        Fall = 3,
        Winter = 4
    }

    Dictionary<Season, int> seasonSprites = new Dictionary<Season, int>();
    int Season_int;

    // Start is called before the first frame update
    public void ReadytoStart()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SeeHereEffect = seasonBtn.transform.GetChild(1).gameObject;

        seasonSprites.Add(Season.Spring, Season_Pano[(int)Season.Spring - 1].transform.childCount);
        seasonSprites.Add(Season.Summer, Season_Pano[(int)Season.Summer - 1].transform.childCount);
        seasonSprites.Add(Season.Fall, Season_Pano[(int)Season.Fall - 1].transform.childCount);
        seasonSprites.Add(Season.Winter, Season_Pano[(int)Season.Winter - 1].transform.childCount);

        for (int index = 0; index < 4; index++)
        {
            Season_Pano[index].SetActive(false);
            Season_Label[index].SetActive(false);
            Season_XR[index].SetActive(false);
            Season_Effect[index].SetActive(false);
        }

        if (DateTime.Now.ToString("MM") == "12" || DateTime.Now.ToString("MM") == "01" || DateTime.Now.ToString("MM") == "02")
        {
            Season_int = SeasonSprite(Season.Winter) - 1;
        }
        else if (DateTime.Now.ToString("MM") == "03" || DateTime.Now.ToString("MM") == "04" || DateTime.Now.ToString("MM") == "05")
        {
            Season_int = SeasonSprite(Season.Spring) - 1;
        }
        else if (DateTime.Now.ToString("MM") == "06" || DateTime.Now.ToString("MM") == "07" || DateTime.Now.ToString("MM") == "08")
        {
            Season_int = SeasonSprite(Season.Summer) - 1;
        }
        else if (DateTime.Now.ToString("MM") == "09" || DateTime.Now.ToString("MM") == "10" || DateTime.Now.ToString("MM") == "11")
        {
            Season_int = SeasonSprite(Season.Fall) - 1;
        }

        Season_Pano[Season_int].SetActive(true);
        Season_Label[Season_int].SetActive(true);
        Season_XR[Season_int].SetActive(true);
        Season_Effect[Season_int].SetActive(true);
        gamemanager.clearmode.AllMapLabels = Season_Label[Season_int];
    }

    private int SeasonSprite(Season season)
    {
        int result = 0;
        int index = (int)season;
        int cnt = 0;
        while (cnt < 4)
        {
            cnt++;
            seasonSprites.TryGetValue((Season)index, out result);
            
            if (result == 0)
            {
                index--;
                if (index < 1)
                {
                    index = 4;
                }
                result = index;
            }
            else
            {
                result = index;
                break;
            }
        }
        return result;
    }

    private void Update()
    {
        seasonnavi_t += Time.deltaTime * 1.2f;

        if (seasonBarMoveOn == true)
        {
            SeasonBarOnOff();
        }

        if (CheckseasonTime == true)
        {
            checkseasonopen += Time.deltaTime;
            if ((int)checkseasonopen >= 60)
            {
                if (seasonBar.transform.localPosition.y <= 509)
                {
                    seasonnavi_t = 0;
                    seasonBarMove = false;
                    seasonBarMoveOn = true;
                    CheckseasonTime = false;
                }
            }
        }
    }
    
    int set_int;

    public void ChangePano(GameObject btn)
    {
        Debug.Log(btn.name);
        if (btn.name == "Spring")
        {
            set_int = (int)Season.Spring - 1;
        }
        else if (btn.name == "Summer")
        {
            set_int = (int)Season.Summer - 1;
        }
        else if (btn.name == "Fall")
        {
            set_int = (int)Season.Fall - 1;
        }
        else if (btn.name == "Winter")
        {
            set_int = (int)Season.Winter - 1;
        }

        if (Season_Pano[set_int].transform.childCount != 0)
        {
            for (int index = 0; index < 4; index++)
            {
                Season_Pano[index].SetActive(false);
                Season_Label[index].SetActive(false);
                Season_XR[index].SetActive(false);
                Season_Effect[index].SetActive(false);
            }
            Season_Pano[set_int].SetActive(true);
            Season_Label[set_int].SetActive(true);
            Season_XR[set_int].SetActive(true);
            Season_Effect[set_int].SetActive(true);
            gamemanager.clearmode.AllMapLabels = Season_Label[set_int];
            gamemanager.labelmake.Clear_MapLabel.Clear();

            gamemanager.label.seedebug();

            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Season, "SeasonChange : " + btn.name + "-True", GetType().ToString());
        } else if (Season_Pano[set_int].transform.childCount == 0)
        {
            NoticeWindow.NoticeWindowOpen("SeasonWaiting");
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Season, "SeasonChange : " + btn.name + "-False", GetType().ToString());
            //gamemanager.ErrorMessage.SetActive(true);
        }
    }

    public void SeasonBarOnOff()
    {
        if (seasonBarMove == false)
        {
            if (seasonBar.transform.localPosition.y < 720)      //filterOpen
            {
                seasonBar.transform.localPosition = new Vector3(seasonBar.transform.localPosition.x, Mathf.Lerp(508, 720, seasonnavi_t), seasonBar.transform.localPosition.z);
            }
            else if (seasonBar.transform.localPosition.y >= 720)
            {
                seasonBar.transform.localPosition = new Vector3(seasonBar.transform.localPosition.x, 720, seasonBar.transform.localPosition.z);
                seasonBarMove = true;
                seasonBarMoveOn = false;
                checkseasonopen = 0;
                CheckseasonTime = false;
                seasonBtn.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if (seasonBarMove == true)
        {
            if (seasonBar.transform.localPosition.y > 508)  //filterClose
            {
                seasonBar.transform.localPosition = new Vector3(seasonBar.transform.localPosition.x, Mathf.Lerp(720, 508, seasonnavi_t), seasonBar.transform.localPosition.z);
            }
            else if (seasonBar.transform.localPosition.y <= 508)
            {
                seasonBar.transform.localPosition = new Vector3(seasonBar.transform.localPosition.x, 508, seasonBar.transform.localPosition.z);
                seasonBarMove = false;
                seasonBarMoveOn = false;
                if(GameManager.currentLang == GameManager.Language_enum.Korea)
                {
                    seasonBtn.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = SeasonOn_K;
                }
                else if (GameManager.currentLang != GameManager.Language_enum.Korea)
                {
                    seasonBtn.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = SeasonOn_E;
                }
                seasonBtn.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void SeasonChange()
    {
        if (SeeHereEffect.activeSelf)
        {
            SeeHereEffect.SetActive(false);
        }

        if (seasonBarMoveOn == false)
        {
            seasonBarMoveOn = true;
            if (seasonBar.transform.localPosition.y < 720)
            {
                seasonnavi_t = 0;
                seasonBarMove = false;
            }
            else if (seasonBar.transform.localPosition.y > 508)
            {
                checkseasonopen = 0;
                CheckseasonTime = true;
                seasonnavi_t = 0;
                seasonBarMove = true;

                if(FunctionCustom.filterOff == false)
                {
                    FunctionCustom.functionorigin.FilterReset();
                }
            }
        }
    }
}
