using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public GameManager gamemanager;

    public InputField SettingPW;
    public GameObject PWPage;
    public GameObject SettingPg;
    public GameObject Setting_background;
    public Text changewaitingtime;
    public GameObject PWError;

    public static string Password_Setting;
    int wt;
    
    bool openkeyboard = false;

    System.Diagnostics.Process ps = new System.Diagnostics.Process();

    // Update is called once per frame
    void Update()
    {
        if (Setting_background.activeSelf && PWPage.activeSelf)
        {
            ps.StartInfo.FileName = "osk.exe";
            var processList = System.Diagnostics.Process.GetProcessesByName("osk");
            if (SettingPW.isFocused == true && openkeyboard == false)
            {
                if (processList.Length == 0)
                {
                    ps.Start();
                }
                openkeyboard = true;
            }
            else if (SettingPW.isFocused == false)
            {
                if (processList.Length != 0)
                {
                    processList[0].Kill();
                }
                openkeyboard = false;
            }
        }
    }

    public void ChangeMainMode(GameObject btn)
    {
        GameManager.MainMode = btn.name;
        for (int index = 0; index < SettingPg.transform.childCount; index++)
        {
            SettingPg.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        btn.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ConfirmWaitingTime()
    {
        GameManager.waitingTime = int.Parse(changewaitingtime.text);

        gamemanager.GetComponent<ReadJson>().CustomWaitingTime();
        SettingPGClose();
    }

    public void SetWaitingTime(GameObject btn)
    {
        wt = int.Parse(changewaitingtime.text);

        if (btn.name == "plus")
        {
            if (wt < 300)
            {
                wt += 30;
            }
        }
        else if (btn.name == "minus")
        {
            if (wt > 90)
            {
                wt -= 30;
            }
        }
        changewaitingtime.text = wt.ToString();
    }

    public void ConfirmChangeMain()
    {
        if (SettingPW.text == Password_Setting)
        {
            for (int index = 0; index < SettingPg.transform.childCount; index++)
            {
                SettingPg.transform.GetChild(index).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

            if (GameManager.MainMode == "LiveMode")
            {
                SettingPg.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (GameManager.MainMode == "XRMode")
            {
                SettingPg.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (GameManager.MainMode == "ClearMode")
            {
                SettingPg.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }

            changewaitingtime.text = GameManager.waitingTime.ToString();
            SettingPg.SetActive(true);
            PWPage.SetActive(false);
            PWError.SetActive(false);
            SettingPW.text = "";

            var processList = System.Diagnostics.Process.GetProcessesByName("osk");
            if (processList.Length != 0)
            {
                processList[0].Kill();
            }
            openkeyboard = false;
        }
        else
        {
            if (SettingPW.text != "")
            {
                PWError.SetActive(true);
            }
        }
    }

    public void SettingPGClose()
    {
        gamemanager.Wcount = 0;
        SettingPW.text = "";
        SettingPg.SetActive(false);
        PWError.SetActive(false);
        PWPage.SetActive(true);
        Setting_background.SetActive(false);
        gamemanager.Settingbtn.transform.GetChild(0).gameObject.SetActive(false);
    }
}
