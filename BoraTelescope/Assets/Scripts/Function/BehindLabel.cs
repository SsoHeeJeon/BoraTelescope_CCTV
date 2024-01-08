using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HiddenPosition
{
    public string LabelName;
    public float Label_X;
    public float Label_Y;
    public float Scale;

    public HiddenPosition(string labelname, float label_x, float label_y, float scale)
    {
        LabelName = labelname;
        Label_X = label_x;
        Label_Y = label_y;
        Scale = scale;
    }
}
public class BehindLabel : MonoBehaviour
{
    //json파일 불러오기
    // xrmanager에서 라벨위치 변경가능하게하기
    public XRMode_Manager xrmodemanager;

    public static List<string> HiddenLabelList = new List<string> { "Hidden1", "Hidden2", "Hidden3" };
    public GameObject[] HiddenObj_s;
    public static GameObject[] HiddenObj;
    public static Vector3[] HiddenLabelPosition = new Vector3[3];
    public static string Allstr;
    public static string[] allstr_json;

    public static List<string> AllLabelPosition = new List<string>();
    public static string[] AllLabelPosition_arr;

    public void ReadytoStart()
    {
        HiddenObj = HiddenObj_s;
    }

    public static void LabelPositionSet_Start()
    {
        for (int index = 0; index < HiddenObj.Length; index++)
        {
            HiddenObj[index].GetComponent<Button>().enabled = true;
        }
    }

    public void ClickHiddenLabel(GameObject btn)
    {
        xrmodemanager.SelectLabel = btn;
        xrmodemanager.setlabelcamera = true;
    }

    public static void LabelPositionSet_Finish()
    {
        for (int index = 0; index < HiddenObj.Length; index++)
        {
            SavePosition(HiddenObj[index].name, HiddenObj[index].transform.localPosition.x, HiddenObj[index].transform.localPosition.y, HiddenObj[index].transform.localScale.x);
            HiddenObj[index].GetComponent<Button>().enabled = false;
        }
    }

    public static void SavePosition(string labelname, float label_x, float label_y, float scale)
    {
        HiddenPosition labelposition = new HiddenPosition(labelname, label_x, label_y, scale);
        //StartPosition = gamemanager.StartLabel;

        string str = JsonUtility.ToJson(labelposition);

        AllLabelPosition_arr = new string[HiddenObj.Length];

        AllLabelPosition.Add(str);
        AllLabelPosition_arr = AllLabelPosition.ToArray();

        Allstr = "";

        if (AllLabelPosition.Count == HiddenObj.Length)
        {
            for (int index = 0; index < HiddenObj.Length; index++)
            {
                Allstr += AllLabelPosition_arr[index].ToString();
            }

            File.WriteAllText(Application.dataPath + ("/XRModeLabelPosition_" + ContentsInfo.ContentsName + "_1.json"), Allstr);

            ReadLabelPosition();
        }
    }

    public static void ReadLabelPosition()
    {
        if (File.Exists(Application.dataPath + ("/XRModeLabelPosition_" + ContentsInfo.ContentsName + "_1.json")))
        {
            Allstr = File.ReadAllText(Application.dataPath + ("/XRModeLabelPosition_" + ContentsInfo.ContentsName + "_1.json"));
            
            if (Allstr.Contains("}"))
            {
                allstr_json = Allstr.Split('}');

                for (int index = -1; index < allstr_json.Length - 1; index++)
                {
                    if (index < allstr_json.Length - 2)
                    {
                        allstr_json[index + 1] = allstr_json[index + 1] + "}";
                    }
                }
            }

            for (int index = 0; index < allstr_json.Length - 2; index++)
            {
                HiddenPosition labelPosition = JsonUtility.FromJson<HiddenPosition>(allstr_json[index]);
                //Debug.Log("today " + HiddenLabelPosition.Length);
                for (int sindex = 0; sindex < HiddenLabelPosition.Length; sindex++)
                {
                    if (sindex == index)
                    {
                        HiddenObj[sindex].transform.localPosition = new Vector3(labelPosition.Label_X * XRMode_Manager.TotalPan, labelPosition.Label_Y * XRMode_Manager.TotalTilt, 0);
                        HiddenObj[sindex].transform.localScale = new Vector3(labelPosition.Scale, labelPosition.Scale, labelPosition.Scale);
                    }
                }
            }
        }
    }
}
