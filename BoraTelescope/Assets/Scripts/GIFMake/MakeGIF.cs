using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeGIF : MonoBehaviour
{
    public Record recordGIF;
    public Text Checktime;
    public static float time;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("readytostart", 8f);
    }

    public void readytostart()
    {
        Debug.Log(time + " Readytostart");
        Record.m_Recorder.Record();
        recordGIF.m_Progress = 0f;
        Invoke("readytosave", 5f);
    }

    public void readytosave()
    {
        Record.m_Recorder.Save();
        recordGIF.m_Progress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;
        //Checktime.text = time.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            readytostart();
        }
    }
}
